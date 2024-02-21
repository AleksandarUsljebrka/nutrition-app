using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutritionApp.Data;
using NutritionApp.Models;

namespace NutritionApp.Controllers
{
	[Authorize]
	public class FoodController : Controller
	{
		private ApplicationDbContext _context;
		public FoodController(ApplicationDbContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			List<Food> foods = _context.Food.ToList();
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
				//newFood.TypeOfFood = MappValue(newFood.TypeOfFood);

				_context.Food.Add(newFood);
				_context.SaveChanges();
				TempData["success"] = "Food added successfully!";

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
			Food? dbFood = _context.Food.Find(foodId);
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
				_context.Food.Update(editedFood);
				_context.SaveChanges();
				TempData["success"] = "Food updated successfully!";

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
			Food? dbFood = _context.Food.Find(foodId);
			if (dbFood == null)
			{
				return NotFound();
			}
			return View(dbFood);
		}

		[HttpPost,ActionName("DeleteFood")]
		public IActionResult DeleteFoodPOST(int? id)
		{
			Food? dbFood = _context.Food.Find(id);
			if(dbFood == null)
			{
				return NotFound();
			}
			_context.Food.Remove(dbFood);
			_context.SaveChanges();
			TempData["success"] = "Food deleted successfully!"; 
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
