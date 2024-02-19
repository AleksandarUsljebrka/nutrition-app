using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NutritionApp.Models;
using NutritionApp.ViewModels;

namespace NutritionApp.Controllers
{
	public class UserController : Controller
	{
		private readonly SignInManager<User> signInManager;
		private readonly UserManager<User> userManager;
        public UserController( SignInManager<User> signInManager, UserManager<User> userManager)
        {
            this.signInManager = signInManager;
			this.userManager = userManager;
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
				var result = await signInManager.PasswordSignInAsync(model.Username!, model.Password!, model.RememberMe, false);
				if(result.Succeeded)
				{
					return RedirectToAction("Index", "Food");
				}
				ModelState.AddModelError("","Login failed.");
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
				User user = new()
				{
					Name = model.Name,
					UserName = model.Email,
					Email = model.Email
				};

				var result = await userManager.CreateAsync(user, model.Password!);
				if (result.Succeeded)
				{
					await signInManager.SignInAsync(user, false);

					return RedirectToAction("Index", "Food");
				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
			}
			return View(model);
		}

		public async Task<IActionResult> Logout()
		{
			await signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}
