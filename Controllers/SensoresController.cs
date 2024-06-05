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
    public class SensoresController : Controller
    {
        private readonly OracleDbContext _context;

        public SensoresController(OracleDbContext context)
        {
            _context = context;
        }

        // GET: Sensores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sensores.ToListAsync());
        }

        // GET: Sensores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sensores = await _context.Sensores
                .FirstOrDefaultAsync(m => m.Id_Sensor == id);
            if (sensores == null)
            {
                return NotFound();
            }

            return View(sensores);
        }

        // GET: Sensores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sensores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Sensor,Tipo,Localizacao,Data_Instalacao,Status")] Sensores sensores)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sensores);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sensores);
        }

        // GET: Sensores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sensores = await _context.Sensores.FindAsync(id);
            if (sensores == null)
            {
                return NotFound();
            }
            return View(sensores);
        }

        // POST: Sensores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Sensor,Tipo,Localizacao,Data_Instalacao,Status")] Sensores sensores)
        {
            if (id != sensores.Id_Sensor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sensores);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SensoresExists(sensores.Id_Sensor))
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
            return View(sensores);
        }

        // GET: Sensores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sensores = await _context.Sensores
                .FirstOrDefaultAsync(m => m.Id_Sensor == id);
            if (sensores == null)
            {
                return NotFound();
            }

            return View(sensores);
        }

        // POST: Sensores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sensores = await _context.Sensores.FindAsync(id);
            if (sensores != null)
            {
                _context.Sensores.Remove(sensores);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SensoresExists(int id)
        {
            return _context.Sensores.Any(e => e.Id_Sensor == id);
        }
    }
}
