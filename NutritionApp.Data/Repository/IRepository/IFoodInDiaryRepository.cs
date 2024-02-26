using NutritionApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NutritionApp.Data.Repository.IRepository
{
	public interface IFoodInDiaryRepository : IRepository<FoodInDiary>
	{
		public void Update(FoodInDiary foodInDiary);

		public ICollection<FoodInDiary> GetAllFoodFromDiary(Expression<Func<FoodInDiary, bool>> filter);
		public void RemoveRang(IEnumerable<FoodInDiary>foodInDiary);
	}
}
