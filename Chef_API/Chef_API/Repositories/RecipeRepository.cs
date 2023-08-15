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
        public async Task<IEnumerable<GetRecipeDto>> GetRecipes(int siteNumber, string searchValue, string category, string dietCategory, bool lunchbox)
        {
            //Query construction based on parameter values
            IQueryable<Recipe> query = _chefDBContext.Recipes;
            if(category != "All")
            {
                query = query.Where(r => r.Category == category);
            }

            if(dietCategory != "All")
            {
                query = query.Where(r => r.Diet_Category== dietCategory);
            }

            if(lunchbox != false)
            {
                query = query.Where(r => r.LunchBox == lunchbox);
            }

            if(!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(recipe => (EF.Functions.Like(recipe.Description, "%" + searchValue + "%") || EF.Functions.Like(recipe.Name, "%" + searchValue + "%") || EF.Functions.Like(recipe.PrepDescription, "%" + searchValue + "%")));
            }

            IEnumerable<Recipe> recipes = await query.Include(r => r.RecipeIngredients).ThenInclude(ri => ri.Ingredient).Include(c => c.Author).Skip(4 * siteNumber).Take(4).ToListAsync();

            if (recipes != null && recipes.Count() > 0)
            {
                var recipesDtos = recipes.ConvertToDto();
                if (recipesDtos != null)
                {
                    return recipesDtos;
                }
                return Enumerable.Empty<GetRecipeDto>();
            }
            return Enumerable.Empty<GetRecipeDto>();
        }
        public async Task<IEnumerable<GetRecipeDto>> GetRecipes(string userName)
        {
            if(await _chefDBContext.Chefs.Where(c => c.UserName == userName).AnyAsync())
            {
                var recipes = await _chefDBContext.Recipes.Where(c => c.Author.UserName == userName).Include(r => r.RecipeIngredients).ThenInclude(ri => ri.Ingredient).Include(c => c.Author).ToListAsync();
                if (recipes != null && recipes.Count() > 0)
                {
                    var recipesDtos = recipes.ConvertToDto();
                    if (recipesDtos != null)
                    {
                        return recipesDtos;
                    }
                    return Enumerable.Empty<GetRecipeDto>();
                }
                return Enumerable.Empty<GetRecipeDto>();
            }
            else
            {
                return Enumerable.Empty<GetRecipeDto>();
            }
        }



        public async Task<GetRecipeDto> GetRecipe(int recipeId)
        {
            
            var recipe = await _chefDBContext.Recipes.Where(r => r.Id == recipeId).Include(r => r.RecipeIngredients).ThenInclude(ri => ri.Ingredient).Include(c => c.Author).FirstOrDefaultAsync();
            
            if (recipe != null)
            {
                var getRecipeDto = recipe.ConvertToDto();
                return getRecipeDto;
            }
            else
            {
                return null;
            }
        }

        public async Task<GetRecipeDto> UploadRecipe(PostRecipeDto postRecipeDto, string recipePhotoUrl, string userName)
        {
            var userId = await _chefDBContext.Chefs.Where(chef => chef.UserName == userName).Select(x => x.Id).FirstAsync();
            var recipe = postRecipeDto.ConvertFromDto(recipePhotoUrl, userId);
            if (recipe != null)
            {
                var result = await _chefDBContext.Recipes.AddAsync(recipe);
                await _chefDBContext.SaveChangesAsync();


                foreach(var ingredient in postRecipeDto.Ingredients)
                {
                    RecipeIngredient recipeIngredient = new RecipeIngredient()
                    {
                        RecipeId = result.Entity.Id,
                        IngredientId = ingredient.Id,
                        Quantity = ingredient.Quantity ?? 0
                    };
                    
                    await UploadRecipeIngredient(recipeIngredient);

                }

                var uploadedRecipe = await _chefDBContext.Recipes.Where(r => r.Id == result.Entity.Id).Include(r => r.RecipeIngredients).ThenInclude(ri => ri.Ingredient).Include(c=>c.Author).FirstOrDefaultAsync();
                if (uploadedRecipe != null)
                {
                    var uploadedRecipeDto = uploadedRecipe.ConvertToDto();

                    return uploadedRecipeDto;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        public async Task<string> GetRecipeImgName(int recipeId)
        {
            var photoName = await _chefDBContext.Recipes.Where(r => r.Id == recipeId).Select(r => r.RecipePhotoURL).FirstOrDefaultAsync();
            return photoName;
        }

        private async Task UploadRecipeIngredient(RecipeIngredient recipeIngredient)
        {
            await _chefDBContext.RecipeIngredients.AddAsync(recipeIngredient);
            await _chefDBContext.SaveChangesAsync();
        }
    }
}
