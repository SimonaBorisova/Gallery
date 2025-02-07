using Gallery.Enums;
using Microsoft.EntityFrameworkCore;

namespace Gallery.Models
{
    [PrimaryKey(nameof(AuthorId), nameof(GenreId))]
    public class AuthorGenre
    {
        public int AuthorId { get; set; }

        public Author Author { get; set; }

        public Genre GenreId { get; set; }
    }
}
