using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AchadosApi.Models;

namespace AchadosApi.Controllers
{
    public class ObservacoesController : Controller
    {
        private readonly Contexto _context;

        public ObservacoesController(Contexto context)
        {
            _context = context;
        }

        // GET: Observacoes
        public async Task<IActionResult> Index()
        {
            var contexto = _context.Observações.Include(o => o.Usuario);
            return View(await contexto.ToListAsync());
        }

        // GET: Observacoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Observações == null)
            {
                return NotFound();
            }

            var observacoes = await _context.Observações
                .Include(o => o.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (observacoes == null)
            {
                return NotFound();
            }

            return View(observacoes);
        }

        // GET: Observacoes/Create
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "UsuarioNome");
            ViewData["AnimalId"] = new SelectList(_context.Animais, "Id", "AnimalNome");
            return View();
        }

        // POST: Observacoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ObservacaoDescricao,ObservacaoLocal,ObservacaoData,AnimalId,UsuarioId")] Observacoes observacoes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(observacoes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "UsuarioNome", observacoes.UsuarioId);
            ViewData["AnimalId"] = new SelectList(_context.Animais, "Id", "AnimalNome", observacoes.AnimalId);
            return View(observacoes);
        }

        // GET: Observacoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Observações == null)
            {
                return NotFound();
            }

            var observacoes = await _context.Observações.FindAsync(id);
            if (observacoes == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "UsuarioNome", observacoes.UsuarioId);
            ViewData["AnimalId"] = new SelectList(_context.Animais, "Id", "AnimalNome", observacoes.AnimalId);
            return View(observacoes);
        }

        // POST: Observacoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ObservacaoDescricao,ObservacaoLocal,ObservacaoData,AnimalId,UsuarioId")] Observacoes observacoes)
        {
            if (id != observacoes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(observacoes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ObservacoesExists(observacoes.Id))
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
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "UsuarioNome", observacoes.UsuarioId);
            ViewData["AnimalId"] = new SelectList(_context.Animais, "Id", "AnimalNome", observacoes.AnimalId);
            return View(observacoes);
        }

        // GET: Observacoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Observações == null)
            {
                return NotFound();
            }

            var observacoes = await _context.Observações
                .Include(o => o.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (observacoes == null)
            {
                return NotFound();
            }

            return View(observacoes);
        }

        // POST: Observacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Observações == null)
            {
                return Problem("Entity set 'Contexto.Observações'  is null.");
            }
            var observacoes = await _context.Observações.FindAsync(id);
            if (observacoes != null)
            {
                _context.Observações.Remove(observacoes);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ObservacoesExists(int id)
        {
          return (_context.Observações?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
