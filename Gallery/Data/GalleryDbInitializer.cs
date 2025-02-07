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
                UserManager<ApplicationUser> userManager = provider
                    .GetRequiredService<UserManager<ApplicationUser>>();

                await SeedRoles.SeedRolesAsync(roleManager);
                await SeedUsers.SeedEmployeesAsync(userManager);
            }

        }
    }
}
