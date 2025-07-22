using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoMVC.Data;
using DemoMVC.Models.Entities;
using Microsoft.AspNetCore.Authorization;

namespace DemoMVC.Controllers
{
    [Authorize]
    public class EmployeeEntitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeEntitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]

        // GET: EmployeeEntities
        public async Task<IActionResult> Index()
        {
            return View(await _context.EmployeeEntities.ToListAsync());
        }

        [AllowAnonymous]

        // GET: EmployeeEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeEntity = await _context.EmployeeEntities
                .FirstOrDefaultAsync(m => m.EmployeesId == id);
            if (employeeEntity == null)
            {
                return NotFound();
            }

            return View(employeeEntity);
        }

        [AllowAnonymous]

        // GET: EmployeeEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmployeeEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeesId,FirstName,LastName,Address,DateOfBirth,EmailAddress,HireDate")] EmployeeEntity employeeEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeEntity);
        }

        // GET: EmployeeEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeEntity = await _context.EmployeeEntities.FindAsync(id);
            if (employeeEntity == null)
            {
                return NotFound();
            }
            return View(employeeEntity);
        }

        // POST: EmployeeEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeesId,FirstName,LastName,Address,DateOfBirth,EmailAddress,HireDate")] EmployeeEntity employeeEntity)
        {
            if (id != employeeEntity.EmployeesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeEntityExists(employeeEntity.EmployeesId))
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
            return View(employeeEntity);
        }

        // GET: EmployeeEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeEntity = await _context.EmployeeEntities
                .FirstOrDefaultAsync(m => m.EmployeesId == id);
            if (employeeEntity == null)
            {
                return NotFound();
            }

            return View(employeeEntity);
        }

        // POST: EmployeeEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeEntity = await _context.EmployeeEntities.FindAsync(id);
            if (employeeEntity != null)
            {
                _context.EmployeeEntities.Remove(employeeEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeEntityExists(int id)
        {
            return _context.EmployeeEntities.Any(e => e.EmployeesId == id);
        }
    }
}
