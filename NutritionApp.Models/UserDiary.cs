using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionApp.Models
{
	public class UserDiary
	{
		[Key]
		public int Id { get; set; }
		[DataType(DataType.DateTime)]
		public DateTime DiaryDate {  get; set; }
		public float? CaloriesSummary { get; set; }
		public float? ProteinsSummary { get; set; }
		public float? CarbsSummary { get; set; }
		public float? FatSummary { get; set; }
		public User User { get; set; }
		public int UserForeignKeyId { get; set; }
		public ICollection<FoodInDiary>? DailyFood { get; set; }

	}
}
