using Chef_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chef_API.Data
{
    public class ChefDBContext:DbContext
    {
        public ChefDBContext(DbContextOptions<ChefDBContext> options):base(options) 
        {

        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Chef> Chefs { get; set; }
        
    }
}
