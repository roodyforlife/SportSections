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
    public class TrainersController : Controller
    {
        private readonly DataBaseContext _context;

        public TrainersController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: Trainers
        public async Task<IActionResult> Index(string email, string name, DateTime birthdayFrom, DateTime birthdayTo, TrainerSort sort = TrainerSort.EmailAsc)
        {
            IQueryable<Trainer> trainers = _context.Trainers;

            if (!String.IsNullOrEmpty(email))
            {
                trainers = trainers.Where(x => x.Email.Contains(email));
            }

            if (!String.IsNullOrEmpty(name))
            {
                trainers = trainers.Where(x => x.Name.Contains(name) || x.Surname.Contains(name) || x.Patronymic.Contains(name));
            }

            if (birthdayTo.Year == 1)
            {
                birthdayTo = DateTime.Now.AddDays(1);
            }

            trainers = trainers.Where(x => x.Birthday >= birthdayFrom);
            trainers = trainers.Where(x => x.Birthday <= birthdayTo);

            switch (sort)
            {
                case TrainerSort.NameAsc:
                    trainers = trainers.OrderBy(x => x.Name);
                    break;
                case TrainerSort.SurnameAsc:
                    trainers = trainers.OrderBy(x => x.Surname);
                    break;
                case TrainerSort.PatronymicAsc:
                    trainers = trainers.OrderBy(x => x.Patronymic);
                    break;
                case TrainerSort.EmailDesc:
                    trainers = trainers.OrderByDescending(x => x.Email);
                    break;
                case TrainerSort.ExperienceAsc:
                    trainers = trainers.OrderBy(x => x.Experience);
                    break;
                default:
                    trainers = trainers.OrderBy(x => x.Email);
                    break;
            }

            ViewBag.Sort = (List<SelectListItem>)Enum.GetValues(typeof(TrainerSort)).Cast<TrainerSort>()
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
            ViewBag.Email = email;
            ViewBag.BirthdayFrom = birthdayFrom;
            ViewBag.BirthdayTo = birthdayTo;
            return View(await trainers.ToListAsync());
        }

        // GET: Trainers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainer = await _context.Trainers
                .FirstOrDefaultAsync(m => m.TrainerId == id);
            if (trainer == null)
            {
                return NotFound();
            }

            return View(trainer);
        }

        // GET: Trainers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trainers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrainerId,Name,Surname,Patronymic,Birthday,Phone,Email,Address,AdmissionDate,Experience")] Trainer trainer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trainer);
        }

        // GET: Trainers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainer = await _context.Trainers.FindAsync(id);
            if (trainer == null)
            {
                return NotFound();
            }
            return View(trainer);
        }

        // POST: Trainers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrainerId,Name,Surname,Patronymic,Birthday,Phone,Email,Address,AdmissionDate,Experience")] Trainer trainer)
        {
            if (id != trainer.TrainerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainerExists(trainer.TrainerId))
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
            return View(trainer);
        }

        // GET: Trainers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainer = await _context.Trainers
                .FirstOrDefaultAsync(m => m.TrainerId == id);
            if (trainer == null)
            {
                return NotFound();
            }

            return View(trainer);
        }

        // POST: Trainers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainer = await _context.Trainers.FindAsync(id);
            _context.Trainers.Remove(trainer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainerExists(int id)
        {
            return _context.Trainers.Any(e => e.TrainerId == id);
        }
    }
}
