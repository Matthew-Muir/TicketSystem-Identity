﻿using TicketSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSystem.Infrastructure
{
    public class OwnershipAuthorizationRequirement : IAuthorizationRequirement
    {
        public bool AllowOwners { get; set; }
        public bool AllowAdmins { get; set; }
    }

    public class OwnershipAuthorizationHandler : AuthorizationHandler<OwnershipAuthorizationRequirement>
    {
        private UserManager<IdentityUser> userManager;

        public OwnershipAuthorizationHandler(UserManager<IdentityUser> usrMgr)
        {
            userManager = usrMgr;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OwnershipAuthorizationRequirement requirement)
        {
          
            Ticket ticket = context.Resource as Ticket;

            string userId = userManager.GetUserId(context.User);

            StringComparison compare = StringComparison.OrdinalIgnoreCase;

            if (ticket != null &&
                userId != null &&
                (requirement.AllowOwners && ticket.UserId.Equals(userId, compare)) ||
                (requirement.AllowAdmins && context.User.IsInRole("Admins"))
            )
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
