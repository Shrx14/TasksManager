using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TasksManager.Data;
using TasksManager.Models;
using System.Linq;

namespace TasksManager.Controllers
{
    public class TaskController : Controller
    {
        private readonly TasksDbContext _context;

        public TaskController(TasksDbContext context)
        {
            _context = context;
        }

        // GET: Task
        public async Task<IActionResult> Index()
        {
            var tasks = await _context.TaskMaster.ToListAsync();
            return View(tasks);
        }

        // GET: Task/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Task/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskMaster taskMaster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taskMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(taskMaster);
        }

        // GET: Task/EditStatus/5
        public async Task<IActionResult> EditStatus(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskMaster = await _context.TaskMaster.FindAsync(id);
            if (taskMaster == null)
            {
                return NotFound();
            }

            var latestStatus = await _context.TaskTransaction
                .Where(t => t.TaskId == id)
                .OrderByDescending(t => t.StatusUpdateDate)
                .FirstOrDefaultAsync();

            ViewBag.LatestStatus = latestStatus?.Status ?? "No status";

            return View(taskMaster);
        }

        // POST: Task/EditStatus/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStatus(int id, string status)
        {
            if (string.IsNullOrEmpty(status))
            {
                ModelState.AddModelError("Status", "Status is required.");
            }

            var taskMaster = await _context.TaskMaster.FindAsync(id);
            if (taskMaster == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var taskTransaction = new TaskTransaction
                {
                    TaskId = id,
                    Status = status,
                    StatusUpdateDate = System.DateTime.Now
                };
                _context.TaskTransaction.Add(taskTransaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var latestStatus = await _context.TaskTransaction
                .Where(t => t.TaskId == id)
                .OrderByDescending(t => t.StatusUpdateDate)
                .FirstOrDefaultAsync();

            ViewBag.LatestStatus = latestStatus?.Status ?? "No status";

            return View(taskMaster);
        }

        // GET: Task/SelectTaskByDate
        public async Task<IActionResult> SelectTaskByDate(string? startDate, string? endDate)
        {
            IQueryable<TaskMaster> query = _context.TaskMaster;

            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                if (System.DateTime.TryParse(startDate, out var start) && System.DateTime.TryParse(endDate, out var end))
                {
                    query = query.Where(t => t.CreatedDate >= start && t.CreatedDate <= end);
                }
            }

            var tasks = await query.ToListAsync();
            return View(tasks);
        }
    }
}
