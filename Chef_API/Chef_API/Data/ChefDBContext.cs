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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>()
                .HasMany(e => e.Ingredients)
                .WithMany(e => e.Recipes)
                .UsingEntity<RecipeIngredient>(
                j => j.HasOne(pt => pt.Ingredient)
                    .WithMany(t => t.RecipeIngredients)
                    .HasForeignKey(pt => pt.IngredientId),
                j => j.HasOne(pt => pt.Recipe)
                     .WithMany(t => t.RecipeIngredients)
                     .HasForeignKey(pt => pt.RecipeId),
                j => j.Property(pt => pt.Quantity)
                );
        }

    }
}
