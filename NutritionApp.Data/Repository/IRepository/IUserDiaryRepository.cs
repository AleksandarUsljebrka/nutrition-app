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
		public IEnumerable<UserDiary> GetAllDiariesIncludeFood(Expression<Func<UserDiary, bool>> filter);
		public UserDiary GetDiaryById(Expression<Func<UserDiary, bool>> filter);
		public int GetDiaryIdByFoodId(int? id);

		public void Update(UserDiary diary);
	}
}
