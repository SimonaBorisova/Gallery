using System.ComponentModel.DataAnnotations;

namespace Gallery.Models
{
    public class Painting
    {
        public int Id { get; set; }
        [Required, Display(Name = "Название")]
        public string Name { get; set; }
        public int AuthorId { get; set; }
        [Required, Display(Name = "Автор")]
        public Author Author { get; set; }
        [Required, Display(Name = "Година на издаване")]
        public int ReleaseYear { get; set; }
        public int GenreId { get; set; }
        [Required, Display(Name = "Жанр")]
        public Genre Genre { get; set; }
        public int StyleId { get; set; }
        [Required, Display(Name = "Стил")]
        public Style Style { get; set; }
        public int ArtTechniqueId { get; set; }
        [Required, Display(Name = "Техника на рисуване")]
        public ArtTechnique ArtTechnique { get; set; }
        [Required, Display(Name = "Снимка")]
        public string PictureUrl {  get; set; }
        [Required, Display(Name = "Номер на залата")]
        public HallNum HallNum { get; set; }
    }
}
