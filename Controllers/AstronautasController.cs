using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppWeb_Astro.Models;

namespace AppWeb_Astro.Controllers
{
    public class AstronautasController : Controller
    {
        private readonly AstroContext _context;

        public AstronautasController(AstroContext context)
        {
            _context = context;
        }

        // GET: Astronautas
        public async Task<IActionResult> Index(string buscar, bool? ACB)
        {
                var AstronautasQuery = from Astronauta in _context.Astronautas select Astronauta;

                if (!String.IsNullOrEmpty(buscar))
                {
                AstronautasQuery = AstronautasQuery.Where(s => s.Nacionalidad != null && s.Nacionalidad.Contains(buscar));

            }
                if(ACB.HasValue)
                {
                    AstronautasQuery = AstronautasQuery.Where(a => a.Activo == ACB.Value);
                }


                  return View(await AstronautasQuery.ToListAsync());


        }

        // GET: Astronautas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var astronauta = await _context.Astronautas
                .Include(a => a.MisionEspacials)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (astronauta == null)
            {
                return NotFound();
            }

            astronauta.MisionEspacials = await _context.MisionesEspaciales.ToListAsync();

            return View(astronauta);
        }

        // GET: Astronautas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Astronautas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Nacionalidad,FechaNacimiento,Edad,Activo,ImagenUrl,Redes")] Astronauta astronauta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(astronauta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(astronauta);
        }

        // GET: Astronautas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Astronautas == null)
            {
                return NotFound();
            }

            var astronauta = await _context.Astronautas.FindAsync(id);
            if (astronauta == null)
            {
                return NotFound();
            }
            return View(astronauta);
        }

        // POST: Astronautas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Nacionalidad,FechaNacimiento,Edad,Activo,ImagenUrl,Redes")] Astronauta astronauta)
        {
            if (id != astronauta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(astronauta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AstronautaExists(astronauta.Id))
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
            return View(astronauta);
        }

        // GET: Astronautas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Astronautas == null)
            {
                return NotFound();
            }

            var astronauta = await _context.Astronautas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (astronauta == null)
            {
                return NotFound();
            }

            return View(astronauta);
        }

        // POST: Astronautas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Astronautas == null)
            {
                return Problem("Entity set 'AstroContext.Astronautas'  is null.");
            }
            var astronauta = await _context.Astronautas.FindAsync(id);
            if (astronauta != null)
            {
                _context.Astronautas.Remove(astronauta);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AstronautaExists(int id)
        {
          return (_context.Astronautas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
