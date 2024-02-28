using NutritionApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionApp.Data.Repository.IRepository
{
	public interface IFoodRepository:IRepository<Food>
	{
		ICollection<Food> GetAllByType(string type);
		void Update(Food food);
	}
}
