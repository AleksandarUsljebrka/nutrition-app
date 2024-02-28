using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionApp.Models
{
	public class FoodInDiary
	{
		[Key]
		public int Id {  get; set; }

		[DisplayName("Količina (g)")]
		[Required(ErrorMessage = "Molimo Vas unesite količinu hrane.")]
		public float Grams { get; set; }

		[DisplayName("Naziv Hrane")]
		public string FoodName { get; set; }

		[DisplayName("Tip Hrane")]
		public string TypeOfFood { get; set; }

		[DisplayName("Proteini (g)")]
		public float Proteins { get; set; }
		
        [DisplayName("Ugljeni Hidrati (g)")]
		public float Carbs { get; set; }

		[DisplayName("Masti (g)")]
		public float Fat { get; set; }

		[DisplayName("Kalorije(kcal)")]
		public float Calories { get; set; }


		public int UserDiaryId { get; set; }
		public UserDiary? UserDiary { get; set; }
		public Food Food { get; set; }	
		public int FoodId { get; set; }	
	}
}
