﻿using NutritionApp.Data.Data;
using NutritionApp.Data.Repository.IRepository;
using NutritionApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionApp.Data.Repository
{
	public class FoodRepository : Repository<Food>, IFoodRepository
	{
        public FoodRepository(ApplicationDbContext _context):base(_context)
        {
            
        }
		public ICollection<Food> GetAllByType(string type)
		{
			return _context.Set<Food>().Where(f => f.TypeOfFood == type).ToList();
		}

		public void Update(Food food)
		{
			_context.Food.Update(food);
		}
	}
}
