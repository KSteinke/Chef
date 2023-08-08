﻿using Chef_API.Data;
using Chef_API.Entities;
using Chef_Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Chef_API.Repositories.Interfaces
{
    public interface IRecipeRepository
    {
        Task<IEnumerable<Recipe>> GetRecipes();

        

        //Tests
        Recipe UploadRecipeTest(Recipe recipe);


        List<Ingredient> GetIngredients();
        Task UploadRecipeIngredient(RecipeIngredient recipeIngredient);


    }
}
