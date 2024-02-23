using System;
using System.Collections.Generic;
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
		public string FoodName { get; set; }
		public string TypeOfFood { get; set; }
		public float Proteins { get; set; }
		public float Carbs { get; set; }
		public float Fat { get; set; }
		public int UserDiaryId { get; set; }
		public UserDiary? UserDiary { get; set; }
		public Food Food { get; set; }	
		public int FoodId { get; set; }	
	}
}
