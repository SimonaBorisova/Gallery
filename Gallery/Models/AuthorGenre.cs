using System.ComponentModel.DataAnnotations.Schema;

namespace Gallery.Models
{
    public class AuthorGenre
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public Author Author { get; set; }
        public int GenreId { get; set; }

        [ForeignKey(nameof(GenreId))] 
        public Genre Genre { get; set; }
    }
}
