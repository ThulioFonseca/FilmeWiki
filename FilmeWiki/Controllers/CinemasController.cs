using FilmeWiki.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmeWiki.Controllers
{
    public class CinemasController : Controller
    {
        private readonly FilmeWikiContext _context;

        public CinemasController(FilmeWikiContext context)
        {
            _context = context;
        }

        // GET: Cinemas
        public async Task<IActionResult> Index(string cinemaEstado, string criterioBusca)
        {

            IQueryable<string> consultaEstado = from c in _context.Cinema
                                                orderby c.UF
                                                select c.UF;

            var cinemas = from c
                          in _context.Cinema
                          select c;

            if (!String.IsNullOrEmpty(criterioBusca))
            {
                cinemas = cinemas.Where(c => c.Descricao.Contains(criterioBusca));
            }

            if (!String.IsNullOrEmpty(cinemaEstado))
            {
                cinemas = cinemas.Where(c => c.UF == cinemaEstado);

            }

            var cinemaEstadoVM = new CinemaEstadoViewModel();
            cinemaEstadoVM.estados = new SelectList(await consultaEstado.Distinct().ToListAsync());
            cinemaEstadoVM.cinemas = await cinemas.ToListAsync();

            return View(cinemaEstadoVM);
        }

        // GET: Cinemas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cinema = await _context.Cinema.Include(c => c.Cartaz).ThenInclude(f => f.Filme).FirstOrDefaultAsync(m => m.ID == id);

            if (cinema == null)
            {
                return NotFound();
            }

            return View(cinema);
        }

        // GET: Cinemas/Create
        public IActionResult Create()

        {

            return View();
        }

        // POST: Cinemas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Descricao,Cidade,UF")] Cinema cinema)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cinema);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cinema);
        }

        // GET: Cinemas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cinema = await _context.Cinema.FindAsync(id);

            Cartaz getCartaz = new Cartaz();

            ViewBag.filmesDisponiveis = new MultiSelectList(_context.Filme, "ID", "Titulo");

            if (cinema == null)
            {
                return NotFound();
            }
            return View(cinema);
        }

        // POST: Cinemas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cinema cinema, int[] selectCartaz)
        {

            if (id != cinema.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    
                    if (!selectCartaz.Any(item => item == 0))
                    {
                                                
                        foreach (int item in selectCartaz)
                        {
                            Cartaz ct = new Cartaz();

                            ct.FilmeId = item;

                            ct.CinemaId = cinema.ID;
                            

                            if (!_context.Cartaz.Any(c => c.CinemaId == cinema.ID && c.FilmeId == item))
                            {                                                              
                                _context.Add(ct);
                                                            
                            }
                           
                        }
                                                                       
                    }

                    else
                    {
                        _context.RemoveRange(_context.Cartaz.Where(c => c.CinemaId == cinema.ID));

                    }


                    _context.Update(cinema);

                    await _context.SaveChangesAsync();


                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CinemaExists(cinema.ID))
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
            return View(cinema);
        }

        // GET: Cinemas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cinema = await _context.Cinema
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cinema == null)
            {
                return NotFound();
            }

            return View(cinema);
        }

        // POST: Cinemas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cinema = await _context.Cinema.FindAsync(id);
            _context.Cinema.Remove(cinema);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CinemaExists(int id)
        {
            return _context.Cinema.Any(e => e.ID == id);
        }
    }
}
