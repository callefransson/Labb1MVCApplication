using Labb1MVCApplication.Data;
using Labb1MVCApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Labb1MVCApplication.Controllers
{
    public class FilterByDateController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FilterByDateController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult FilterDate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FilterDate(int month)
        {
            if(month != 0)
            {
                List<Leave> leaves = await _context.Leaves
                .Where(l=>l.TimeWhenLeaveWasSet.Month == month)
                .Include(l=>l.Employee)
                .ToListAsync();

            FilterLeaveByDateModel model = new FilterLeaveByDateModel
            {
                Leaves = leaves,
                FilterByDate = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month)
            };
                
            return View("FilterDateResult", model);
            }
            else
            {
                return View();
            }

        }
        public IActionResult FilterDateResult(FilterLeaveByDateModel filter)
        {
            return View(filter);
        }
    }
}
