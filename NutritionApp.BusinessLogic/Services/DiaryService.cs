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

			if (userDiary == null)
			{
				userDiary = new UserDiary
				{
					UserForeignKeyId = currentUser.Id,
					DiaryDate = DateTime.Now.Date,
					ProteinsSummary = food.Proteins * (grams / 100),
					CarbsSummary = food.Carbs * (grams / 100),
					FatSummary = food.Fat * (grams / 100),
					CaloriesSummary = ((food.Proteins * 4) + (food.Carbs * 4) + (food.Fat * 9))

				};
				_unitOfWork.UserDiaryRepository.Add(userDiary);
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
					UserDiaryId = userDiary.Id
				};

				_unitOfWork.FoodInDiaryRepository.Add(foodInDiary);

				//userDiary.ProteinsSummary += food.Proteins;
				//userDiary.CarbsSummary += food.Carbs;
				//userDiary.FatSummary += food.Fat;
				//userDiary.CaloriesSummary += ((food.Proteins * 4) + (food.Carbs * 4) + (food.Fat * 9));
			}
			_unitOfWork.SaveChanges();
			//Food? dbFood = _unitOfWork.FoodRepository.Get(f => f.Id == foodId);

			//FoodInDiary foodInDiary = new FoodInDiary();
			//foodInDiary.FoodId = dbFood.Id;
			//foodInDiary.FoodName = dbFood.FoodName;
			//foodInDiary.TypeOfFood = dbFood.TypeOfFood;
			//foodInDiary.Proteins = dbFood.Proteins;
			//foodInDiary.Carbs = dbFood.Carbs;
			//foodInDiary.Fat = dbFood.Fat;

			//_unitOfWork.FoodInDiaryRepository.Add(dbF);
		}

		public UserDiary GetDiary()
		{
			UserDiary dbDiary = _unitOfWork.UserDiaryRepository.Get(d => d.DiaryDate == DateTime.Now.Date);
			if (dbDiary == null)
			{
				return null;
			}
			return dbDiary;
		}
	}
}
