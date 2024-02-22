using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace NutritionApp.Models.AuthModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Name is required!")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required!")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required!")]
        [DisplayName("Confirm Password")]
        [Compare("Password", ErrorMessage = "Password don't match!")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
    }
}
