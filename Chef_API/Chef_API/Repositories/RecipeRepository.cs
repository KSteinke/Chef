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
                var recipeIngredients = postRecipeDto.Ingredients.ConverFromDto(result.Entity.Id);
                foreach(var recipeIngredient in recipeIngredients)
                {
                    //TO DO - przerobić tak żeby najpierw konwertuję do List<IngredientRecipe> 
                }
                return result.Entity;
            }

            return null;

        }


    }
}
