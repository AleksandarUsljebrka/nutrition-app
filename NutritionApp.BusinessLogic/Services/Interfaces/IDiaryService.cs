using NutritionApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionApp.BusinessLogic.Services.Interfaces
{
	public interface IDiaryService
	{
		public void AddFoodToDiary(int? foodId, float grams);
		public UserDiary GetTodaysDiary();
		public UserDiary GetDiaryById(int? id);
		public int GetDiaryIdByFoodId(int? id);
		public List<UserDiary> GetAllDiaries();
		public FoodInDiary GetFoodInDiary(int? foodId);
		public bool DeleteFoodFromDiary(int? foodId);
		public bool DeleteDiary(int? id);
		public bool UpdateFoodFromDiary(int? foodId, float editedGrams);




    }
}
