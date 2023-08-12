using Chef_API.Entities;
using Chef_Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.IO;
using System.Net.Mime;

namespace Chef_API.Extentions
{
    public static class DtoConvertions
    {

        public static IEnumerable<IngredientDto> ConverToDto(this IEnumerable<Ingredient> ingredients)
        {
            IEnumerable<IngredientDto> ingredientsDto = (from ingredient in ingredients
                                                         select new IngredientDto
                                                         { 
                                                             Id = ingredient.Id,
                                                             Name = ingredient.Name,
                                                             Countable = ingredient.Countable
                                                         }).ToList();
            return ingredientsDto;
        }

        public static Recipe ConvertFromDto(this PostRecipeDto postRecipeDto, string recipePhotoUrl, int userId)
        {
            
            Recipe recipe = new Recipe
            {
                Name = postRecipeDto.Name,
                Description = postRecipeDto.Description,
                PrepDescription = postRecipeDto.PrepDescription,
                Category = postRecipeDto.Category,
                LunchBox = postRecipeDto.LunchBox,
                Diet_Category = postRecipeDto.Diet_Category,
                RecipePhotoURL = recipePhotoUrl,
                AuthorId = userId
            };
            return recipe;
        }

        

        public static GetRecipeDto ConvertToDto(this Recipe recipe)
        {
            List<IngredientDto> ingredientDtos = new List<IngredientDto>();
            foreach(var ingredient in recipe.RecipeIngredients)
            {
                IngredientDto ingredientDto = new IngredientDto
                {
                    Id = ingredient.IngredientId,
                    Name = ingredient.Ingredient.Name,
                    Countable = ingredient.Ingredient.Countable,
                    Quantity = ingredient.Quantity
                };
                ingredientDtos.Add(ingredientDto);
            }

            GetRecipeDto getRecipeDto = new GetRecipeDto
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Description = recipe.Description,
                PrepDescription = recipe.PrepDescription,
                Category = recipe.Category,
                LunchBox = recipe.LunchBox,
                Diet_Category = recipe.Diet_Category,
                AuthorName = recipe.Author.UserName,
                IngredientDtos = ingredientDtos
            };
            return getRecipeDto;
        }

        public static List<GetRecipeDto> ConvertToDto(this IEnumerable<Recipe> recipes)
        {
            List<GetRecipeDto> getRecipeDtos = new List<GetRecipeDto>();
            foreach(var recipe in recipes)
            {
               var getRecipeDto = recipe.ConvertToDto();
               getRecipeDtos.Add(getRecipeDto);
            }
            return getRecipeDtos;
        }

        public static List<RecipeIngredient> ConverFromDto(this List<IngredientDto> ingredientsDto, int recipeId)
        {
            List<RecipeIngredient> recipeIngredients = new List<RecipeIngredient>();
            foreach(var ingredientDto in ingredientsDto)
            {
                recipeIngredients.Add(new RecipeIngredient
                {
                    RecipeId = recipeId,
                    IngredientId = ingredientDto.Id,
                    Quantity = ingredientDto.Quantity ?? 0
                });
            }

            return recipeIngredients;
        }

    }
}
