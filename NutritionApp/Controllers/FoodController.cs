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
			List<Food> foods = _foodService.GetAllFood();
			return View(foods);
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
			return View();
		}

		public IActionResult EditFood(int? foodId)
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

		[HttpPost]
		public IActionResult EditFood(Food editedFood)
		{

			if (ModelState.IsValid)
			{
				_foodService.UpdateFood(editedFood);
				TempData["success"] = "Hrana uspešno izmenjena!";

				return RedirectToAction("Index");
			}
			return View();
		}

		public IActionResult DeleteFood(int? foodId)
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

		[HttpPost,ActionName("DeleteFood")]
		public IActionResult DeleteFoodPOST(int? id)
		{
			Food? dbFood = _foodService.GetFood(id);
			if(dbFood == null)
			{
				TempData["error"] = "Greška prilikom brisanja hrane!";

				return NotFound();
			}

			_foodService.DeleteFood(dbFood);

			TempData["success"] = "Hrana uspešno obrisana!"; 
			return RedirectToAction("Index");

		}
		//public string MappValue(string typeOfFood)
		//{
		//	Dictionary<string, string> valueMappings = new Dictionary<string, string>
		//	{
		//		{ "meatFishEggs", "Meat, Fish and eggs" },
		//		{ "dairy", "Dairy product" },
		//		{ "fruit", "Fruits" },
		//		{ "vegetables", "Vegetables" },
		//		{ "grains", "Grains" },
		//		{ "legumes", "Legumes" },
		//		{ "nuts", "Nuts and Seeds" },
		//		{ "oils", "Oils and Fats" },
		//		{ "sweets", "Sweets and Snacks" },
		//		{ "beverages", "Beverages" },

		//	};
		//	if (valueMappings.ContainsKey(typeOfFood))
		//	{
		//		typeOfFood = valueMappings[typeOfFood];

		//	}
		//	return typeOfFood;
		//}
	}
}
