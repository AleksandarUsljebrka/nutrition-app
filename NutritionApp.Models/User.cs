using Microsoft.AspNetCore.Identity;

namespace NutritionApp.Models
{
    public class User : IdentityUser
    {
        public string? Name { get; set; }

    }
}
