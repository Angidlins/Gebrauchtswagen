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
    public class KundesController : Controller
    {
        private readonly AutoDBContext _context;

        public KundesController(AutoDBContext context)
        {
            _context = context;
        }

        // GET: Kundes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kundes.ToListAsync());
        }

        // GET: Kundes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kunde = await _context.Kundes
                .FirstOrDefaultAsync(m => m.KundeId == id);
            if (kunde == null)
            {
                return NotFound();
            }

            return View(kunde);
        }

        // GET: Kundes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kundes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KundeId,Vorname,Nachname,Ort,Plz,Strasse,Hn")] Kunde kunde)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kunde);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kunde);
        }

        // GET: Kundes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kunde = await _context.Kundes.FindAsync(id);
            if (kunde == null)
            {
                return NotFound();
            }
            return View(kunde);
        }

        // POST: Kundes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KundeId,Vorname,Nachname,Ort,Plz,Strasse,Hn")] Kunde kunde)
        {
            if (id != kunde.KundeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kunde);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KundeExists(kunde.KundeId))
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
            return View(kunde);
        }

        // GET: Kundes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kunde = await _context.Kundes
                .FirstOrDefaultAsync(m => m.KundeId == id);
            if (kunde == null)
            {
                return NotFound();
            }

            return View(kunde);
        }

        // POST: Kundes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kunde = await _context.Kundes.FindAsync(id);
            _context.Kundes.Remove(kunde);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KundeExists(int id)
        {
            return _context.Kundes.Any(e => e.KundeId == id);
        }
    }
}
