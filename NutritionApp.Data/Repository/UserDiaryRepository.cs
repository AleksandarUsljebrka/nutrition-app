using Microsoft.EntityFrameworkCore;
using NutritionApp.Data.Data;
using NutritionApp.Data.Repository.IRepository;
using NutritionApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NutritionApp.Data.Repository
{
	public class UserDiaryRepository : Repository<UserDiary>, IUserDiaryRepository
	{
		public UserDiaryRepository(ApplicationDbContext _context) : base(_context)
		{

		}
		public UserDiary GetDiaryIncludeFood(Expression<Func<UserDiary, bool>> filter)
		{
			return _context.Set<UserDiary>().Include(diary => diary.DailyFood).FirstOrDefault(filter);
		}
		public IEnumerable<UserDiary> GetAllDiariesIncludeFood(Expression<Func<UserDiary, bool>> filter)
		{
			return _context.Set<UserDiary>().Include(d => d.DailyFood).Where(filter).ToList();
		}
		public UserDiary GetDiaryById(Expression<Func<UserDiary, bool>> filter)
		{
			return _context.Set<UserDiary>().Include(d => d.DailyFood).FirstOrDefault(filter);

		}
		public int GetDiaryIdByFoodId(int? id)
		{
			var idReturn = _context.Set<FoodInDiary>().Where(f => f.Id == id).FirstOrDefault().UserDiaryId;
			return idReturn;
		}

		public void Update(UserDiary diary)
		{
			_context.UserDiaries.Update(diary);
		}
	}
}
