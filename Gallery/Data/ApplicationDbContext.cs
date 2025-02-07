using Gallery.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gallery.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Author> Authors { get; set; }

        public DbSet<AuthorGenre> AuthorGenres { get; set; }

        public DbSet<Painting> Paintings { get; set; }
    }
}
