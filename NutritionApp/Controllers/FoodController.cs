using Microsoft.AspNetCore.Mvc;
using NutritionApp.Data;
using NutritionApp.Models;

namespace NutritionApp.Controllers
{
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
            _context.Add(newFood);
            _context.SaveChanges();
            return View();
        }
    }
} 
