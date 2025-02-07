using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gallery.Data;
using Gallery.Models;
using Gallery.Enums;
using Gallery.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Gallery.Services;

namespace Gallery.Controllers
{
    public class PaintingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEnumConvertService _enumConvertService;

        public PaintingsController(ApplicationDbContext context, IEnumConvertService enumDisplayService)
        {
            _context = context;
            _enumConvertService = enumDisplayService;
        }

        [HttpGet]
        public IActionResult Index(string searched, int? authorId)
        {
            IQueryable<Painting> paintings = _context.Paintings
                .Include(painting => painting.Author)
                .AsQueryable();

            ViewBag.Searched = searched;

            if (!string.IsNullOrEmpty(searched))
            {
                paintings = paintings
                    .Where(Painting => Painting.Name.Contains(searched));
            }

            if (authorId != null)
            {
                paintings = paintings
                    .Where(painting => painting.AuthorId == authorId);
            }

            PaintingAuthorViewModel paintingAuthorViewModel = new()
            {
                Paintings = paintings,
                Authors = new(_context.Authors.ToList(), "Id", "Name")
            };

            return View(paintingAuthorViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Painting? painting = await _context.Paintings
                .Include(painting => painting.Author)
                .FirstOrDefaultAsync(painting => painting.Id == id);

            if (painting == null)
            {
                return NotFound();
            }

            return View(painting);
        }

        [HttpGet]
        [Authorize(Roles = "Employee")]
        public IActionResult Create()
        {
            PaintingViewModel paintingViewModel = CreateNewPaintingViewModel();
            return View(paintingViewModel);
        }

        [Authorize(Roles = "Employee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaintingViewModel paintingViewModel)
        {
            if (ModelState.IsValid)
            {
                // Не е ясно дали всички свойства се попълват.
                Painting painting = new()
                {
                    Name = paintingViewModel.Name,
                    AuthorId = paintingViewModel.SelectedAuthor,
                    ReleaseYear = paintingViewModel.ReleaseYear,
                    GenreId = (Genre)paintingViewModel.SelectedGenre,
                    StyleId = (Style)paintingViewModel.SelectedStyle,
                    ArtTechniqueId = (ArtTechnique)paintingViewModel.SelectedArtTechnique
                };

                //string imagePath = "";

                //using (MemoryStream memoryStream = new())
                //{
                //    paintingViewModel.Picture.CopyTo(memoryStream);

                //    imagePath = $"wwwroot/images/pictures/{author.Name.ToString()}/{painting.Name}.png";
                //    Directory.CreateDirectory(Path.GetDirectoryName(imagePath));
                //    FileStream fileStream = new(imagePath, FileMode.Create);
                //    paintingViewModel.Picture.CopyTo(fileStream);
                //}

                //painting.PictureUrl = imagePath;

                _context.Add(painting);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            paintingViewModel = CreateNewPaintingViewModel();
            return View(paintingViewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Painting? painting = await _context.Paintings.FindAsync(id);

            if (painting == null)
            {
                return NotFound();
            }

            // За оправяне.
            ViewData["ArtTechniqueId"] = new SelectList(Enum.GetValues(typeof(ArtTechnique)), "ArtTechnique");
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "BirthYear", painting.AuthorId);
            ViewData["GenreId"] = new SelectList(Enum.GetValues(typeof(Genre)), "Genre");
            ViewData["StyleId"] = new SelectList(Enum.GetValues(typeof(Style)), "Style");

            return View(painting);
        }

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

            // За оправяне.
            ViewData["ArtTechniqueId"] = new SelectList(Enum.GetValues(typeof(ArtTechnique)), "ArtTechnique");
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "BirthYear", painting.AuthorId);
            ViewData["GenreId"] = new SelectList(Enum.GetValues(typeof(Genre)), "Genre");
            ViewData["StyleId"] = new SelectList(Enum.GetValues(typeof(Style)), "Style");

            return View(painting);
        }

        [HttpGet]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Painting? painting = await _context.Paintings
                .Include(painting => painting.Author)
                .FirstOrDefaultAsync(painting => painting.Id == id);

            if (painting == null)
            {
                return NotFound();
            }

            return View(painting);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Painting? painting = await _context.Paintings.FindAsync(id);

            if (painting != null)
            {
                _context.Paintings.Remove(painting);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaintingExists(int id)
        {
            return _context.Paintings.Any(painting => painting.Id == id);
        }

        private PaintingViewModel CreateNewPaintingViewModel()
        {
            return new()
            {
                Authors = new SelectList(_context.Authors, "Id", "Name"),
                Genres = _enumConvertService.ByteEnumValuesToSelectList<Genre>().ToList(),
                Styles = _enumConvertService.ByteEnumValuesToSelectList<Style>().ToList(),
                ArtTechniques = _enumConvertService.ByteEnumAttributesToSelectList<ArtTechnique>().ToList(),
                FloorNumbers = _enumConvertService.ByteEnumValuesToSelectList<FloorNumber>().ToList()
            };
        }
    }
}
