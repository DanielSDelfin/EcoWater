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
    public class ProprietariosController : Controller
    {
        private readonly OracleDbContext _context;

        public ProprietariosController(OracleDbContext context)
        {
            _context = context;
        }

        // GET: Proprietarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Proprietarios.ToListAsync());
        }

        // GET: Proprietarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proprietarios = await _context.Proprietarios
                .FirstOrDefaultAsync(m => m.Id_Proprietario == id);
            if (proprietarios == null)
            {
                return NotFound();
            }

            return View(proprietarios);
        }

        // GET: Proprietarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Proprietarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Proprietario,Nome,Endereco,Telefone,Email")] Proprietarios proprietarios)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proprietarios);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proprietarios);
        }

        // GET: Proprietarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proprietarios = await _context.Proprietarios.FindAsync(id);
            if (proprietarios == null)
            {
                return NotFound();
            }
            return View(proprietarios);
        }

        // POST: Proprietarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Proprietario,Nome,Endereco,Telefone,Email")] Proprietarios proprietarios)
        {
            if (id != proprietarios.Id_Proprietario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proprietarios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProprietariosExists(proprietarios.Id_Proprietario))
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
            return View(proprietarios);
        }

        // GET: Proprietarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proprietarios = await _context.Proprietarios
                .FirstOrDefaultAsync(m => m.Id_Proprietario == id);
            if (proprietarios == null)
            {
                return NotFound();
            }

            return View(proprietarios);
        }

        // POST: Proprietarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proprietarios = await _context.Proprietarios.FindAsync(id);
            if (proprietarios != null)
            {
                _context.Proprietarios.Remove(proprietarios);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProprietariosExists(int id)
        {
            return _context.Proprietarios.Any(e => e.Id_Proprietario == id);
        }
    }
}
