using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NutritionApp.BusinessLogic.Services.Interfaces;
using NutritionApp.Models;
using NutritionApp.Models.AuthModels;

namespace NutritionApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginAsync(model);
                if (result)
                {
                    return RedirectToAction("Index", "Food");
                }
                ModelState.AddModelError("", "Prijava nije uspela.");
            }
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterAsync(model);
                if (result)
                {
                    return RedirectToAction("Index", "Food");
                }
                ModelState.AddModelError("", "Registracija nije uspela.");
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutAsync();
            return RedirectToAction("Login");
        }

        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AllUsers()
        {
            List<User> users = await _userService.GetUsersOfTypeUserAsync();
            if(users == null)
            {
                return NotFound();
            }
            return View(users);
        }
		[Authorize(Roles = "Admin")]

		public async Task<IActionResult> DeleteUser(string? id)
        {
			if (id == null)
			{
				TempData["error"] = "Greška prilikom brisanja korisnika!";
				return RedirectToAction("AllUsers");
			}
			User user = await _userService.GetUserById(id);
            if (user == null)
			{
				TempData["error"] = "Korisnik nije pronadjen!";
				return RedirectToAction("AllUsers");
			}
			return View(user);
		}

        [Authorize(Roles ="Admin")]
		[HttpPost,ActionName("DeleteUser")]
        public async Task<IActionResult> DeleteUserPOST(string? id)
        {
            if (id == null)
            {
				TempData["error"] = "Greška prilikom brisanja korisnika!";

				return RedirectToAction("AllUsers");
			}
            bool success = await _userService.DeleteUserAsync(id);
            if(!success)
                TempData["error"] = "Greška prilikom brisanja korisnika!";
            else
			    TempData["success"] = "Korisnik uspešno obrisan!";

			return RedirectToAction("AllUsers");

		}
	}
}
