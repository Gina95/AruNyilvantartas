using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AruNyilvantartas.Data;
using AruNyilvantartas.Models;
using Microsoft.AspNetCore.Authorization;

namespace AruNyilvantartas.Controllers
{
    public class AruController : Controller
    {
        private readonly ApplicationDbContext _context;
     

        public AruController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Aru
        public async Task<IActionResult> Index(string keresElnevezes, string keresKategoria)
        {
            var aruk = _context.Aru.Select(x=>x);
            if (!string.IsNullOrEmpty(keresElnevezes))
            {
                aruk = aruk.Where(aru => aru.Elnevezes.Contains(keresElnevezes));
            }

            if (!string.IsNullOrEmpty(keresKategoria))
            {
                aruk = aruk.Where(aru => aru.Kategoria.Equals(keresKategoria));
            }

            var Elnevezes = _context.Aru.Select(Aru => Aru.Elnevezes).Distinct();
            var Kategoria = _context.Aru.Select(Aru => Aru.Kategoria).Distinct();

            AruKeres modelkereseshez = new AruKeres();
            modelkereseshez.Kategoria = new SelectList(await Kategoria.ToListAsync());
            modelkereseshez.Elnevezes = new SelectList(await Elnevezes.ToListAsync());
            modelkereseshez.keresElnevezes = keresElnevezes;
            modelkereseshez.nyilvantartas = await aruk.ToListAsync();




            return View(modelkereseshez);
        }

        // GET: Aru/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aru = await _context.Aru
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aru == null)
            {
                return NotFound();
            }

            return View(aru);
        }

        // GET: Aru/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Aru/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Elnevezes,Kategoria,CsomagolasiEgyseg,DarabSzam")] Aru aru)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aru);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aru);
        }

        // GET: Aru/Edit/5

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aru = await _context.Aru.FindAsync(id);
            if (aru == null)
            {
                return NotFound();
            }
            return View(aru);
        }

        // POST: Aru/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Elnevezes,Kategoria,CsomagolasiEgyseg,DarabSzam")] Aru aru)
        {
            if (id != aru.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aru);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AruExists(aru.Id))
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
            return View(aru);
        }

        // GET: Aru/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aru = await _context.Aru
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aru == null)
            {
                return NotFound();
            }

            return View(aru);
        }

        // POST: Aru/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aru = await _context.Aru.FindAsync(id);
            _context.Aru.Remove(aru);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AruExists(int id)
        {
            return _context.Aru.Any(e => e.Id == id);
        }
    }
}
