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
    public class EmbarcacoesController : Controller
    {
        private readonly OracleDbContext _context;

        public EmbarcacoesController(OracleDbContext context)
        {
            _context = context;
        }

        // GET: Embarcacoes
        public async Task<IActionResult> Index()
        {
            var oracleDbContext = _context.Embarcacoes.Include(e => e.Proprietarios);
            return View(await oracleDbContext.ToListAsync());
        }

        // GET: Embarcacoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var embarcacoes = await _context.Embarcacoes
                .Include(e => e.Proprietarios)
                .FirstOrDefaultAsync(m => m.Id_Embarcacao == id);
            if (embarcacoes == null)
            {
                return NotFound();
            }

            return View(embarcacoes);
        }

        // GET: Embarcacoes/Create
        public IActionResult Create()
        {
            ViewData["Id_Proprietario"] = new SelectList(_context.Proprietarios, "Id_Proprietario", "Id_Proprietario");
            return View();
        }

        // POST: Embarcacoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Embarcacao,Nome,Tipo,Bandeira,Capacidade,Ano_Fabricação,Id_Proprietario")] Embarcacoes embarcacoes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(embarcacoes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_Proprietario"] = new SelectList(_context.Proprietarios, "Id_Proprietario", "Id_Proprietario", embarcacoes.Id_Proprietario);
            return View(embarcacoes);
        }

        // GET: Embarcacoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var embarcacoes = await _context.Embarcacoes.FindAsync(id);
            if (embarcacoes == null)
            {
                return NotFound();
            }
            ViewData["Id_Proprietario"] = new SelectList(_context.Proprietarios, "Id_Proprietario", "Id_Proprietario", embarcacoes.Id_Proprietario);
            return View(embarcacoes);
        }

        // POST: Embarcacoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Embarcacao,Nome,Tipo,Bandeira,Capacidade,Ano_Fabricação,Id_Proprietario")] Embarcacoes embarcacoes)
        {
            if (id != embarcacoes.Id_Embarcacao)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(embarcacoes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmbarcacoesExists(embarcacoes.Id_Embarcacao))
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
            ViewData["Id_Proprietario"] = new SelectList(_context.Proprietarios, "Id_Proprietario", "Id_Proprietario", embarcacoes.Id_Proprietario);
            return View(embarcacoes);
        }

        // GET: Embarcacoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var embarcacoes = await _context.Embarcacoes
                .Include(e => e.Proprietarios)
                .FirstOrDefaultAsync(m => m.Id_Embarcacao == id);
            if (embarcacoes == null)
            {
                return NotFound();
            }

            return View(embarcacoes);
        }

        // POST: Embarcacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var embarcacoes = await _context.Embarcacoes.FindAsync(id);
            if (embarcacoes != null)
            {
                _context.Embarcacoes.Remove(embarcacoes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmbarcacoesExists(int id)
        {
            return _context.Embarcacoes.Any(e => e.Id_Embarcacao == id);
        }
    }
}
