using System.ComponentModel.DataAnnotations;

namespace Gallery.Enums
{
    public enum ArtTechnique : byte
    {
        [Display(Name = "Маслени бои")]
        OilPaint = 1,

        [Display(Name = "Темперни бои")]
        TemperPaint = 2,

        [Display(Name = "Сух пастел")]
        DryPastel = 3,

        [Display(Name = "Акварелни бои")]
        WaterColor = 4
    }
}
