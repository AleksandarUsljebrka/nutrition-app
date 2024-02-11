using Microsoft.AspNetCore.Mvc;

namespace NutritionApp.Controllers
{
    public class FoodController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddFood()
        {
            return View();
        }
    }
}
