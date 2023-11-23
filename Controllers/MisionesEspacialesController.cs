using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppWeb_Astro.Models;

namespace AppWeb_Astro.Controllers
{
    public class MisionesEspacialesController : Controller
    {
        private readonly AstroContext _context;

        public MisionesEspacialesController(AstroContext context)
        {
            _context = context;
        }

        // GET: MisionesEspaciales
        public async Task<IActionResult> Index()
        {
              return _context.MisionesEspaciales != null ? 
                          View(await _context.MisionesEspaciales.ToListAsync()) :
                          Problem("Entity set 'AstroContext.MisionesEspaciales'  is null.");
        }

        // GET: MisionesEspaciales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MisionesEspaciales == null)
            {
                return NotFound();
            }

            var misionesEspaciale = await _context.MisionesEspaciales
                
                .FirstOrDefaultAsync(m => m.Id == id);
            if (misionesEspaciale == null)
            {
                return NotFound();
            }

            return View(misionesEspaciale);
        }

        // GET: MisionesEspaciales/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MisionesEspaciales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion")] MisionesEspaciale misionesEspaciale)
        {
            if (ModelState.IsValid)
            {
                _context.Add(misionesEspaciale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(misionesEspaciale);
        }

        // GET: MisionesEspaciales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MisionesEspaciales == null)
            {
                return NotFound();
            }

            var misionesEspaciale = await _context.MisionesEspaciales.FindAsync(id);
            if (misionesEspaciale == null)
            {
                return NotFound();
            }
            return View(misionesEspaciale);
        }

        // POST: MisionesEspaciales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion")] MisionesEspaciale misionesEspaciale)
        {
            if (id != misionesEspaciale.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(misionesEspaciale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MisionesEspacialeExists(misionesEspaciale.Id))
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
            return View(misionesEspaciale);
        }

        // GET: MisionesEspaciales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MisionesEspaciales == null)
            {
                return NotFound();
            }

            var misionesEspaciale = await _context.MisionesEspaciales
                .FirstOrDefaultAsync(m => m.Id == id);
            if (misionesEspaciale == null)
            {
                return NotFound();
            }

            return View(misionesEspaciale);
        }

        // POST: MisionesEspaciales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MisionesEspaciales == null)
            {
                return Problem("Entity set 'AstroContext.MisionesEspaciales'  is null.");
            }
            var misionesEspaciale = await _context.MisionesEspaciales.FindAsync(id);
            if (misionesEspaciale != null)
            {
                _context.MisionesEspaciales.Remove(misionesEspaciale);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MisionesEspacialeExists(int id)
        {
          return (_context.MisionesEspaciales?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
