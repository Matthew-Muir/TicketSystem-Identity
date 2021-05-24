using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketSystem.Data;
using TicketSystem.Models;

namespace TicketSystem.Controllers
{
    public class TicketsController : Controller
    {
        private readonly TicketContext _context;
        private IUnitOfWork _unitOfWork;
        private ITicketRepository _repo;

        public TicketsController(TicketContext context, ITicketRepository repo, IUnitOfWork unitOfWork)
        {
            _context = context;
            _repo = repo;
            _unitOfWork = unitOfWork;
        }

        // GET: Tickets
        public IActionResult Index()
        {
            return View(_repo.GetAllWithDescription());
        }

        // GET: Tickets/Details/5
        public IActionResult Details(int id = 0)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var ticket = _repo.Get(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,DateOpened,DateClosed,Description,ResolutionType")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(ticket);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public IActionResult Edit(int id = 0)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var ticket = _repo.Get(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,DateOpened,DateClosed,Description,ResolutionType")] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var updatedTicket = _repo.Get(ticket.Id);

                updatedTicket.Description = ticket.Description;
                updatedTicket.ResolutionType = ticket.ResolutionType;
                updatedTicket.DateClosed = ticket.DateClosed;
                updatedTicket.DateOpened = ticket.DateOpened;

                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public IActionResult Delete(int id = 0)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var ticket = _repo.Get(id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var ticket = _repo.Get(id);
            _repo.Remove(ticket);
            _unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _repo.GetAll().Any(t => t.Id == id);
        }
    }
}
