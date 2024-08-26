using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rengo.Data;
using Rengo.Models;

namespace Rengo.Controllers
{
    public class RemindersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RemindersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reminders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reminders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reminder reminder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reminder);
                await _context.SaveChangesAsync();

                // Schedule email notification here (e.g., using a background service)
                // EmailService.ScheduleEmail(reminder); // Placeholder for your email scheduling logic

                return RedirectToAction(nameof(Index));
            }
            return View(reminder);
        }

        // GET: Reminders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reminders.ToListAsync());
        }
    }
}
