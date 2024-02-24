using Microsoft.AspNetCore.Mvc;
using NutritionApp.BusinessLogic.Services;
using NutritionApp.BusinessLogic.Services.Interfaces;
using NutritionApp.Models;
using System.ComponentModel;

namespace NutritionApp.Controllers
{
    public class DiaryController : Controller
    {
		private readonly IDiaryService _diaryService;
		private readonly IFoodService _foodService;


		public DiaryController(IDiaryService diaryService, IFoodService foodService) 
		{
			_diaryService = diaryService;
			_foodService = foodService;
		}
        public IActionResult Index()
        {
			UserDiary diary = _diaryService.GetDiary();
			if(diary == null)
			{
				ViewBag.NotFound = "There is no data to show.";
				return View();
			}
            return View(diary);
        }

        public IActionResult AddToDiary(int? foodId)
		{

			if (foodId == null || foodId == 0)
			{
				return NotFound();
			}
			Food? dbFood = _foodService.GetFood(foodId);
			if (dbFood == null)
			{
				return NotFound();
			}
			return View(dbFood);
		}
		[HttpPost, ActionName("AddToDiary")]
		public IActionResult AddToDiaryPOST(int? id)
		{

			if (id == null || id == 0)
			{
				return NotFound();
			}
			var grams = Request.Form["grams"];
			if (float.TryParse(grams, out float quantity))
			{
				
				_diaryService.Add(id, quantity);
			}
			
			return RedirectToAction("Index");
		}
	}
}
