using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSystem.Data
{
    public interface IUnitOfWork
    {
        ITicketRepository Tickets { get; }
        int Complete();
    }
}
