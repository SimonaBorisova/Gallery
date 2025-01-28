using Gallery.Models;
using Microsoft.AspNetCore.Identity;

namespace Gallery.Data
{
    public class ApplicationUser:IdentityUser
    {
        public ICollection<Painting> Paintings { get; set; }
    }
}
