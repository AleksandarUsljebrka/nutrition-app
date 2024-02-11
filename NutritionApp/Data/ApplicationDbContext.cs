using Microsoft.EntityFrameworkCore;

namespace NutritionApp.Data
{
    public class ApplicationDbContext:DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
                
        }
    }
}
