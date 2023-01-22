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
    public class TrainerSectionsController : Controller
    {
        private readonly DataBaseContext _context;

        public TrainerSectionsController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: TrainerSections
        public async Task<IActionResult> Index()
        {
            var dataBaseContext = _context.TrainerSections.Include(t => t.Section).Include(t => t.Trainer);
            return View(await dataBaseContext.ToListAsync());
        }

        // GET: TrainerSections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainerSection = await _context.TrainerSections
                .Include(t => t.Section)
                .Include(t => t.Trainer)
                .FirstOrDefaultAsync(m => m.TrainerSectionId == id);
            if (trainerSection == null)
            {
                return NotFound();
            }

            return View(trainerSection);
        }

        // GET: TrainerSections/Create
        public IActionResult Create()
        {
            ViewData["SectionId"] = new SelectList(_context.Sections, "SectionId", "Name");
            ViewData["TrainerId"] = new SelectList(_context.Trainers, "TrainerId", "Email");
            return View();
        }

        // POST: TrainerSections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrainerSectionId,SectionId,TrainerId")] TrainerSection trainerSection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainerSection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SectionId"] = new SelectList(_context.Sections, "SectionId", "Name", trainerSection.SectionId);
            ViewData["TrainerId"] = new SelectList(_context.Trainers, "TrainerId", "Email", trainerSection.TrainerId);
            return View(trainerSection);
        }

        // GET: TrainerSections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainerSection = await _context.TrainerSections.FindAsync(id);
            if (trainerSection == null)
            {
                return NotFound();
            }
            ViewData["SectionId"] = new SelectList(_context.Sections, "SectionId", "Name", trainerSection.SectionId);
            ViewData["TrainerId"] = new SelectList(_context.Trainers, "TrainerId", "Email", trainerSection.TrainerId);
            return View(trainerSection);
        }

        // POST: TrainerSections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrainerSectionId,SectionId,TrainerId")] TrainerSection trainerSection)
        {
            if (id != trainerSection.TrainerSectionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainerSection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainerSectionExists(trainerSection.TrainerSectionId))
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
            ViewData["SectionId"] = new SelectList(_context.Sections, "SectionId", "Name", trainerSection.SectionId);
            ViewData["TrainerId"] = new SelectList(_context.Trainers, "TrainerId", "Email", trainerSection.TrainerId);
            return View(trainerSection);
        }

        // GET: TrainerSections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainerSection = await _context.TrainerSections
                .Include(t => t.Section)
                .Include(t => t.Trainer)
                .FirstOrDefaultAsync(m => m.TrainerSectionId == id);
            if (trainerSection == null)
            {
                return NotFound();
            }

            return View(trainerSection);
        }

        // POST: TrainerSections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainerSection = await _context.TrainerSections.FindAsync(id);
            _context.TrainerSections.Remove(trainerSection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainerSectionExists(int id)
        {
            return _context.TrainerSections.Any(e => e.TrainerSectionId == id);
        }
    }
}
