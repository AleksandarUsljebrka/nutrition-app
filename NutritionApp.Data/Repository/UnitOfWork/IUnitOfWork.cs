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
		public void SaveChanges();
	}
}
