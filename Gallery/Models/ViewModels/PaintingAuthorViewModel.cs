using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gallery.Models.ViewModels
{
    public class PaintingAuthorViewModel
    {
        public IQueryable<Painting> Paintings { get; set; }

        public SelectList Authors { get; set; }

        public int AuthorId {  get; set; }
    }
}
