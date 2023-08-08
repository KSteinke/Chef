using Chef_API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Chef_API.Data
{
    public class ChefDBContext:DbContext
    {
        public ChefDBContext(DbContextOptions<ChefDBContext> options):base(options) 
        {

        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Chef> Chefs { get; set; }
        public DbSet<Ingredient> Ingredients { get; set;}
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecipeIngredient>().HasKey(rI => new { rI.RecipeId, rI.IngredientId });
            
            modelBuilder.Entity<RecipeIngredient>()
                .HasOne<Recipe>(rI => rI.Recipe)
                .WithMany(r => r.RecipeIngredients)
                .HasForeignKey(rI => rI.RecipeId);

            modelBuilder.Entity<RecipeIngredient>()
                .HasOne<Ingredient>(rI => rI.Ingredient)
                .WithMany(i => i.RecipeIngredients)
                .HasForeignKey(rI => rI.IngredientId);
        }

    }
}
