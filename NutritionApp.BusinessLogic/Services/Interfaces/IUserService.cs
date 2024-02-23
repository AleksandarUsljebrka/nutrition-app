using NutritionApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionApp.BusinessLogic.Services.Interfaces
{
	public interface IUserService
	{
		public User GetUser(string username);
	}
}
