using System.ComponentModel.DataAnnotations;
using Gallery.Enums;

namespace Gallery.Models.ViewModels
{
    public class AuthorViewModel
    {
        [Required(ErrorMessage = "Това поле е задължително."), Display(Name = "Име")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Това поле е задължително."), Display(Name = "Година на раждане")]
        public string BirthYear { get; set; }

        [Required(ErrorMessage = "Това поле е задължително."), Display(Name = "Година на смъртта")]
        public string DeathYear { get; set; }

        [Required(ErrorMessage = "Това поле е задължително."), Display(Name = "Националност")]
        public Nationality Nationality { get; set; }

        [Required(ErrorMessage = "Това поле е задължително.") , Display(Name = "Портрет")]
        [DataType(DataType.Upload)]
        public IFormFile Portrait{ get; set; }

    }
}
