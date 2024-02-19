using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NutritionApp.ViewModels
{
	public class LoginViewModel
	{
		[Required(ErrorMessage ="Username is required!")]
		public string? Username { get; set; }

		[Required(ErrorMessage ="Password is required!")]
		[DataType(DataType.Password)]	
		public string? Password { get; set; }

		[DisplayName("Remember Me")]
		public bool RememberMe { get; set; }
	}
}
