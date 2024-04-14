using Labb1MVCApplication.Data;
using Labb1MVCApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Labb1MVCApplication.Controllers
{
    public class SearchLeaveForEmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SearchLeaveForEmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search()
        {
            ViewData["FkEmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "FirstName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(Guid? id)
        {
            if (id != null)
            {
                Employee? employee = await _context.Employees.FindAsync(id);
                if (employee != null)
                {
                    List<Leave>? leaves = _context.Leaves.Where(e =>
                        e.FkEmployeeId == employee.EmployeeId && e.LeaveStartDate >= DateTime.Now).ToList();

                    SearchLeaveForEmployee searchEmployee = new()
                    {
                        Employee = employee,
                        Leaves = leaves,
                        HasActiveLeave = leaves.Any()
                    };
                    return View("SearchEmployee", searchEmployee);
                }
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult SearchEmployee(SearchLeaveForEmployee searchEmployee)
        {
            return View(searchEmployee);
        }
    }
}
