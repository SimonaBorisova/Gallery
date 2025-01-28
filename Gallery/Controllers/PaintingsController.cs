using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gallery.Data;
using Gallery.Models;

namespace Gallery.Controllers
{
    public class PaintingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaintingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Paintings

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Paintings.Include(p => p.ArtTechnique).Include(p => p.Author).Include(p => p.Genre).Include(p => p.Style);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Paintings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var painting = await _context.Paintings
                .Include(p => p.ArtTechnique)
                .Include(p => p.Author)
                .Include(p => p.Genre)
                .Include(p => p.Style)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (painting == null)
            {
                return NotFound();
            }

            return View(painting);
        }

        // GET: Paintings/Create
        public IActionResult Create()
        {
            ViewData["ArtTechniqueId"] = new SelectList(_context.ArtTechniques, "Id", "Id");
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "BirthYear");
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name");
            ViewData["StyleId"] = new SelectList(_context.Styles, "Id", "Name");
            return View();
        }

        // POST: Paintings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,AuthorId,ReleaseYear,GenreId,StyleId,ArtTechniqueId,PictureUrl,HallNum")] Painting painting)
        {
            if (ModelState.IsValid)
            {
                _context.Add(painting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtTechniqueId"] = new SelectList(_context.ArtTechniques, "Id", "Id", painting.ArtTechniqueId);
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "BirthYear", painting.AuthorId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", painting.GenreId);
            ViewData["StyleId"] = new SelectList(_context.Styles, "Id", "Name", painting.StyleId);
            return View(painting);
        }

        // GET: Paintings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var painting = await _context.Paintings.FindAsync(id);
            if (painting == null)
            {
                return NotFound();
            }
            ViewData["ArtTechniqueId"] = new SelectList(_context.ArtTechniques, "Id", "Id", painting.ArtTechniqueId);
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "BirthYear", painting.AuthorId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", painting.GenreId);
            ViewData["StyleId"] = new SelectList(_context.Styles, "Id", "Name", painting.StyleId);
            return View(painting);
        }

        // POST: Paintings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,AuthorId,ReleaseYear,GenreId,StyleId,ArtTechniqueId,PictureUrl,HallNum")] Painting painting)
        {
            if (id != painting.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(painting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaintingExists(painting.Id))
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
            ViewData["ArtTechniqueId"] = new SelectList(_context.ArtTechniques, "Id", "Id", painting.ArtTechniqueId);
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "BirthYear", painting.AuthorId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", painting.GenreId);
            ViewData["StyleId"] = new SelectList(_context.Styles, "Id", "Name", painting.StyleId);
            return View(painting);
        }

        // GET: Paintings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var painting = await _context.Paintings
                .Include(p => p.ArtTechnique)
                .Include(p => p.Author)
                .Include(p => p.Genre)
                .Include(p => p.Style)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (painting == null)
            {
                return NotFound();
            }

            return View(painting);
        }

        // POST: Paintings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var painting = await _context.Paintings.FindAsync(id);
            if (painting != null)
            {
                _context.Paintings.Remove(painting);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaintingExists(int id)
        {
            return _context.Paintings.Any(e => e.Id == id);
        }
    }
}
