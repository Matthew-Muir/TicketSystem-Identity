using Microsoft.EntityFrameworkCore;
using TicketSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSystem.Data
{
    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        public TicketRepository(TicketContext context) : base(context)
        {

        }

        public IEnumerable<Ticket> Take(int num)
        {
            return AppContext.Ticket.Take(num);
        }

        public IEnumerable<Ticket> GetAllWithDescription()
        {
            return AppContext.Ticket;
        }

        private TicketContext AppContext
        {
            get { return Context as TicketContext; }
        }
    }
}
