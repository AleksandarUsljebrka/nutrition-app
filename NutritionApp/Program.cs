using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using NutritionApp.BusinessLogic.Services;
using NutritionApp.BusinessLogic.Services.Interfaces;
using NutritionApp.Data;
using NutritionApp.Data.Data;
using NutritionApp.Data.Repository;
using NutritionApp.Data.Repository.IRepository;
using NutritionApp.Data.Repository.UnitOfWork;
using NutritionApp.Models;

public class Program
{
	public static async Task Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddControllersWithViews();
		builder.Services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
		builder.Services.AddIdentity<User, IdentityRole>(
			options =>
			{
				options.Password.RequiredUniqueChars = 0;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequiredLength = 6;
				options.Password.RequireUppercase = false;
				options.Password.RequireLowercase = false;
			}
			).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

		//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedEmail = false)
		//	.AddRoles<IdentityRole>()
		//	.AddEntityFrameworkStores<ApplicationDbContext>();

		//builder.Services.AddIdentityCore<User>()
		//	.AddUserManager<UserManager<User>>()
		//	.AddEntityFrameworkStores<ApplicationDbContext>();

		builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
		builder.Services.AddScoped<IFoodRepository, FoodRepository>();
		builder.Services.AddScoped<IFoodInDiaryRepository, FoodInDiaryRepository>();
		builder.Services.AddScoped<IUserDiaryRepository, UserDiaryRepository>();
		builder.Services.AddScoped<IFoodService, FoodService>();
		builder.Services.AddScoped<IUserService, UserService>();
		builder.Services.AddScoped<IDiaryService, DiaryService>();




		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Home/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}

		app.UseHttpsRedirection();
		app.UseStaticFiles();

		app.UseRouting();
		app.UseAuthentication();
		app.UseAuthorization();

		app.MapControllerRoute(
			name: "default",
			pattern: "{controller=Home}/{action=Index}/{id?}");

		using (var scope = app.Services.CreateScope())
		{
			var services = scope.ServiceProvider;


			var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

			var roles = new []{ "Admin", "User" };
			foreach (var role in roles)
			{

				if (!await roleManager.RoleExistsAsync(role))
				{
					await roleManager.CreateAsync(new IdentityRole(role));					
				}
			}

		}

		//using (var scope = app.Services.CreateScope())
		//{
		//	var services = scope.ServiceProvider;


		//	var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

		//	string email = "admin@gmail.com";
		//	string password = "Admin_123";

		//	if (await userManager.FindByEmailAsync(email) == null)
		//	{
		//		var user = new IdentityUser();
		//		user.UserName = email;

		//		await userManager.CreateAsync(user, password);

		//		await userManager.AddToRoleAsync(user, "Admin");
		//	}

		//}

		app.Run();
	}
}