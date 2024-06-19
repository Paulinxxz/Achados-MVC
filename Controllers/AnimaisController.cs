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
    public class AnimaisController : Controller
    {
        private readonly Contexto _context;

        public AnimaisController(Contexto context)
        {
            _context = context;
        }

        // GET: Animais
        public async Task<IActionResult> Index()
        {
            var contexto = _context.Animais.Include(a => a.Usuario);
            return View(await contexto.ToListAsync());
        }

        // GET: Animais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Animais == null)
            {
                return NotFound();
            }

            var animais = await _context.Animais
                .Include(a => a.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animais == null)
            {
                return NotFound();
            }

            return View(animais);
        }

        // GET: Animais/Create
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "UsuarioNome");
            return View();
        }

        // POST: Animais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AnimalNome,AnimalRaca,AnimalTipo,AnimalCor,AnimalSexo,AnimalObservacao,AnimalFoto,AnimalDtDesaparecimento,AnimalDtEncontro,AnimalStatus,UsuarioId")] Animais animais)
        {
            if (ModelState.IsValid)
            {
                _context.Add(animais);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "UsuarioNome", animais.UsuarioId);
            return View(animais);
        }

        // GET: Animais/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Animais == null)
            {
                return NotFound();
            }

            var animais = await _context.Animais.FindAsync(id);
            if (animais == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "UsuarioNome", animais.UsuarioId);
            return View(animais);
        }

        // POST: Animais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AnimalNome,AnimalRaca,AnimalTipo,AnimalCor,AnimalSexo,AnimalObservacao,AnimalFoto,AnimalDtDesaparecimento,AnimalDtEncontro,AnimalStatus,UsuarioId")] Animais animais)
        {
            if (id != animais.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animais);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimaisExists(animais.Id))
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
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "UsuarioNome", animais.UsuarioId);
            return View(animais);
        }

        // GET: Animais/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Animais == null)
            {
                return NotFound();
            }

            var animais = await _context.Animais
                .Include(a => a.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animais == null)
            {
                return NotFound();
            }

            return View(animais);
        }

        // POST: Animais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Animais == null)
            {
                return Problem("Entity set 'Contexto.Animais'  is null.");
            }
            var animais = await _context.Animais.FindAsync(id);
            if (animais != null)
            {
                _context.Animais.Remove(animais);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimaisExists(int id)
        {
          return (_context.Animais?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
