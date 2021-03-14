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
    public class VerkaeufersController : Controller
    {
        private readonly AutoDBContext _context;

        public VerkaeufersController(AutoDBContext context)
        {
            _context = context;
        }

        // GET: Verkaeufers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Verkaeufers.ToListAsync());
        }

        // GET: Verkaeufers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var verkaeufer = await _context.Verkaeufers
                .FirstOrDefaultAsync(m => m.VerkaeuferId == id);
            if (verkaeufer == null)
            {
                return NotFound();
            }

            return View(verkaeufer);
        }

        // GET: Verkaeufers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Verkaeufers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VerkaeuferId,Vorname,Nachname")] Verkaeufer verkaeufer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(verkaeufer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(verkaeufer);
        }

        // GET: Verkaeufers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var verkaeufer = await _context.Verkaeufers.FindAsync(id);
            if (verkaeufer == null)
            {
                return NotFound();
            }
            return View(verkaeufer);
        }

        // POST: Verkaeufers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VerkaeuferId,Vorname,Nachname")] Verkaeufer verkaeufer)
        {
            if (id != verkaeufer.VerkaeuferId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(verkaeufer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VerkaeuferExists(verkaeufer.VerkaeuferId))
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
            return View(verkaeufer);
        }

        // GET: Verkaeufers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var verkaeufer = await _context.Verkaeufers
                .FirstOrDefaultAsync(m => m.VerkaeuferId == id);
            if (verkaeufer == null)
            {
                return NotFound();
            }

            return View(verkaeufer);
        }

        // POST: Verkaeufers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var verkaeufer = await _context.Verkaeufers.FindAsync(id);
            _context.Verkaeufers.Remove(verkaeufer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VerkaeuferExists(int id)
        {
            return _context.Verkaeufers.Any(e => e.VerkaeuferId == id);
        }
    }
}
