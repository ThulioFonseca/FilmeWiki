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
    public class FilmesController : Controller
    {
        private readonly FilmeWikiContext _context;

        public FilmesController(FilmeWikiContext context)
        {
            _context = context;
        }

        // GET: Filmes
        public async Task<IActionResult> Index(string filmeGenero, string criterioBusca)
        {

            IQueryable<string> consultaGenero = from f in _context.Filme
                                                orderby f.Genero
                                                select f.Genero;


            var filmes = from m
                         in _context.Filme
                         select m;

            if (!String.IsNullOrEmpty(criterioBusca))
            {

                filmes = filmes.Where(f => f.Titulo.Contains(criterioBusca));
            }

            if (!String.IsNullOrEmpty(filmeGenero))
            {
                filmes = filmes.Where(f => f.Genero == filmeGenero);
            }

            var filmeGeneroVM = new FilmeGeneroViewModel();
            filmeGeneroVM.generos = new SelectList(await consultaGenero.Distinct().ToListAsync());
            filmeGeneroVM.filmes = await filmes.ToListAsync();

            return View(filmeGeneroVM);
        }

        // GET: Filmes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filme = await _context.Filme.FirstOrDefaultAsync(m => m.ID == id);

            if (filme == null)
            {
                return NotFound();
            }

            return View(filme);
        }

        // GET: Filmes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Filmes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Titulo,Lancamento,Genero,Classificacao,Preco")] Filme filme)
        {
            if (ModelState.IsValid)
            {
                _context.Add(filme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(filme);
        }

        // GET: Filmes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filme = await _context.Filme.FindAsync(id);
            if (filme == null)
            {
                return NotFound();
            }
            return View(filme);
        }

        // POST: Filmes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Titulo,Lancamento,Genero,Classificacao,Preco")] Filme filme)
        {
            if (id != filme.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(filme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmeExists(filme.ID))
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
            return View(filme);
        }

        // GET: Filmes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filme = await _context.Filme
                .FirstOrDefaultAsync(m => m.ID == id);
            if (filme == null)
            {
                return NotFound();
            }

            return View(filme);
        }

        // POST: Filmes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var filme = await _context.Filme.FindAsync(id);
            _context.Filme.Remove(filme);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmeExists(int id)
        {
            return _context.Filme.Any(e => e.ID == id);
        }
    }
}
