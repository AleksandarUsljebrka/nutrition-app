using NutritionApp.Data.Data;
using NutritionApp.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionApp.Data.Repository.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private ApplicationDbContext _context;
		public UnitOfWork(ApplicationDbContext context)
		{
			_context = context;
			FoodRepository = new FoodRepository(_context);
			UserRepository = new UserRepository(_context);
			UserDiaryRepository = new UserDiaryRepository(_context);	
			FoodInDiaryRepository = new FoodInDiaryRepository(_context);
		}
		public IFoodRepository FoodRepository { get; set; }
		public IUserRepository UserRepository { get; set; }
		public IUserDiaryRepository UserDiaryRepository { get; set; }
		public IFoodInDiaryRepository FoodInDiaryRepository { get; set; }

		public void SaveChanges()
		{
			_context.SaveChanges();
		}
	}
}
