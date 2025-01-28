using Gallery.Models;
using Microsoft.AspNetCore.Identity;

namespace Gallery.Data
{
    public class GalleryDbInitializer
    {
        public static async void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var scope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                await CreateRoles(roleManager);
            }
        }
        public static async Task CreateRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Employee"))
            {
                await roleManager.CreateAsync(new IdentityRole("Employee"));
                await roleManager.CreateAsync(new IdentityRole("Guest"));
            }
        }
    }
}
