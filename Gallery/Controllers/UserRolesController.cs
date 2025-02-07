using Gallery.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gallery.Controllers
{
    public class UserRolesController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserRolesController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            List<IdentityUser> users = await _userManager.Users.ToListAsync();
            List<UserRolesViewModel> userRolesViewModels = new();

            foreach (IdentityUser user in users)
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
