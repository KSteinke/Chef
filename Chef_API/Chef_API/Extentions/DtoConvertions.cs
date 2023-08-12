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
        /// <summary>
        /// Converts IEnumerable that cointains Recipe objects to IEnumerable that contains RecipeDto objects
        /// </summary>
        /// <param name="recipies">IEnumerable list of Recipe objects</param>
        /// <returns></returns>
        //public static IEnumerable<PostRecipeDto> ConvertoToDto(this IEnumerable<Recipe> recipies, IEnumerable<Chef> chefs)
        //{
        //    //IEnumerable<RecipeDto> recipesDto = new List<RecipeDto>();


        //    IEnumerable<PostRecipeDto> recipesDto = (from recipe in recipies
        //                                         join chef in chefs
        //                                         on recipe.AuthorId equals chef.Id

        //                                         select new PostRecipeDto
        //                                         {
        //                                             Id = recipe.Id,
        //                                             Name = recipe.Name,
        //                                             Description = recipe.Description,
        //                                             Category = recipe.Category,
        //                                             LunchBox = recipe.LunchBox,
        //                                             Diet_Category = recipe.Diet_Category,
        //                                             AuthorName = chef.UserName,
        //                                             PrepDescription = recipe.PrepDescription
        //                                         }).ToList();

        //    return recipesDto;

        //}

        public static string ReadImgByte(string imgUrl)
        {
            try
            {
                string recipeImgPath = Path.Combine(Config.RecipeImgPath, imgUrl);
                byte[] recipeImgBytes = File.ReadAllBytes(recipeImgPath);
                string recipeImgBase64 = Convert.ToBase64String(recipeImgBytes, 0, recipeImgBytes.Length);
                return recipeImgBase64;

            }
            catch (Exception)
            {

                throw;
            }

        }

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

        //public static GetRecipeDto ConvertToDto(this Recipe recipe, IEnumerable<IngredientDto> ingredientDtos)
        //{
        //    GetRecipeDto getRecipeDto = new GetRecipeDto
        //    {
        //        Id = recipe.Id,
        //        Name = recipe.Name,
        //        Description = recipe.Description,
        //        PrepDescription = recipe.PrepDescription,
        //        Category= recipe.Category,
        //        LunchBox = recipe.LunchBox,
        //        Diet_Category = recipe.Diet_Category,
        //        AuthorId = recipe.AuthorId,
        //        IngredientDtos = ingredientDtos
                
        //    };

        //    return getRecipeDto;
        //}

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
                AuthorId = recipe.AuthorId,
                IngredientDtos = ingredientDtos
            };
            return getRecipeDto;
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
