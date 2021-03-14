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
    public class RechnungfahrzeugsController : Controller
    {
        private readonly AutoDBContext _context;

        public RechnungfahrzeugsController(AutoDBContext context)
        {
            _context = context;
        }

        // GET: Rechnungfahrzeugs
        public async Task<IActionResult> Index()
        {
            var autoDBContext = _context.Rechnungfahrzeugs.Include(r => r.Fahrzeug);
            return View(await autoDBContext.ToListAsync());
        }

        // GET: Rechnungfahrzeugs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rechnungfahrzeug = await _context.Rechnungfahrzeugs
                .Include(r => r.Fahrzeug)
                .FirstOrDefaultAsync(m => m.RechnungfahrzeugId == id);
            if (rechnungfahrzeug == null)
            {
                return NotFound();
            }

            return View(rechnungfahrzeug);
        }

        // GET: Rechnungfahrzeugs/Create
        public IActionResult Create()
        {
            ViewData["FahrzeugId"] = new SelectList(_context.Fahrzeugs, "FahrzeugId", "Bezeichnung");
            return View();
        }

        // POST: Rechnungfahrzeugs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RechnungfahrzeugId,Menge,FahrzeugId,PreisBeiRechnungserstellung")] Rechnungfahrzeug rechnungfahrzeug)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rechnungfahrzeug);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FahrzeugId"] = new SelectList(_context.Fahrzeugs, "FahrzeugId", "Bezeichnung", rechnungfahrzeug.FahrzeugId);
            return View(rechnungfahrzeug);
        }

        // GET: Rechnungfahrzeugs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rechnungfahrzeug = await _context.Rechnungfahrzeugs.FindAsync(id);
            if (rechnungfahrzeug == null)
            {
                return NotFound();
            }
            ViewData["FahrzeugId"] = new SelectList(_context.Fahrzeugs, "FahrzeugId", "Bezeichnung", rechnungfahrzeug.FahrzeugId);
            return View(rechnungfahrzeug);
        }

        // POST: Rechnungfahrzeugs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RechnungfahrzeugId,Menge,FahrzeugId,PreisBeiRechnungserstellung")] Rechnungfahrzeug rechnungfahrzeug)
        {
            if (id != rechnungfahrzeug.RechnungfahrzeugId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rechnungfahrzeug);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RechnungfahrzeugExists(rechnungfahrzeug.RechnungfahrzeugId))
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
            ViewData["FahrzeugId"] = new SelectList(_context.Fahrzeugs, "FahrzeugId", "Bezeichnung", rechnungfahrzeug.FahrzeugId);
            return View(rechnungfahrzeug);
        }

        // GET: Rechnungfahrzeugs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rechnungfahrzeug = await _context.Rechnungfahrzeugs
                .Include(r => r.Fahrzeug)
                .FirstOrDefaultAsync(m => m.RechnungfahrzeugId == id);
            if (rechnungfahrzeug == null)
            {
                return NotFound();
            }

            return View(rechnungfahrzeug);
        }

        // POST: Rechnungfahrzeugs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rechnungfahrzeug = await _context.Rechnungfahrzeugs.FindAsync(id);
            _context.Rechnungfahrzeugs.Remove(rechnungfahrzeug);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RechnungfahrzeugExists(int id)
        {
            return _context.Rechnungfahrzeugs.Any(e => e.RechnungfahrzeugId == id);
        }
    }
}
