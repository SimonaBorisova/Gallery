using Gallery.Data.Seeds;

using Microsoft.AspNetCore.Identity;

namespace Gallery.Data
{
    public class GalleryDbInitializer
    {
        public static async Task Initialise(IApplicationBuilder app)
        {
            using (IServiceScope service = app.ApplicationServices.CreateScope())
            {
                IServiceProvider provider = service.ServiceProvider;

                RoleManager<IdentityRole> roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();

                UserManager<IdentityUser> userManager = provider
                    .GetRequiredService<UserManager<IdentityUser>>();

                ApplicationDbContext contextArtTechniques = provider.GetRequiredService<ApplicationDbContext>();

                await SeedRoles.SeedRolesAsync(roleManager);
                await SeedUsers.SeedEmployeesAsync(userManager);
            }
        }
    }
}
