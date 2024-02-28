using Microsoft.AspNetCore.Identity;
using NutritionApp.BusinessLogic.Services.Interfaces;
using NutritionApp.Data.Repository.UnitOfWork;
using NutritionApp.Models;
using NutritionApp.Models.AuthModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionApp.BusinessLogic.Services
{
	public class UserService : IUserService
	{
		private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public UserService(IUnitOfWork unitOfWork, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
			_signInManager = signInManager;
			_userManager = userManager;
		}

        public async Task<bool> LoginAsync(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username!, model.Password!, model.RememberMe, false);
            return result.Succeeded;
        }

        public async Task<bool> RegisterAsync(RegisterViewModel model)
        {
            User user = new()
            {
                Name = model.Name,
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password!);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                await _signInManager.SignInAsync(user, false);
                return true;
            }

            return false;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

		public async Task<bool> DeleteUserAsync(string userId)
		{
			var user = await _userManager.FindByIdAsync(userId);
			if (user == null)
			{
				return false;
			}

			var result = await _userManager.DeleteAsync(user);
			return result.Succeeded;
		}


		public async Task<List<User>> GetUsersOfTypeUserAsync()
        {
            
            var users = await _userManager.GetUsersInRoleAsync("User");
			if (users == null)
			{
				return null;
			}
			return users.ToList();
        }
        public async Task<User> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            
            if(user == null)
            {
                return null;
            }
            return user;
        }
    }
}
