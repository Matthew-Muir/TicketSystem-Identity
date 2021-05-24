using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TicketSystem.Data;
using System;
using System.Linq;

namespace TicketSystem.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TicketContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<TicketContext>>()))
            {
                
                if (context.Ticket.Any())
                {
                    return;   
                }

                context.Ticket.AddRange(
                    new Ticket
                    {
                        
                        DateOpened = DateTime.Now.AddDays(-5),
                        Description = "Printer not printing",
                        ResolutionType = ResolutionType.Open
                    },

                    new Ticket
                    {
                        DateClosed = DateTime.Now.AddDays(-2),
                        DateOpened = DateTime.Now.AddDays(-5),
                        Description = "Emails not loading",
                        ResolutionType = ResolutionType.Resolved
                    },

                    new Ticket
                    {
                        DateClosed = DateTime.Now.AddDays(-4),
                        DateOpened = DateTime.Now.AddDays(-10),
                        Description = "Spilt drink on laptop",
                        ResolutionType = ResolutionType.Unresolved
                    }


                ); 
                
                context.SaveChanges();
            }
        }
    }
}