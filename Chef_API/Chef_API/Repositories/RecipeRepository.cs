using Chef_API.Data;
using Chef_API.Entities;
using Chef_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chef_API.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly ChefDBContext chefDBContext;
        public RecipeRepository(ChefDBContext chefDBContext)
        {
            this.chefDBContext = chefDBContext;
        }
        public async Task<IEnumerable<Recipe>> GetRecipes()
        {
            var recipes = await this.chefDBContext.Recipes.ToListAsync();
            return recipes;
        }
    }
}
