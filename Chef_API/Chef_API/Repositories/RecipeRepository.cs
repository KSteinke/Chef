using Chef_API.Data;
using Chef_API.Entities;
using Chef_API.Extentions;
using Chef_API.Repositories.Interfaces;
using Chef_Models.Dtos;
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

        public async Task<Recipe> UploadRecipe(PostRecipeDto postRecipeDto, string recipePhotoUrl, string userName)
        {
            var userId = await _chefDBContext.Chefs.Where(chef => chef.UserName == userName).Select(x => x.Id).FirstAsync();
            var recipe = postRecipeDto.ConvertFromDto(recipePhotoUrl, userId);
            if (recipe != null)
            {
                var result = await _chefDBContext.Recipes.AddAsync(recipe);
                await _chefDBContext.SaveChangesAsync();
                
                return result.Entity;
            }

            return null;

        }


        public Recipe UploadRecipeTest(Recipe recipe)
        {
           var result = _chefDBContext.Recipes.Add(recipe);
           _chefDBContext.SaveChanges();
            return result.Entity;
        }

        public List<Ingredient> GetIngredients()
        {
            return _chefDBContext.Ingredients.ToList();
        }

        public async Task UploadRecipeIngredient(RecipeIngredient recipeIngredient)
        {
            await _chefDBContext.RecipeIngredients.AddAsync(recipeIngredient);
            await _chefDBContext.SaveChangesAsync();

            var sklanikiRecipe11 = _chefDBContext.RecipeIngredients
                .Where(r => r.RecipeId == 11)
                .Select(r => r.Ingredient).ToList();
        }
    }
}
