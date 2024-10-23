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
    public class ApostadorController : Controller
    {
        private readonly WebetContext _context;

        public ApostadorController(WebetContext context)
        {
            _context = context;
        }

        // GET: Apostador
        public async Task<IActionResult> Index()
        {
            return View(await _context.Apostadores.ToListAsync());
        }

        // GET: Apostador/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apostador = await _context.Apostadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (apostador == null)
            {
                return NotFound();
            }

            return View(apostador);
        }

        // GET: Apostador/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Apostador/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Saldo")] Apostador apostador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(apostador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(apostador);
        }

        // GET: Apostador/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apostador = await _context.Apostadores.FindAsync(id);
            if (apostador == null)
            {
                return NotFound();
            }
            return View(apostador);
        }

        // POST: Apostador/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Saldo")] Apostador apostador)
        {
            if (id != apostador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(apostador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApostadorExists(apostador.Id))
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
            return View(apostador);
        }

        // GET: Apostador/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apostador = await _context.Apostadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (apostador == null)
            {
                return NotFound();
            }

            return View(apostador);
        }

        // POST: Apostador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var apostador = await _context.Apostadores.FindAsync(id);
            if (apostador != null)
            {
                _context.Apostadores.Remove(apostador);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApostadorExists(int id)
        {
            return _context.Apostadores.Any(e => e.Id == id);
        }
    }
}
