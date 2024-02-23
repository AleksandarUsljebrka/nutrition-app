using Microsoft.AspNetCore.Mvc;
using NutritionApp.Models;

namespace NutritionApp.Controllers
{
    public class DiaryController : Controller
    {
		
		public DiaryController() { }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddToDiary(int? foodId)
		{
			//if (foodId == null || foodId == 0)
			//{
			//	return NotFound();
			//}

			//Food? dbFood = _foodService.GetFood(foodId);

			//if (dbFood == null)
			//{
			//	return NotFound();
			//}
			//return View(dbFood);
			return View();
		}
    }
}
