using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Apoio.Data;
using Apoio.Models;

namespace Apoio.Controllers
{
    public class ApostaController : Controller
    {
        private readonly WebetContext _context;

        public ApostaController(WebetContext context)
        {
            _context = context;
        }

        // GET: Aposta
        public async Task<IActionResult> Index()
        {
            var webetContext = _context.Apostas.Include(a => a.Apostador);
            return View(await webetContext.ToListAsync());
        }

        // GET: Aposta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aposta = await _context.Apostas
                .Include(a => a.Apostador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aposta == null)
            {
                return NotFound();
            }

            return View(aposta);
        }

        // GET: Aposta/Create
        public IActionResult Create()
        {
            ViewData["ApostadorId"] = new SelectList(_context.Apostadores, "Id", "Id");
            return View();
        }

        // POST: Aposta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Valor,PrevisaoDeGanho,Vencedora,ApostadorId")] Aposta aposta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aposta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApostadorId"] = new SelectList(_context.Apostadores, "Id", "Id", aposta.ApostadorId);
            return View(aposta);
        }

        // GET: Aposta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aposta = await _context.Apostas.FindAsync(id);
            if (aposta == null)
            {
                return NotFound();
            }
            ViewData["ApostadorId"] = new SelectList(_context.Apostadores, "Id", "Id", aposta.ApostadorId);
            return View(aposta);
        }

        // POST: Aposta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Valor,PrevisaoDeGanho,Vencedora,ApostadorId")] Aposta aposta)
        {
            if (id != aposta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aposta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApostaExists(aposta.Id))
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
            ViewData["ApostadorId"] = new SelectList(_context.Apostadores, "Id", "Id", aposta.ApostadorId);
            return View(aposta);
        }

        // GET: Aposta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aposta = await _context.Apostas
                .Include(a => a.Apostador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aposta == null)
            {
                return NotFound();
            }

            return View(aposta);
        }

        // POST: Aposta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aposta = await _context.Apostas.FindAsync(id);
            if (aposta != null)
            {
                _context.Apostas.Remove(aposta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApostaExists(int id)
        {
            return _context.Apostas.Any(e => e.Id == id);
        }
    }
}
