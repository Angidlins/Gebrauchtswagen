using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gebrauchtswagen.Models;

namespace Gebrauchtswagen.Controllers
{
    public class FahrzeugsController : Controller
    {
        private readonly AutoDBContext _context;

        public FahrzeugsController(AutoDBContext context)
        {
            _context = context;
        }

        // GET: Fahrzeugs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fahrzeugs.ToListAsync());
        }

        // GET: Fahrzeugs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fahrzeug = await _context.Fahrzeugs
                .FirstOrDefaultAsync(m => m.FahrzeugId == id);
            if (fahrzeug == null)
            {
                return NotFound();
            }

            return View(fahrzeug);
        }

        // GET: Fahrzeugs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fahrzeugs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FahrzeugId,Preis,Zustand,Bezeichnung")] Fahrzeug fahrzeug)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fahrzeug);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fahrzeug);
        }

        // GET: Fahrzeugs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fahrzeug = await _context.Fahrzeugs.FindAsync(id);
            if (fahrzeug == null)
            {
                return NotFound();
            }
            return View(fahrzeug);
        }

        // POST: Fahrzeugs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FahrzeugId,Preis,Zustand,Bezeichnung")] Fahrzeug fahrzeug)
        {
            if (id != fahrzeug.FahrzeugId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fahrzeug);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FahrzeugExists(fahrzeug.FahrzeugId))
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
            return View(fahrzeug);
        }

        // GET: Fahrzeugs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fahrzeug = await _context.Fahrzeugs
                .FirstOrDefaultAsync(m => m.FahrzeugId == id);
            if (fahrzeug == null)
            {
                return NotFound();
            }

            return View(fahrzeug);
        }

        // POST: Fahrzeugs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fahrzeug = await _context.Fahrzeugs.FindAsync(id);
            _context.Fahrzeugs.Remove(fahrzeug);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FahrzeugExists(int id)
        {
            return _context.Fahrzeugs.Any(e => e.FahrzeugId == id);
        }
    }
}
