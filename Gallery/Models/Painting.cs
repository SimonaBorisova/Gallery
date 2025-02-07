using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gallery.Enums;

namespace Gallery.Models
{
    public class Painting
    {
        public int Id { get; set; }

        [Required, Display(Name = "Название")]
        public string Name { get; set; }

        public int AuthorId { get; set; }

        [Display(Name = "Автор")]
        public Author Author { get; set; }

        [Required, Display(Name = "Година на издаване")]
        public int ReleaseYear { get; set; }

        [Display(Name = "Жанр")]
        public Genre GenreId { get; set; }

        [Required, Display(Name = "Стил")]
        public Style StyleId { get; set; }

        [Required, Display(Name = "Техника на рисуване")]
        public ArtTechnique ArtTechniqueId { get; set; }

        [Required, Display(Name = "Снимка")]
        public string PictureUrl {  get; set; }

        [Required, Display(Name = "Номер на залата")]
        [Column(TypeName = "char(4)")]
        public string HallNumber { get; set; }
    }
}
