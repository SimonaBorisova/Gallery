using Gallery.Enums;
using Microsoft.AspNetCore.Identity;

namespace Gallery.Data.Seeds
{
    public static class SeedUsers
    {
       
        public static async Task SeedEmployeesAsync(UserManager<ApplicationUser> userManager)
        {
            ApplicationUser employee1 = new()
            {
                UserName = "moni",
                Email = "moni@gmail.com",
                EmailConfirmed = true,

            };
            ApplicationUser employee2 = new()
            {
                UserName = "deni",
                Email = "deni@gmail.com",
                EmailConfirmed = true,

            };
            ApplicationUser employee3 = new()
            {
                UserName = "eni",
                Email = "eni@gmail.com",
                EmailConfirmed = true,

            };


            ApplicationUser alreadyExists = await userManager.FindByEmailAsync(employee1.Email);

            if (alreadyExists == null)
            {
                await userManager.CreateAsync(employee1, "123%Ab");
                await userManager.CreateAsync(employee2, "abcdef");
                await userManager.CreateAsync(employee3, "abcdef");

                await userManager.AddToRoleAsync(employee1, Role.Employee.ToString());
                await userManager.AddToRoleAsync(employee2, Role.Employee.ToString());
                await userManager.AddToRoleAsync(employee3, Role.Employee.ToString());
            }
        }
    }
}
