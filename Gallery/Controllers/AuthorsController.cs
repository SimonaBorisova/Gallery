using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gallery.Data;
using Gallery.Models;
using Gallery.Models.ViewModels;

namespace Gallery.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searched)
        {
            IQueryable<Author> authors = _context.Authors
                 .AsNoTracking();

            ViewBag.Searched = searched;

            if (!string.IsNullOrEmpty(searched))
            {
                authors = authors
                    .Where(Painting => Painting.Name.Contains(searched));
            }

            return View(authors);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( AuthorViewModel authorsViewModel)
        {

            if (ModelState.IsValid)
            {
                Author author = new Author()
                {
                    Name = authorsViewModel.Name,
                    BirthYear = authorsViewModel.BirthYear,
                    DeathYear = authorsViewModel.DeathYear,

                };
                string imagePath = "";
                using (var memoryStream = new MemoryStream())
                {
                    authorsViewModel.Portrait.CopyTo(memoryStream);

                    imagePath = $"wwwroot/images/authorsPortraits/{authorsViewModel.Name.ToString()}/{authorsViewModel.Name}.png";
                    Directory.CreateDirectory(Path.GetDirectoryName(imagePath));
                    FileStream fileStream = new FileStream(imagePath, FileMode.Create);
                    authorsViewModel.Portrait.CopyTo(fileStream);
                }
                author.PortraitUrl = imagePath;

                _context.Authors.Add(author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(authorsViewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BirthYear,DeathYear,Nationality,PortraitUrl")] Author author)
        {
            if (id != author.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(author);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(author.Id))
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
            return View(author);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author != null)
            {
                _context.Authors.Remove(author);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}
