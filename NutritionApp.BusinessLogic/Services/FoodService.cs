using Microsoft.EntityFrameworkCore;
using NutritionApp.BusinessLogic.Services.Interfaces;
using NutritionApp.Data.Repository.IRepository;
using NutritionApp.Data.Repository.UnitOfWork;
using NutritionApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionApp.BusinessLogic.Services
{
	public class FoodService : IFoodService
	{

		private readonly IUnitOfWork _unitOfWork;
		public FoodService(IUnitOfWork unitOfWork)
        {
			_unitOfWork = unitOfWork;
		}
        public void AddFood(Food food)
		{
			_unitOfWork.FoodRepository.Add(food);
			_unitOfWork.SaveChanges();
		}

		public void DeleteFood(Food? food)
		{
			_unitOfWork.FoodRepository.Remove(food);
			_unitOfWork.SaveChanges();
		}

		public Food GetFood(int? id)
		{
			Food? dbFood = _unitOfWork.FoodRepository.Get(f => f.Id == id);
			return dbFood;
		}

		public List<Food> GetAllFood()
		{
			var foods = _unitOfWork.FoodRepository.GetAll();
			if(foods == null)
			{
				return null;
			}

			return foods.ToList();
		}
		public List<Food> GetFoodByType(string type)
		{
			IEnumerable<Food> filteredFoods = null;

			if (type.Equals("Sva Hrana"))
				filteredFoods = _unitOfWork.FoodRepository.GetAll();
			else
				filteredFoods = _unitOfWork.FoodRepository.GetAllByType(type);

			if (filteredFoods == null)
				return null;

			return filteredFoods.ToList();
		}

		public void UpdateFood(Food? food)
		{
			_unitOfWork.FoodRepository.Update(food);
			_unitOfWork.SaveChanges();
			
		}
	}
}
