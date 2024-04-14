using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Labb1MVCApplication.Data;
using Labb1MVCApplication.Models;

namespace Labb1MVCApplication.Controllers
{
    public class LeavesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeavesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Leaves
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Leaves.Include(l => l.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Leaves/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leave = await _context.Leaves
                .Include(l => l.Employee)
                .FirstOrDefaultAsync(m => m.LeaveId == id);
            if (leave == null)
            {
                return NotFound();
            }

            return View(leave);
        }

        // GET: Leaves/Create
        public IActionResult Create()
        {
            ViewData["FkEmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "FirstName");
            return View();
        }

        // POST: Leaves/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeaveId,LeaveType,LeaveStartDate,LeaveEndDate,TimeWhenLeaveWasSet,FkEmployeeId")] Leave leave)
        {
            if (ModelState.IsValid)
            {
                leave.LeaveId = Guid.NewGuid();
                leave.TimeWhenLeaveWasSet = DateTime.Now;
                _context.Add(leave);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkEmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "FirstName", leave.FkEmployeeId);
            return View(leave);
        }
        // GET: Leaves/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            var leaveEmployee = _context.Leaves.Include(l => l.Employee).FirstOrDefault(l => l.LeaveId == id);
            if (id == null)
            {
                return NotFound();
            }
            ViewData["Title"] = "Edit";
            ViewData["EmployeeName"] = leaveEmployee?.Employee?.FirstName;

            var leave = await _context.Leaves.FindAsync(id);
            if (leave == null)
            {
                return NotFound();
            }
            await _context.SaveChangesAsync();
            ViewData["FkEmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "FirstName", leave.FkEmployeeId);
            return View(leave);
        }

        // POST: Leaves/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("LeaveId,LeaveType,LeaveStartDate,LeaveEndDate,TimeWhenLeaveWasSet,FkEmployeeId")] Leave leave)
        {
            if (id != leave.LeaveId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    leave.TimeWhenLeaveWasSet = DateTime.Now;
                    _context.Update(leave);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveExists(leave.LeaveId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkEmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "FirstName", leave.FkEmployeeId);
            return View(leave);
        }

        // GET: Leaves/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leave = await _context.Leaves
                .Include(l => l.Employee)
                .FirstOrDefaultAsync(m => m.LeaveId == id);
            if (leave == null)
            {
                return NotFound();
            }

            return View(leave);
        }
        public async Task<IActionResult> SearchResult(Guid employeeId)
        {
            var employee = await _context.Employees
                .Include(e => e.Leaves)
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);

            if (employee == null)
            {
                return NotFound();
            }
            if (employee.Leaves != null)
            {
                // Visa information om ledighet
                ViewBag.Message = $"{employee.FirstName} har ledighet.";
            }
            else
            {
                ViewBag.Message = $"{employee.FirstName} har ingen ledighet.";
            }
            return View();
        }


        // POST: Leaves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var leave = await _context.Leaves.FindAsync(id);
            if (leave != null)
            {
                _context.Leaves.Remove(leave);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

            private bool LeaveExists(Guid id)
        {
            return _context.Leaves.Any(e => e.LeaveId == id);
        }
    }
}
