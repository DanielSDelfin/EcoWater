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
    public class MonitoramentosController : Controller
    {
        private readonly OracleDbContext _context;

        public MonitoramentosController(OracleDbContext context)
        {
            _context = context;
        }

        // GET: Monitoramentos
        public async Task<IActionResult> Index()
        {
            var oracleDbContext = _context.Monitoramentos.Include(m => m.Embarcacoes).Include(m => m.Sensores);
            return View(await oracleDbContext.ToListAsync());
        }

        // GET: Monitoramentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monitoramentos = await _context.Monitoramentos
                .Include(m => m.Embarcacoes)
                .Include(m => m.Sensores)
                .FirstOrDefaultAsync(m => m.Id_Monitoramento == id);
            if (monitoramentos == null)
            {
                return NotFound();
            }

            return View(monitoramentos);
        }

        // GET: Monitoramentos/Create
        public IActionResult Create()
        {
            ViewData["Id_Embarcacao"] = new SelectList(_context.Embarcacoes, "Id_Embarcacao", "Id_Embarcacao");
            ViewData["Id_Sensor"] = new SelectList(_context.Sensores, "Id_Sensor", "Id_Sensor");
            return View();
        }

        // POST: Monitoramentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Monitoramento,Id_Embarcacao,Id_Sensor,Data,Hora,Localizacao,Nivel_Poluicao")] Monitoramentos monitoramentos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(monitoramentos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_Embarcacao"] = new SelectList(_context.Embarcacoes, "Id_Embarcacao", "Id_Embarcacao", monitoramentos.Id_Embarcacao);
            ViewData["Id_Sensor"] = new SelectList(_context.Sensores, "Id_Sensor", "Id_Sensor", monitoramentos.Id_Sensor);
            return View(monitoramentos);
        }

        // GET: Monitoramentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monitoramentos = await _context.Monitoramentos.FindAsync(id);
            if (monitoramentos == null)
            {
                return NotFound();
            }
            ViewData["Id_Embarcacao"] = new SelectList(_context.Embarcacoes, "Id_Embarcacao", "Id_Embarcacao", monitoramentos.Id_Embarcacao);
            ViewData["Id_Sensor"] = new SelectList(_context.Sensores, "Id_Sensor", "Id_Sensor", monitoramentos.Id_Sensor);
            return View(monitoramentos);
        }

        // POST: Monitoramentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Monitoramento,Id_Embarcacao,Id_Sensor,Data,Hora,Localizacao,Nivel_Poluicao")] Monitoramentos monitoramentos)
        {
            if (id != monitoramentos.Id_Monitoramento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(monitoramentos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MonitoramentosExists(monitoramentos.Id_Monitoramento))
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
            ViewData["Id_Embarcacao"] = new SelectList(_context.Embarcacoes, "Id_Embarcacao", "Id_Embarcacao", monitoramentos.Id_Embarcacao);
            ViewData["Id_Sensor"] = new SelectList(_context.Sensores, "Id_Sensor", "Id_Sensor", monitoramentos.Id_Sensor);
            return View(monitoramentos);
        }

        // GET: Monitoramentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monitoramentos = await _context.Monitoramentos
                .Include(m => m.Embarcacoes)
                .Include(m => m.Sensores)
                .FirstOrDefaultAsync(m => m.Id_Monitoramento == id);
            if (monitoramentos == null)
            {
                return NotFound();
            }

            return View(monitoramentos);
        }

        // POST: Monitoramentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var monitoramentos = await _context.Monitoramentos.FindAsync(id);
            if (monitoramentos != null)
            {
                _context.Monitoramentos.Remove(monitoramentos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MonitoramentosExists(int id)
        {
            return _context.Monitoramentos.Any(e => e.Id_Monitoramento == id);
        }
    }
}
