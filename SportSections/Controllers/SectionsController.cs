using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportSections.DataBase;
using SportSections.Enums;
using SportSections.Models;

namespace SportSections.Controllers
{
    public class SectionsController : Controller
    {
        private readonly DataBaseContext _context;

        public SectionsController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: Sections
        public async Task<IActionResult> Index(string name, string address, DateTime dateFrom, DateTime dateTo, SectionSort sort = SectionSort.NameAsc)
        {
            IQueryable<Section> sections = _context.Sections;

            if (!String.IsNullOrEmpty(name))
            {
                sections = sections.Where(x => x.Name.Contains(name));
            }

            if (!String.IsNullOrEmpty(address))
            {
                sections = sections.Where(x => x.Address.Contains(address));
            }

            if (dateTo.Year == 1)
            {
                dateTo = DateTime.Now.AddDays(1);
            }

            sections = sections.Where(x => x.StartDate >= dateFrom);
            sections = sections.Where(x => x.StartDate <= dateTo);

            switch (sort)
            {
                case SectionSort.NameDesc:
                    sections = sections.OrderByDescending(x => x.Name);
                    break;
                case SectionSort.AddressAsc:
                    sections = sections.OrderBy(x => x.Address);
                    break;
                case SectionSort.AddressDesc:
                    sections = sections.OrderByDescending(x => x.Address);
                    break;
                case SectionSort.FloorAsc:
                    sections = sections.OrderBy(x => x.Floor);
                    break;
                default:
                    sections = sections.OrderBy(x => x.Name);
                    break;
            }

            ViewBag.Sort = (List<SelectListItem>)Enum.GetValues(typeof(SectionSort)).Cast<SectionSort>()
                .Select(x => new SelectListItem
                {
                    Text = x.GetType()
            .GetMember(x.ToString())
            .FirstOrDefault()
            .GetCustomAttribute<DisplayAttribute>()?
            .GetName(),
                    Value = x.ToString(),
                    Selected = (x == sort)
                }).ToList();

            ViewBag.Name = name;
            ViewBag.Address = address;
            ViewBag.DateFrom = dateFrom;
            ViewBag.DateTo = dateTo;

            return View(await sections.ToListAsync());
        }

        // GET: Sections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var section = await _context.Sections
                .Include(x => x.StudentSections)
                .ThenInclude(x => x.Student)
                .ThenInclude(x => x.Group)
                .FirstOrDefaultAsync(m => m.SectionId == id);
            if (section == null)
            {
                return NotFound();
            }

            return View(section);
        }

        // GET: Sections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SectionId,Name,Address,Floor,StartDate,FinishDate")] Section section)
        {
            if (ModelState.IsValid)
            {
                _context.Add(section);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(section);
        }

        // GET: Sections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var section = await _context.Sections.FindAsync(id);
            if (section == null)
            {
                return NotFound();
            }
            return View(section);
        }

        // POST: Sections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SectionId,Name,Address,Floor,StartDate,FinishDate")] Section section)
        {
            if (id != section.SectionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(section);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SectionExists(section.SectionId))
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
            return View(section);
        }

        // GET: Sections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var section = await _context.Sections
                .FirstOrDefaultAsync(m => m.SectionId == id);
            if (section == null)
            {
                return NotFound();
            }

            return View(section);
        }

        // POST: Sections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var section = await _context.Sections.FindAsync(id);
            _context.Sections.Remove(section);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SectionExists(int id)
        {
            return _context.Sections.Any(e => e.SectionId == id);
        }

        public async Task<IActionResult> Automation(int? id)
        {
            Section section = await _context.Sections
                .Include(x => x.StudentSections)
                .ThenInclude(x => x.Student)
                .Include(x => x.TrainerSections)
                .FirstOrDefaultAsync(x => x.SectionId == id);

            if (section.StudentSections.Count() < 2)
            {
                return RedirectToAction(nameof(Index));
            }

            List<StudentSection> studentSections = section.StudentSections.Skip((int)(section.StudentSections.Count() / 2)).ToList();
            _context.RemoveRange(studentSections);
            await _context.SaveChangesAsync();
            foreach (StudentSection item in studentSections)
            {
                item.StudentSectionId = 0;
            }

            _context.Update(section);
            await _context.AddRangeAsync(studentSections);

            Section newSection = new Section()
            {
                Name = section.Name + "2",
                Address = section.Address,
                Floor = section.Floor,
                StartDate = section.StartDate,
                FinishDate = section.FinishDate,
                TrainerSections = section.TrainerSections,
                StudentSections = studentSections
            };

            _context.Add(newSection);
            _context.AddRange(newSection.StudentSections);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
