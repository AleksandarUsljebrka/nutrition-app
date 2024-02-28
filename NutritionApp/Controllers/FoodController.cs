using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutritionApp.BusinessLogic.Services.Interfaces;
using NutritionApp.Data;
using NutritionApp.Data.Data;
using NutritionApp.Data.Repository.IRepository;
using NutritionApp.Data.Repository.UnitOfWork;
using NutritionApp.Models;

namespace NutritionApp.Controllers
{
    [Authorize]
	public class FoodController : Controller
	{
		private readonly IFoodService _foodService;
		public FoodController(IFoodService foodService)
		{
			_foodService = foodService;
		}
		public IActionResult Index()
		{
			string typeOfFood = TempData["typeOfFood"] as string;

			List<Food> filteredFoods = string.IsNullOrEmpty(typeOfFood) ? 
											  _foodService.GetAllFood() : 
											  _foodService.GetFoodByType(typeOfFood);

			ViewBag.typeOfFood = typeOfFood;
			return View(filteredFoods);
		}

		public IActionResult FilterFood(string? typeOfFood)
		{
			if(typeOfFood == null)
			{
				TempData["error"] = "Greška prilikom pristupa podacima!";
				return RedirectToAction("Index");
			}
			if (typeOfFood.Equals("Sva Hrana"))
			{
				TempData["typeOfFood"] = "Sva Hrana";
			}
		
			else
			{
				TempData["typeOfFood"] = typeOfFood;
			}


			return RedirectToAction("Index");
		}

		public IActionResult AddFood()
		{
			return View();
		}

		[HttpPost]
		public IActionResult AddFood(Food newFood)
		{
			if (ModelState.IsValid)
			{
				_foodService.AddFood(newFood);
				TempData["success"] = "Hrana uspešno dodata!";

				return RedirectToAction("Index");
			}
			TempData["error"] = "Greška prilikom dodavanja hrane!";

			return View();
		}

		public IActionResult EditFood(int? foodId)
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
			return View(dbFood);
		}

		[HttpPost]
		public IActionResult EditFood(Food editedFood)
		{

			if (ModelState.IsValid)
			{
				_foodService.UpdateFood(editedFood);
				TempData["success"] = "Hrana uspešno izmenjena!";

				return RedirectToAction("Index");
			}
			TempData["error"] = "Greška prilikom izmene hrane!";

			return View();
		}

		public IActionResult DeleteFood(int? foodId)
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
			return View(dbFood);
		}

		[HttpPost,ActionName("DeleteFood")]
		public IActionResult DeleteFoodPOST(int? id)
		{
			Food? dbFood = _foodService.GetFood(id);
			if(dbFood == null)
			{
				TempData["error"] = "Greška prilikom brisanja hrane!";

				return RedirectToAction("Index");
			}

			_foodService.DeleteFood(dbFood);

			TempData["success"] = "Hrana uspešno obrisana!"; 
			return RedirectToAction("Index");

		}
		
	}
}
