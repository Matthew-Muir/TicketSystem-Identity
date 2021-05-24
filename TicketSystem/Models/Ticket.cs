using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace TicketSystem.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Display(Name = "Date Opened")]
        [DataType(DataType.Date)]
        public DateTime DateOpened { get; set; }

        [Display(Name = "Date Closed")]
        [DataType(DataType.Date)]
        public DateTime? DateClosed { get; set; }


        public string Description { get; set; }

        [Display(Name = "Resolution Type")]
        public ResolutionType ResolutionType { get; set; }

        public virtual string UserId { get; set; }
        public virtual IdentityUser User { get; set; }

    }
}
