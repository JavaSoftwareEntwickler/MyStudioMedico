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
    public class AppuntamentiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppuntamentiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Appuntamenti
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Appuntamento.Include(a => a.Dottore);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Appuntamenti/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appuntamento = await _context.Appuntamento
                .Include(a => a.Dottore)
                .FirstOrDefaultAsync(m => m.AppuntamentoID == id);
            if (appuntamento == null)
            {
                return NotFound();
            }

            return View(appuntamento);
        }

        // GET: Appuntamenti/Create
        public IActionResult Create()
        {
            List<Dottore> dottori = GetDottoriConNomeDott();
            //var dottori = _context.Dottore;
            ViewData["DottoreID"] = new SelectList(dottori, "DottoreID", "NomeDott");
            return View();
        }

        // POST: Appuntamenti/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppuntamentoID,Data,Nome,Cognome,DottoreID")] Appuntamento appuntamento)
        {
            List<Dottore> dottori = GetDottoriConNomeDott();

            var appuntamenti = _context.Appuntamento;
            if (ModelState.IsValid)
            {

                foreach (var app in appuntamenti)
                {
                    if (app.Data == appuntamento.Data)
                    {
                        ViewBag.messInserimento = "Data / Orario non disponibile ...riprovare";
                        ViewData["DottoreID"] = new SelectList(dottori, "DottoreID", "NomeDott", appuntamento.DottoreID);
                        return View(appuntamento);
                    }
                }
                _context.Add(appuntamento);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            ViewBag.messInserimento = "";

            ViewData["DottoreID"] = new SelectList(dottori, "DottoreID", "NomeDott", appuntamento.DottoreID);
            return View(appuntamento);
        }

        private List<Dottore> GetDottoriConNomeDott()
        {
            var db = _context.Dottore;
            List<Dottore> dottori = new();

            foreach (var d in db)
            {
                dottori.Add(new Dottore
                {
                    DottoreID = d.DottoreID,
                    NomeDott = d.Nome + " " + d.Cognome
                });
            }

            return dottori;
        }

        // GET: Appuntamenti/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            List<Dottore> dottori = GetDottoriConNomeDott();
            var appuntamento = await _context.Appuntamento.FindAsync(id);
            if (appuntamento == null)
            {
                return NotFound();
            }
            ViewData["DottoreID"] = new SelectList(dottori, "DottoreID", "NomeDott", appuntamento.DottoreID);
            return View(appuntamento);
        }

        // POST: Appuntamenti/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppuntamentoID,Data,Nome,Cognome,DottoreID")] Appuntamento appuntamento)
        {
            if (id != appuntamento.AppuntamentoID)
            {
                return NotFound();
            }
            List<Dottore> dottori = GetDottoriConNomeDott();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appuntamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppuntamentoExists(appuntamento.AppuntamentoID))
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
            ViewData["DottoreID"] = new SelectList(dottori, "DottoreID", "NomeDott", appuntamento.DottoreID);
            return View(appuntamento);
        }

        // GET: Appuntamenti/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appuntamento = await _context.Appuntamento
                .Include(a => a.Dottore)
                .FirstOrDefaultAsync(m => m.AppuntamentoID == id);
            if (appuntamento == null)
            {
                return NotFound();
            }

            return View(appuntamento);
        }

        // POST: Appuntamenti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appuntamento = await _context.Appuntamento.FindAsync(id);
            _context.Appuntamento.Remove(appuntamento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppuntamentoExists(int id)
        {
            return _context.Appuntamento.Any(e => e.AppuntamentoID == id);
        }
    }
}
