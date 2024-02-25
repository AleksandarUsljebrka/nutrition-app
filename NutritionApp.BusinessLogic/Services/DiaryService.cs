using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NutritionApp.BusinessLogic.Services.Interfaces;
using NutritionApp.Data.Repository.UnitOfWork;
using NutritionApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionApp.BusinessLogic.Services
{
	public class DiaryService : IDiaryService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UserManager<User> _userManager;

		public DiaryService(IUnitOfWork unitOfWork, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
		{
			_unitOfWork = unitOfWork;
			_userManager = userManager;
			_httpContextAccessor = httpContextAccessor;
		}
		public void Add(int? foodId, float grams)
		{
			var currentUser = _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User).Result;

			var userDiary = _unitOfWork.UserDiaryRepository.GetAll().FirstOrDefault(d => d.UserForeignKeyId == currentUser.Id && d.DiaryDate.Date == DateTime.Now.Date);
			var food = _unitOfWork.FoodRepository.Get(f => f.Id == foodId);

			var calories = (food.Proteins * (grams / 100) * 4) + (food.Carbs * (grams / 100) * 4) + (food.Fat * (grams / 100) * 9);
			
			if (userDiary == null)
			{

				userDiary = new UserDiary
				{
					UserForeignKeyId = currentUser.Id,
					DiaryDate = DateTime.Now.Date,
					ProteinsSummary = UpdateMacros(grams, food.Proteins),
					CarbsSummary = UpdateMacros(grams, food.Carbs),
					FatSummary = UpdateMacros(grams, food.Fat),
					CaloriesSummary =calories,


				};
				_unitOfWork.UserDiaryRepository.Add(userDiary);
				_unitOfWork.SaveChanges();
			}
			else
			{
				userDiary.ProteinsSummary	+= UpdateMacros(grams, food.Proteins);
				userDiary.CarbsSummary	+= UpdateMacros(grams, food.Carbs);
				userDiary.FatSummary  += UpdateMacros(grams, food.Fat);
				userDiary.CaloriesSummary += calories;

				_unitOfWork.UserDiaryRepository.Update(userDiary);
				_unitOfWork.SaveChanges();
			}

			if (food != null)
			{
				var foodInDiary = new FoodInDiary
				{
					FoodId = food.Id,
					FoodName = food.FoodName,
					TypeOfFood = food.TypeOfFood,
					Proteins = food.Proteins * (grams / 100),
					Carbs = food.Carbs * (grams / 100),
					Fat = food.Fat * (grams / 100),
					UserDiaryId = userDiary.Id,
					Grams = grams,
					Calories = calories
				};

				_unitOfWork.FoodInDiaryRepository.Add(foodInDiary);
				_unitOfWork.SaveChanges();

				}
		
		}

		public UserDiary GetDiary()
		{
			UserDiary dbDiary = _unitOfWork.UserDiaryRepository.GetDiaryIncludeFood(d => d.DiaryDate == DateTime.Now.Date);
			
			if (dbDiary == null)
			{
				return null;
			}

			
			return dbDiary;
		}
	
		public FoodInDiary GetFoodInDiary(int? foodId)
		{
			FoodInDiary foodDiary = _unitOfWork.FoodInDiaryRepository.Get(f => f.Id == foodId);
			if(foodDiary == null)
			{
				return null;
			}
			return foodDiary;
		}

		public bool DeleteFoodFromDiary(int? foodId)
		{
			FoodInDiary foodInDiary = _unitOfWork.FoodInDiaryRepository.Get(f => f.Id == foodId);
			
			if(foodInDiary == null)
			{
				return false;
			}
			
			UserDiary userDiary = _unitOfWork.UserDiaryRepository.Get(d => d.Id == foodInDiary.UserDiaryId);

			SubDiaryMacros(userDiary, foodInDiary);

			_unitOfWork.UserDiaryRepository.Update(userDiary);
			_unitOfWork.FoodInDiaryRepository.Remove(foodInDiary);
			_unitOfWork.SaveChanges();

			return true;
		}

		public bool UpdateFoodFromDiary(int? foodId, float editedGrams)
		{
			FoodInDiary foodInDiary = _unitOfWork.FoodInDiaryRepository.Get(f => f.Id == foodId);
			var originalFood = _unitOfWork.FoodRepository.Get(f => f.Id == foodInDiary.FoodId);

			if (foodInDiary == null)
			{
				return false;
			}

			UserDiary userDiary = _unitOfWork.UserDiaryRepository.Get(d => d.Id == foodInDiary.UserDiaryId);

			var newProteins = UpdateMacros(editedGrams, originalFood.Proteins);
			var newCarbs = UpdateMacros(editedGrams, originalFood.Carbs);
			var newFat = UpdateMacros(editedGrams, originalFood.Fat);
			var newCalories = (originalFood.Proteins * (editedGrams / 100) * 4) + (originalFood.Carbs * (editedGrams / 100) * 4) + (originalFood.Fat * (editedGrams / 100) * 9);

			SubDiaryMacros(userDiary, foodInDiary);

			userDiary.ProteinsSummary += newProteins;
			userDiary.CarbsSummary += newCarbs;
			userDiary.FatSummary += newFat;
			userDiary.CaloriesSummary += newCalories;

			foodInDiary.Proteins = newProteins;
			foodInDiary.Carbs = newCarbs;
			foodInDiary.Fat = newFat;
			foodInDiary.Calories = newCalories;
			foodInDiary.Grams = editedGrams;

			_unitOfWork.UserDiaryRepository.Update(userDiary);	
			_unitOfWork.FoodInDiaryRepository.Update(foodInDiary);
			_unitOfWork.SaveChanges();

			return true;
		}

		//macros default indicate macros per 100g
		public float UpdateMacros(float grams, float macrosDefault)
		{
			return macrosDefault * (grams/100);
		}

		public void SubDiaryMacros(UserDiary userDiary, FoodInDiary foodInDiary)
		{

			userDiary.ProteinsSummary = (userDiary.ProteinsSummary - foodInDiary.Proteins) < 0 ? 0 : (userDiary.ProteinsSummary - foodInDiary.Proteins);
			userDiary.CarbsSummary = (userDiary.CarbsSummary - foodInDiary.Carbs) < 0 ? 0 : (userDiary.CarbsSummary - foodInDiary.Carbs);
			userDiary.FatSummary = (userDiary.FatSummary - foodInDiary.Fat) < 0 ? 0 : (userDiary.FatSummary - foodInDiary.Fat);
			userDiary.CaloriesSummary = (userDiary.CaloriesSummary - foodInDiary.Calories) < 0 ? 0 : (userDiary.CaloriesSummary - foodInDiary.Calories);

		}
	}

}
