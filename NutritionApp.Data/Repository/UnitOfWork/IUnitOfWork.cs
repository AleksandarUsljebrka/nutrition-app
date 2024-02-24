using NutritionApp.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionApp.Data.Repository.UnitOfWork
{
	public interface IUnitOfWork
	{
		public IFoodRepository FoodRepository { get; set; }
		public IUserRepository UserRepository { get; set; }
		public IUserDiaryRepository UserDiaryRepository { get; set; }
		public IFoodInDiaryRepository FoodInDiaryRepository { get; set; }

		public void SaveChanges();
	}
}
