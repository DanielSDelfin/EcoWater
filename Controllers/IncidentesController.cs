using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EcoWater.Models;
using EcoWater.Persistence;

namespace EcoWater.Controllers
{
    public class IncidentesController : Controller
    {
        private readonly OracleDbContext _context;

        public IncidentesController(OracleDbContext context)
        {
            _context = context;
        }

        // GET: Incidentes
        public async Task<IActionResult> Index()
        {
            var oracleDbContext = _context.Incidentes.Include(i => i.Embarcacoes);
            return View(await oracleDbContext.ToListAsync());
        }

        // GET: Incidentes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incidentes = await _context.Incidentes
                .Include(i => i.Embarcacoes)
                .FirstOrDefaultAsync(m => m.Id_Incidente == id);
            if (incidentes == null)
            {
                return NotFound();
            }

            return View(incidentes);
        }

        // GET: Incidentes/Create
        public IActionResult Create()
        {
            ViewData["Id_Embarcacao"] = new SelectList(_context.Embarcacoes, "Id_Embarcacao", "Id_Embarcacao");
            return View();
        }

        // POST: Incidentes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Incidente,Id_Embarcacao,Data,Descricao,Tipo_Poluicao,Severidade")] Incidentes incidentes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(incidentes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_Embarcacao"] = new SelectList(_context.Embarcacoes, "Id_Embarcacao", "Id_Embarcacao", incidentes.Id_Embarcacao);
            return View(incidentes);
        }

        // GET: Incidentes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incidentes = await _context.Incidentes.FindAsync(id);
            if (incidentes == null)
            {
                return NotFound();
            }
            ViewData["Id_Embarcacao"] = new SelectList(_context.Embarcacoes, "Id_Embarcacao", "Id_Embarcacao", incidentes.Id_Embarcacao);
            return View(incidentes);
        }

        // POST: Incidentes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Incidente,Id_Embarcacao,Data,Descricao,Tipo_Poluicao,Severidade")] Incidentes incidentes)
        {
            if (id != incidentes.Id_Incidente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(incidentes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IncidentesExists(incidentes.Id_Incidente))
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
            ViewData["Id_Embarcacao"] = new SelectList(_context.Embarcacoes, "Id_Embarcacao", "Id_Embarcacao", incidentes.Id_Embarcacao);
            return View(incidentes);
        }

        // GET: Incidentes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incidentes = await _context.Incidentes
                .Include(i => i.Embarcacoes)
                .FirstOrDefaultAsync(m => m.Id_Incidente == id);
            if (incidentes == null)
            {
                return NotFound();
            }

            return View(incidentes);
        }

        // POST: Incidentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var incidentes = await _context.Incidentes.FindAsync(id);
            if (incidentes != null)
            {
                _context.Incidentes.Remove(incidentes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IncidentesExists(int id)
        {
            return _context.Incidentes.Any(e => e.Id_Incidente == id);
        }
    }
}
