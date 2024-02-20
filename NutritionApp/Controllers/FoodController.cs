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
			Dictionary<string, string> valueMappings = new Dictionary<string, string>
			{
				{ "meatFishEggs", "Meat, Fish and eggs" },
				{ "dairy", "Dairy product" },
				{ "fruit", "Fruits" },
				{ "vegetables", "Vegetables" },
				{ "grains", "Grains" },
				{ "legumes", "Legumes" },
				{ "nuts", "Nuts and Seeds" },
				{ "oils", "Oils and Fats" },
				{ "sweets", "Sweets and Snacks" },
				{ "beverages", "Beverages" },

			};

			
			if (ModelState.IsValid)
			{
				if (valueMappings.ContainsKey(newFood.TypeOfFood))
				{
					newFood.TypeOfFood = valueMappings[newFood.TypeOfFood];

				}
				_context.Add(newFood);
				_context.SaveChanges();
				return RedirectToAction("Index");
			}
			return View();
		}

		public IActionResult EditFood(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Food dbFood = _context.Food.Find(id);
			if (dbFood == null )
			{
				return NotFound();
			}
			return View(dbFood);
		}

	}
}
