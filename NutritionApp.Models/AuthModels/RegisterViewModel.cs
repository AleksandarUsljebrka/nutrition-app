using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace NutritionApp.Models.AuthModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Ime je obavezno!")]
		[DisplayName("Ime")]
		public string? Name { get; set; }

        [Required(ErrorMessage = "Email je obavezan!")]
		[DisplayName("Email adresa")]
		[DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
		[DisplayName("Šifra")]
		[Required(ErrorMessage = "Šifra je obavezna!")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Potvrda šifre je obavezna!")]
        [DisplayName("Potvrda Šifre")]
        [Compare("Password", ErrorMessage = "Šifre se ne poklapaju!")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
    }
}
