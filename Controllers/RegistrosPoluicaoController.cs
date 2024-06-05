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
    public class RegistrosPoluicaoController : Controller
    {
        private readonly OracleDbContext _context;

        public RegistrosPoluicaoController(OracleDbContext context)
        {
            _context = context;
        }

        // GET: RegistrosPoluicao
        public async Task<IActionResult> Index()
        {
            var oracleDbContext = _context.RegistrosPoluicao.Include(r => r.Embarcacoes);
            return View(await oracleDbContext.ToListAsync());
        }

        // GET: RegistrosPoluicao/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registrosPoluicao = await _context.RegistrosPoluicao
                .Include(r => r.Embarcacoes)
                .FirstOrDefaultAsync(m => m.Id_Registro == id);
            if (registrosPoluicao == null)
            {
                return NotFound();
            }

            return View(registrosPoluicao);
        }

        // GET: RegistrosPoluicao/Create
        public IActionResult Create()
        {
            ViewData["Id_Embarcacao"] = new SelectList(_context.Embarcacoes, "Id_Embarcacao", "Id_Embarcacao");
            return View();
        }

        // POST: RegistrosPoluicao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Registro,Id_Embarcacao,Data,Hora,Localizacao,Tipo_Poluicao,Quantidade_Poluida,Testemunhas")] RegistrosPoluicao registrosPoluicao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registrosPoluicao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_Embarcacao"] = new SelectList(_context.Embarcacoes, "Id_Embarcacao", "Id_Embarcacao", registrosPoluicao.Id_Embarcacao);
            return View(registrosPoluicao);
        }

        // GET: RegistrosPoluicao/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registrosPoluicao = await _context.RegistrosPoluicao.FindAsync(id);
            if (registrosPoluicao == null)
            {
                return NotFound();
            }
            ViewData["Id_Embarcacao"] = new SelectList(_context.Embarcacoes, "Id_Embarcacao", "Id_Embarcacao", registrosPoluicao.Id_Embarcacao);
            return View(registrosPoluicao);
        }

        // POST: RegistrosPoluicao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id_Registro,Id_Embarcacao,Data,Hora,Localizacao,Tipo_Poluicao,Quantidade_Poluida,Testemunhas")] RegistrosPoluicao registrosPoluicao)
        {
            if (id != registrosPoluicao.Id_Registro)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registrosPoluicao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistrosPoluicaoExists(registrosPoluicao.Id_Registro))
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
            ViewData["Id_Embarcacao"] = new SelectList(_context.Embarcacoes, "Id_Embarcacao", "Id_Embarcacao", registrosPoluicao.Id_Embarcacao);
            return View(registrosPoluicao);
        }

        // GET: RegistrosPoluicao/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registrosPoluicao = await _context.RegistrosPoluicao
                .Include(r => r.Embarcacoes)
                .FirstOrDefaultAsync(m => m.Id_Registro == id);
            if (registrosPoluicao == null)
            {
                return NotFound();
            }

            return View(registrosPoluicao);
        }

        // POST: RegistrosPoluicao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var registrosPoluicao = await _context.RegistrosPoluicao.FindAsync(id);
            if (registrosPoluicao != null)
            {
                _context.RegistrosPoluicao.Remove(registrosPoluicao);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistrosPoluicaoExists(string id)
        {
            return _context.RegistrosPoluicao.Any(e => e.Id_Registro == id);
        }
    }
}
