using NutritionApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NutritionApp.Data.Repository.IRepository
{
	public interface IUserDiaryRepository : IRepository<UserDiary>
	{
		public UserDiary GetDiaryIncludeFood(Expression<Func<UserDiary, bool>> filter);
		public void Update(UserDiary diary);
	}
}
