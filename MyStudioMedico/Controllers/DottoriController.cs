using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyStudioMedico.Data;
using MyStudioMedico.Models;

namespace MyStudioMedico.Controllers
{
    public class DottoriController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DottoriController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dottori
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dottore.ToListAsync());
        }

        // GET: Dottori/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dottore = await _context.Dottore
                .FirstOrDefaultAsync(m => m.DottoreID == id);
            if (dottore == null)
            {
                return NotFound();
            }

            return View(dottore);
        }

        // GET: Dottori/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dottori/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DottoreID,Nome,Cognome")] Dottore dottore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dottore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dottore);
        }

        // GET: Dottori/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dottore = await _context.Dottore.FindAsync(id);
            if (dottore == null)
            {
                return NotFound();
            }
            return View(dottore);
        }

        // POST: Dottori/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DottoreID,Nome,Cognome")] Dottore dottore)
        {
            if (id != dottore.DottoreID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dottore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DottoreExists(dottore.DottoreID))
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
            return View(dottore);
        }

        // GET: Dottori/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dottore = await _context.Dottore
                .FirstOrDefaultAsync(m => m.DottoreID == id);
            if (dottore == null)
            {
                return NotFound();
            }

            return View(dottore);
        }

        // POST: Dottori/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dottore = await _context.Dottore.FindAsync(id);
            _context.Dottore.Remove(dottore);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DottoreExists(int id)
        {
            return _context.Dottore.Any(e => e.DottoreID == id);
        }
    }
}
