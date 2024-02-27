using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NutritionApp.Models.AuthModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Korisničko ime je obavezno!")]
		[DisplayName("Korisničko ime")]
		public string? Username { get; set; }

        [Required(ErrorMessage = "Šifra je obavezna!")]
        [DataType(DataType.Password)]
		[DisplayName("Šifra")]
		public string? Password { get; set; }

        [DisplayName("Zapamti me")]
        public bool RememberMe { get; set; }
    }
}
