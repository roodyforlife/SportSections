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
    public class StudentSectionsController : Controller
    {
        private readonly DataBaseContext _context;

        public StudentSectionsController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: StudentSections
        public async Task<IActionResult> Index()
        {
            var dataBaseContext = _context.StudentSections.Include(s => s.Section).Include(s => s.Student);
            return View(await dataBaseContext.ToListAsync());
        }

        // GET: StudentSections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentSection = await _context.StudentSections
                .Include(s => s.Section)
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.StudentSectionId == id);
            if (studentSection == null)
            {
                return NotFound();
            }

            return View(studentSection);
        }

        // GET: StudentSections/Create
        public IActionResult Create()
        {
            ViewData["SectionId"] = new SelectList(_context.Sections, "SectionId", "Address");
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "Address");
            return View();
        }

        // POST: StudentSections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentSectionId,StudentId,SectionId")] StudentSection studentSection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentSection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SectionId"] = new SelectList(_context.Sections, "SectionId", "Address", studentSection.SectionId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "Address", studentSection.StudentId);
            return View(studentSection);
        }

        // GET: StudentSections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentSection = await _context.StudentSections.FindAsync(id);
            if (studentSection == null)
            {
                return NotFound();
            }
            ViewData["SectionId"] = new SelectList(_context.Sections, "SectionId", "Address", studentSection.SectionId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "Address", studentSection.StudentId);
            return View(studentSection);
        }

        // POST: StudentSections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentSectionId,StudentId,SectionId")] StudentSection studentSection)
        {
            if (id != studentSection.StudentSectionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentSection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentSectionExists(studentSection.StudentSectionId))
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
            ViewData["SectionId"] = new SelectList(_context.Sections, "SectionId", "Address", studentSection.SectionId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "Address", studentSection.StudentId);
            return View(studentSection);
        }

        // GET: StudentSections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentSection = await _context.StudentSections
                .Include(s => s.Section)
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.StudentSectionId == id);
            if (studentSection == null)
            {
                return NotFound();
            }

            return View(studentSection);
        }

        // POST: StudentSections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentSection = await _context.StudentSections.FindAsync(id);
            _context.StudentSections.Remove(studentSection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentSectionExists(int id)
        {
            return _context.StudentSections.Any(e => e.StudentSectionId == id);
        }
    }
}
