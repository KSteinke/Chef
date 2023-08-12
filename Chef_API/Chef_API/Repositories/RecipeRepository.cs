﻿using Chef_API.Data;
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

        public async Task<GetRecipeDto> GetRecipe(int recipeId)
        {
            
            var recipe = await _chefDBContext.Recipes.Where(r => r.Id == recipeId).Include(r => r.RecipeIngredients).ThenInclude(ri => ri.Ingredient).FirstOrDefaultAsync();
            var recipeAuthorName = await _chefDBContext.Chefs.Where(c => c.Id == recipe.AuthorId).Select(c => c.UserName).FirstOrDefaultAsync();
            if (recipe != null)
            {
                var getRecipeDto = recipe.ConvertToDto(recipeAuthorName);
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
                
                //var getRecipeDto = result.Entity.ConvertToDto(postRecipeDto.Ingredients);
                //return getRecipeDto;

                var uploadedRecipe = await _chefDBContext.Recipes.Where(r => r.Id == result.Entity.Id).Include(r => r.RecipeIngredients).ThenInclude(ri => ri.Ingredient).FirstOrDefaultAsync();
                var recipeAuthorName = await _chefDBContext.Chefs.Where(c => c.Id == uploadedRecipe.AuthorId).Select(c => c.UserName).FirstOrDefaultAsync();
                if (uploadedRecipe != null)
                {
                    var uploadedRecipeDto = uploadedRecipe.ConvertToDto(recipeAuthorName);

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


        //Tests
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

        }
    }
}
