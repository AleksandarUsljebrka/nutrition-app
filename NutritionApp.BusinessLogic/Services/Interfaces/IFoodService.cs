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
		public List<Food> GetAllFood();
		public void AddFood(Food? food);
		public Food GetFood(int? id);
		public void UpdateFood(Food? food);
		public void DeleteFood(Food? food);

	}
}
