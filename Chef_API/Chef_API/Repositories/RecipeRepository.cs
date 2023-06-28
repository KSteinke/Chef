using Chef_API.Data;
using Chef_API.Entities;
using Chef_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chef_API.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly ChefDBContext _chefDBContext;
        public RecipeRepository(ChefDBContext chefDBContext)
        {
            _chefDBContext = chefDBContext;
        }
        public async Task<IEnumerable<Recipe>> GetRecipes()
        {
            var recipes = await _chefDBContext.Recipes.ToListAsync();
            return recipes;
        }
    }
}
