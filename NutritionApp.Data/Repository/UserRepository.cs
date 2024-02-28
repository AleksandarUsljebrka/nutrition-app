using NutritionApp.Data.Data;
using NutritionApp.Data.Repository.IRepository;
using NutritionApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionApp.Data.Repository
{
	public class UserRepository : Repository<User>, IUserRepository
	{
        public UserRepository(ApplicationDbContext _context):base(_context)
        {
            
        }
  
	}
}
