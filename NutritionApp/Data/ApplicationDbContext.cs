using Microsoft.EntityFrameworkCore;
using NutritionApp.Models;

namespace NutritionApp.Data
{
    public class ApplicationDbContext:DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
                
        }
        public DbSet<Food> Food { get; set; }
    }
}
