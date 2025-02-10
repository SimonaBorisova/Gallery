using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gallery.Data;
using Gallery.Models;
using Gallery.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Gallery.Services;
using NuGet.Protocol.Core.Types;

namespace Gallery.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEnumConvertService _enumConvertService;

        public AuthorsController(ApplicationDbContext context, IEnumConvertService enumDisplayService)
        {
            _context = context;
            _enumConvertService = enumDisplayService;
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

        [HttpGet]
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

        [HttpGet]
        [Authorize(Roles = "Employee")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Employee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuthorViewModel authorsViewModel)
        {
            if (authorsViewModel.Portrait != null && authorsViewModel.Portrait.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(authorsViewModel.Portrait.FileName);

                var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                var filePath = Path.Combine(uploadFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await authorsViewModel.Portrait.CopyToAsync(stream);
                }

                if (ModelState.IsValid)
                {
                    Author author = new Author()
                    {
                        Name = authorsViewModel.Name,
                        BirthYear = authorsViewModel.BirthYear,
                        DeathYear = authorsViewModel.DeathYear,
                        NationalityId = authorsViewModel.Nationality,
                        PortraitUrl = fileName
                    };

                    _context.Authors.Add(author);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(authorsViewModel);
        }

        
        [HttpGet]
        [Authorize(Roles = "Employee")]
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
        [Authorize(Roles = "Employee")]
        [HttpGet]
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
