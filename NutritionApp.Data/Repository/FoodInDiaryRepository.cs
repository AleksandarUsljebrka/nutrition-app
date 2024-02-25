using NutritionApp.Data.Data;
using NutritionApp.Data.Repository.IRepository;
using NutritionApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NutritionApp.Data.Repository
{
	public class FoodInDiaryRepository : Repository<FoodInDiary>, IFoodInDiaryRepository
	{
        public FoodInDiaryRepository(ApplicationDbContext _context):base(_context)
        {
            
        }

		public void Update(FoodInDiary foodInDiary)
		{
			_context.FoodInDiaries.Update(foodInDiary);
		}
		//nije dovrseno, stoji onako samo metoda
		public ICollection<FoodInDiary> GetAllFoodFromDiary(Expression<Func<FoodInDiary, bool>> filter)
		{
			return _context.Set<FoodInDiary>().ToList();
		}

	}
}
