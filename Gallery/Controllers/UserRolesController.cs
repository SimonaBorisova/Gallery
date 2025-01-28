using Gallery.Data;
using Gallery.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gallery.Controllers
{
    public class UserRolesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRolesController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task <IActionResult> Index()
        {
            List<ApplicationUser> users = await _userManager.Users.ToListAsync();
            List<UserRolesViewModel> userRolesViewModels = new();

            foreach(ApplicationUser user in users) 
            {
                UserRolesViewModel userRolesViewModel = new()
                {
                    UserId = user.Id,
                    Email = user.Email
                };
                userRolesViewModels.Add(userRolesViewModel);
            }
            return View(userRolesViewModels);
        }
    }
}
