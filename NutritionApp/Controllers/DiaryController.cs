using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
				TempData["error"] = "Greška prilikom pokušaja pristupa!";
				return RedirectToAction("Index");
			}
			Food? dbFood = _foodService.GetFood(foodId);

			if (dbFood == null)
			{
				TempData["error"] = "Greška prilikom pokušaja pristupa!";
				return RedirectToAction("Index");
			}

			AddFoodToDiaryModel newDiaryFood = 
								new AddFoodToDiaryModel(dbFood.Id,
														dbFood.FoodName,
														0,
														dbFood.Proteins,
														dbFood.Carbs,
														dbFood.Fat,
														dbFood.TypeOfFood);

			return View(newDiaryFood);
		}
		[HttpPost, ActionName("AddToDiary")]
		public IActionResult AddToDiaryPOST(AddFoodToDiaryModel addFoodToDiaryModel)
		{

			if (!ModelState.IsValid)
			{
				TempData["error"] = "Greška prilikom dodavanja hrane!";

				return RedirectToAction("Index");
			}
			
				
			_diaryService.AddFoodToDiary(addFoodToDiaryModel.Id, addFoodToDiaryModel.Grams);
			
			TempData["success"] = "Hrana uspešno dodata!";

			return RedirectToAction("Index");
		}

		public IActionResult DeleteFood(int? foodId)
		{

			if (foodId == null || foodId == 0)
			{
				TempData["error"] = "Greška prilikom pokušaja pristupa!";
				return RedirectToAction("Index");
			}
			FoodInDiary? dbFood = _diaryService.GetFoodInDiary(foodId);
			if (dbFood == null)
			{
				TempData["error"] = "Greška prilikom pokušaja pristupa!";
				return RedirectToAction("Index");
			}
			return View(dbFood);
		}

		[HttpPost, ActionName("DeleteFood")]
		public IActionResult DeleteFoodPOST(int? id)
		{

			if (id == null)
			{
				TempData["error"] = "Greška prilikom brisanja hrane!";

				return RedirectToAction("Index");
			}

			if (!_diaryService.DeleteFoodFromDiary(id))
			{
				TempData["error"] = "Greška prilikom brisanja hrane!";
				
				return RedirectToAction("Index");
			}

			TempData["success"] = "Hrana uspešno obrisana!";
			return RedirectToAction("Index");

		}

		public  IActionResult EditFood(int? foodId)
		{
			

			if (foodId == null || foodId == 0)
			{
				TempData["error"] = "Greška prilikom pokušaja pristupa!";
				return RedirectToAction("Index");
			}
			FoodInDiary? dbFood = _diaryService.GetFoodInDiary(foodId);
			if (dbFood == null)
			{
				TempData["error"] = "Greška prilikom pokušaja pristupa!";
				return RedirectToAction("Index");
			}
			
			return View(dbFood);
		}


		[HttpPost, ActionName("EditFood")]
		public IActionResult EditFoodPOST(int? id, float grams)
		{
		
			if (id == null)
			{
				TempData["error"] = "Greška prilikom izmene hrane!";
				
				return RedirectToAction("Index");
			}

			if (!_diaryService.UpdateFoodFromDiary(id, grams))
			{
				TempData["error"] = "Greška prilikom izmene hrane!";

				return RedirectToAction("Index");
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
				TempData["error"] = "Greška prilikom pokušaja pristupa!";
				return RedirectToAction("AllDiaries");
			}
			FoodInDiary? dbFood = _diaryService.GetFoodInDiary(foodId);
			if (dbFood == null)
			{
				TempData["error"] = "Greška prilikom pokušaja pristupa!";
				return RedirectToAction("AllDiaries");
			}

			return View(dbFood);
		}


		[HttpPost, ActionName("EditHistoryFood")]
		public IActionResult EditHistoryFoodPOST(int? id, float grams)
		{

			if (id == null)
			{
				TempData["error"] = "Greška prilikom izmene hrane!";

				return RedirectToAction("AllDiaries");
			}

			if (!_diaryService.UpdateFoodFromDiary(id, grams))
			{
				TempData["error"] = "Greška prilikom izmene hrane!";

				return RedirectToAction("AllDiaries");
			}


			TempData["success"] = "Hrana uspešno izmenjena!";

			return RedirectToAction("DiaryDetails", new { id = _diaryService.GetDiaryIdByFoodId(id) });

		}
		public IActionResult DeleteHistoryFood(int? foodId)
		{

			if (foodId == null || foodId <= 0)
			{
				TempData["error"] = "Greška prilikom pokušaja pristupa!";
			
				return RedirectToAction("AllDiaries");
			}
			FoodInDiary? dbFood = _diaryService.GetFoodInDiary(foodId);
			if (dbFood == null)
			{
				TempData["error"] = "Greška prilikom pokušaja pristupa!";
				return RedirectToAction("AllDiaries");
			}
			return View(dbFood);
		}

		[HttpPost, ActionName("DeleteHistoryFood")]
		public IActionResult DeleteHistoryFoodPOST(int? id)
		{

			if (id == null)
			{
				TempData["error"] = "Greška prilikom brisanja hrane!";
				return RedirectToAction("AllDiaries");
			}
			var diaryid = _diaryService.GetDiaryIdByFoodId(id);

			if (!_diaryService.DeleteFoodFromDiary(id))
			{
				TempData["error"] = "Greška prilikom brisanja hrane!";
				
				return RedirectToAction("AllDiaries");
			}

			if (diaryid == -1 || diaryid == null)
			{
				TempData["error"] = "Greška prilikom brisanja hrane!";

				return RedirectToAction("AllDiaries");

			}
			TempData["success"] = "Hrana uspešno obrisana!";
			return RedirectToAction("DiaryDetails", new { id = diaryid });


		}
		public IActionResult DeleteDiary(int? id)
		{

			if (id == null || id == 0)
			{
				TempData["error"] = "Greška prilikom pokušaja pristupa!";
				return RedirectToAction("AllDiaries");
			}
			UserDiary? diary = _diaryService.GetDiaryById(id);
			if (diary == null)
			{
				TempData["error"] = "Greška prilikom pokušaja pristupa!";
				return RedirectToAction("AllDiaries");
			}
			return View(diary);
		}

		[HttpPost, ActionName("DeleteDiary")]
		public IActionResult DeleteDiaryPOST(int? id)
		{
			if (id == null)
			{
				TempData["error"] = "Greška prilikom brisanja dnevnika!";
				
				return RedirectToAction("AllDiaries");
			}

			if (!_diaryService.DeleteDiary(id))
			{
				TempData["error"] = "Greška prilikom brisanja dnevnika!";
				
				return RedirectToAction("AllDiaries");
			}

			TempData["success"] = "Dnevnik uspešno obrisan!";
			return RedirectToAction("AllDiaries");

		}

	}
}
