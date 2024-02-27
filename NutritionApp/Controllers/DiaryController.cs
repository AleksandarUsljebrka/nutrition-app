using Microsoft.AspNetCore.Mvc;
using NutritionApp.BusinessLogic.Services;
using NutritionApp.BusinessLogic.Services.Interfaces;
using NutritionApp.Data.Repository.IRepository;
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
			UserDiary diary = _diaryService.GetTodaysDiary();
			if(diary == null)
			{
				ViewBag.NotFound = "Trenutno nema dnevnika.";
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
				TempData["error"] = "Greška prilikom dodavanja hrane!";

				return NotFound();
			}
			var grams = Request.Form["grams"];
			if (float.TryParse(grams, out float quantity))
			{
				
				_diaryService.Add(id, quantity);
			}
			TempData["success"] = "Hrana uspešno dodata!";

			return RedirectToAction("Index");
		}

		public IActionResult DeleteFood(int? foodId)
		{

			if (foodId == null || foodId == 0)
			{
				return NotFound();
			}
			FoodInDiary? dbFood = _diaryService.GetFoodInDiary(foodId);
			if (dbFood == null)
			{
				return NotFound();
			}
			return View(dbFood);
		}

		[HttpPost, ActionName("DeleteFood")]
		public IActionResult DeleteFoodPOST(int? id)
		{
			//FoodInDiary? dbFoodDiary = _diaryService.GetFoodInDiary(id);
			if (id == null)
			{
				TempData["error"] = "Greška prilikom brisanja hrane!";

				return NotFound();
			}

			if (!_diaryService.DeleteFoodFromDiary(id))
			{
				TempData["error"] = "Greška prilikom brisanja hrane!";

				return NotFound();
			}

			TempData["success"] = "Hrana uspešno obrisana!";
			return RedirectToAction("Index");

		}

		public  IActionResult EditFood(int? foodId)
		{
			

			if (foodId == null || foodId == 0)
			{
				return NotFound();
			}
			FoodInDiary? dbFood = _diaryService.GetFoodInDiary(foodId);
			if (dbFood == null)
			{
				return NotFound();
			}
			
			return View(dbFood);
		}


		[HttpPost, ActionName("EditFood")]
		public IActionResult EditFoodPOST(int? id, float grams)
		{
		
			if (id == null)
			{
				TempData["error"] = "Greška prilikom izmene hrane!";

				return NotFound();
			}

			if (!_diaryService.UpdateFoodFromDiary(id, grams))
			{
				TempData["error"] = "Greška prilikom izmene hrane!";

				return NotFound();
			}
			

			TempData["success"] = "Hrana uspešno izmenjena!";
			
			return RedirectToAction("Index");

		}
		public IActionResult AllDiaries()
		{
            List<UserDiary> diaries = _diaryService.GetAllDiaries();
            if (diaries == null || diaries.Count <= 0)
            {
                ViewBag.NotFound = "Trenutno nema dnevnika.";
                return View();
            }
            return View(diaries);
        }

		public IActionResult DiaryDetails(int? id)
		{
			UserDiary diary = _diaryService.GetDiaryById(id);
			if (diary == null)
			{
				ViewBag.NotFound = "Trenutno nema dnevnika.";
				return View();
			}
			return View(diary);
		}


		public IActionResult EditHistoryFood(int? foodId)
		{


			if (foodId == null || foodId == 0)
			{
				return NotFound();
			}
			FoodInDiary? dbFood = _diaryService.GetFoodInDiary(foodId);
			if (dbFood == null)
			{
				return NotFound();
			}

			return View(dbFood);
		}


		[HttpPost, ActionName("EditHistoryFood")]
		public IActionResult EditHistoryFoodPOST(int? id, float grams)
		{

			if (id == null)
			{
				TempData["error"] = "Greška prilikom izmene hrane!";

				return NotFound();
			}

			if (!_diaryService.UpdateFoodFromDiary(id, grams))
			{
				TempData["error"] = "Greška prilikom izmene hrane!";

				return NotFound();
			}


			TempData["success"] = "Hrana uspešno izmenjena!";

			return RedirectToAction("DiaryDetails", new { id = _diaryService.GetDiaryIdByFoodId(id) });

		}

		public IActionResult DeleteDiary(int? id)
		{

			if (id == null || id == 0)
			{
				return NotFound();
			}
			UserDiary? diary = _diaryService.GetDiaryById(id);
			if (diary == null)
			{
				return NotFound();
			}
			return View(diary);
		}

		[HttpPost, ActionName("DeleteDiary")]
		public IActionResult DeleteDiaryPOST(int? id)
		{
			if (id == null)
			{
				TempData["error"] = "Greška prilikom brisanja hrane!";

				return NotFound();
			}

			if (!_diaryService.DeleteDiary(id))
			{
				TempData["error"] = "Greška prilikom brisanja hrane!";

				return NotFound();
			}

			TempData["success"] = "Hrana uspešno obrisana!";
			return RedirectToAction("AllDiaries");

		}

	}
}
