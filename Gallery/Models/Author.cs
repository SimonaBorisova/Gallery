using System.ComponentModel.DataAnnotations;

namespace Gallery.Models
{
    public class Author
    {
        public int Id { get; set; }
        [Required, Display(Name = "Година на раждане")]
        public string BirthYear { get; set; }
        [Required, Display(Name = "Година на смъртта")]
        public string DeathYear { get; set; }
        [Required, Display(Name = "Националност")]
        public string Nationality { get; set; }
        [Required, Display(Name = "Портрет")]
        public string PortraitUrl { get; set; }
        public ICollection<Painting> Paintings {  get; set; }
        public ICollection<AuthorGenre> AuthorGenres { get; set; }
    }
}
