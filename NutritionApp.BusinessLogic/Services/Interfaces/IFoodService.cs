using NutritionApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionApp.BusinessLogic.Services.Interfaces
{
	public interface IFoodService
	{
		List<Food> GetAllFood();
		List<Food> GetFoodByType(string type);
		void AddFood(Food? food);
		Food GetFood(int? id);
		void UpdateFood(Food? food);
		void DeleteFood(Food? food);

	}
}
