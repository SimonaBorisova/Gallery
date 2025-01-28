using System.ComponentModel.DataAnnotations;

namespace Gallery.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [Required, Display(Name = "Название")]
        public string Name { get; set; }

        public ICollection<Painting> Paintings { get; set; }

        public ICollection<AuthorGenre> AuthorGenres { get; set; }
    }
}
