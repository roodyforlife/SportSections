using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportSections.DataBase;
using SportSections.Models;

namespace SportSections.Controllers
{
    public class DepartamentsController : Controller
    {
        private readonly DataBaseContext _context;

        public DepartamentsController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: Departaments
        public async Task<IActionResult> Index(string name)
        {
            IQueryable<Departament> dataBaseContext = _context.Departaments.Include(d => d.Faculty);

            if (!String.IsNullOrEmpty(name))
            {
                dataBaseContext = dataBaseContext.Where(x => x.ShortName.Contains(name) || x.FullName.Contains(name));
            }

            return View(await dataBaseContext.ToListAsync());
        }

        // GET: Departaments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departament = await _context.Departaments
                .Include(d => d.Faculty)
                .Include(x => x.Groups)
                .FirstOrDefaultAsync(m => m.DepartamentId == id);
            if (departament == null)
            {
                return NotFound();
            }

            return View(departament);
        }

        // GET: Departaments/Create
        public IActionResult Create()
        {
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "FullName");
            return View();
        }

        // POST: Departaments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartamentId,ShortName,FullName,FacultyId")] Departament departament)
        {
            if (ModelState.IsValid)
            {
                _context.Add(departament);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "FullName", departament.FacultyId);
            return View(departament);
        }

        // GET: Departaments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departament = await _context.Departaments.FindAsync(id);
            if (departament == null)
            {
                return NotFound();
            }
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "FullName", departament.FacultyId);
            return View(departament);
        }

        // POST: Departaments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepartamentId,ShortName,FullName,FacultyId")] Departament departament)
        {
            if (id != departament.DepartamentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departament);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartamentExists(departament.DepartamentId))
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
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "FullName", departament.FacultyId);
            return View(departament);
        }

        // GET: Departaments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departament = await _context.Departaments
                .Include(d => d.Faculty)
                .FirstOrDefaultAsync(m => m.DepartamentId == id);
            if (departament == null)
            {
                return NotFound();
            }

            return View(departament);
        }

        // POST: Departaments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var departament = await _context.Departaments.FindAsync(id);
            _context.Departaments.Remove(departament);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartamentExists(int id)
        {
            return _context.Departaments.Any(e => e.DepartamentId == id);
        }
    }
}
