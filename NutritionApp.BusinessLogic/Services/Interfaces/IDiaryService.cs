using NutritionApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionApp.BusinessLogic.Services.Interfaces
{
	public interface IDiaryService
	{
		public void Add(int? foodId, float grams);
		public UserDiary GetDiary();
	}
}
