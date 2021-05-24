using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSystem.Models;

namespace TicketSystem.Data
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        IEnumerable<Ticket> Take(int num);
        IEnumerable<Ticket> GetAllWithDescription();
    }
}