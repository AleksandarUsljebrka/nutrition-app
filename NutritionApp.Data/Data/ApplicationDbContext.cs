using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NutritionApp.Models;

namespace NutritionApp.Data.Data
{
	public class ApplicationDbContext : IdentityDbContext<User>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}
		public DbSet<Food> Food { get; set; }
		//    public DbSet<User> User {  get; set; }  
		public DbSet<UserDiary> UserDiaries { get; set; }
		public DbSet<FoodInDiary> FoodInDiaries { get; set; }
	}
}
