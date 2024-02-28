using NutritionApp.Models;
using NutritionApp.Models.AuthModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionApp.BusinessLogic.Services.Interfaces
{
	public interface IUserService
	{

        Task<bool> LoginAsync(LoginViewModel model);
        Task<bool> RegisterAsync(RegisterViewModel model);
        Task LogoutAsync();
        Task<List<User>> GetUsersOfTypeUserAsync();
		Task<bool> DeleteUserAsync(string userId);
		Task<User> GetUserById(string id);


	}
}
