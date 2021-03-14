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
    public class RechnungsController : Controller
    {
        private readonly AutoDBContext _context;

        public RechnungsController(AutoDBContext context)
        {
            _context = context;
        }

        // GET: Rechnungs
        public async Task<IActionResult> Index()
        {
            var autoDBContext = _context.Rechnungs.Include(r => r.Kunde).Include(r => r.Rechnungfahrzeug).Include(r => r.Verkaeufer);
            return View(await autoDBContext.ToListAsync());
        }

        // GET: Rechnungs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rechnung = await _context.Rechnungs
                .Include(r => r.Kunde)
                .Include(r => r.Rechnungfahrzeug)
                .Include(r => r.Verkaeufer)
                .FirstOrDefaultAsync(m => m.RechnungId == id);
            if (rechnung == null)
            {
                return NotFound();
            }

            return View(rechnung);
        }

        // GET: Rechnungs/Create
        public IActionResult Create()
        {
            ViewData["KundeId"] = new SelectList(_context.Kundes, "KundeId", "Hn");
            ViewData["RechnungfahrzeugId"] = new SelectList(_context.Rechnungfahrzeugs, "RechnungfahrzeugId", "RechnungfahrzeugId");
            ViewData["VerkaeuferId"] = new SelectList(_context.Verkaeufers, "VerkaeuferId", "Nachname");
            return View();
        }

        // POST: Rechnungs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RechnungId,Rechnungsnummer,Datum,KundeId,VerkaeuferId,RechnungfahrzeugId")] Rechnung rechnung)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rechnung);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KundeId"] = new SelectList(_context.Kundes, "KundeId", "Hn", rechnung.KundeId);
            ViewData["RechnungfahrzeugId"] = new SelectList(_context.Rechnungfahrzeugs, "RechnungfahrzeugId", "RechnungfahrzeugId", rechnung.RechnungfahrzeugId);
            ViewData["VerkaeuferId"] = new SelectList(_context.Verkaeufers, "VerkaeuferId", "Nachname", rechnung.VerkaeuferId);
            return View(rechnung);
        }

        // GET: Rechnungs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rechnung = await _context.Rechnungs.FindAsync(id);
            if (rechnung == null)
            {
                return NotFound();
            }
            ViewData["KundeId"] = new SelectList(_context.Kundes, "KundeId", "Hn", rechnung.KundeId);
            ViewData["RechnungfahrzeugId"] = new SelectList(_context.Rechnungfahrzeugs, "RechnungfahrzeugId", "RechnungfahrzeugId", rechnung.RechnungfahrzeugId);
            ViewData["VerkaeuferId"] = new SelectList(_context.Verkaeufers, "VerkaeuferId", "Nachname", rechnung.VerkaeuferId);
            return View(rechnung);
        }

        // POST: Rechnungs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RechnungId,Rechnungsnummer,Datum,KundeId,VerkaeuferId,RechnungfahrzeugId")] Rechnung rechnung)
        {
            if (id != rechnung.RechnungId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rechnung);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RechnungExists(rechnung.RechnungId))
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
            ViewData["KundeId"] = new SelectList(_context.Kundes, "KundeId", "Hn", rechnung.KundeId);
            ViewData["RechnungfahrzeugId"] = new SelectList(_context.Rechnungfahrzeugs, "RechnungfahrzeugId", "RechnungfahrzeugId", rechnung.RechnungfahrzeugId);
            ViewData["VerkaeuferId"] = new SelectList(_context.Verkaeufers, "VerkaeuferId", "Nachname", rechnung.VerkaeuferId);
            return View(rechnung);
        }

        // GET: Rechnungs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rechnung = await _context.Rechnungs
                .Include(r => r.Kunde)
                .Include(r => r.Rechnungfahrzeug)
                .Include(r => r.Verkaeufer)
                .FirstOrDefaultAsync(m => m.RechnungId == id);
            if (rechnung == null)
            {
                return NotFound();
            }

            return View(rechnung);
        }

        // POST: Rechnungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rechnung = await _context.Rechnungs.FindAsync(id);
            _context.Rechnungs.Remove(rechnung);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RechnungExists(int id)
        {
            return _context.Rechnungs.Any(e => e.RechnungId == id);
        }
    }
}
