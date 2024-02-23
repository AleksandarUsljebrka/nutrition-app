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
	public class UserService : IUserService
	{
		private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public User GetUser(string username)
		{
			User? user = _unitOfWork.UserRepository.Get(u => u.UserName == username);
			return user;
		}
	}
}
