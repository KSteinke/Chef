﻿using Chef_API.Entities;
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
        public static IEnumerable<RecipeDto> ConvertoToDto(this IEnumerable<Recipe> recipies, IEnumerable<Chef> chefs)
        {
            //IEnumerable<RecipeDto> recipesDto = new List<RecipeDto>();


            IEnumerable<RecipeDto> recipesDto =  (from recipe in recipies
                                                  join chef in chefs
                                                  on recipe.AuthorId equals chef.Id

                    select new RecipeDto
                    {
                        Id = recipe.Id,
                        Name = recipe.Name,
                        Description = recipe.Description,
                        Category = recipe.Category,
                        LunchBox = recipe.LunchBox,
                        Diet_Category = recipe.Diet_Category,
                        RecipePhotoURL = recipe.RecipePhotoURL,
                        AuthorName = chef.UserName
                    }).ToList();
            foreach (RecipeDto recipe in recipesDto)
            {
                //recipe.RecipeImg = ReadImg(recipe.RecipePhotoURL);
                //recipe.RecipeImg = ReadImgMultipart(recipe.RecipePhotoURL);
                recipe.RecipeImgBase64 = ReadImgByte(recipe.RecipePhotoURL);
            }
            return recipesDto;
            
        }

        public static string ReadImgByte(string imgUrl)
        {
            try
            {
                string recipeImgPath = Path.Combine(Config.RecipeImgPath, imgUrl);
                byte[] recipeImgBytes = File.ReadAllBytes(recipeImgPath);
                string recipeImgBase64 = Convert.ToBase64String(recipeImgBytes, 0, recipeImgBytes.Length);
                //File.WriteAllBytes("D:\\Programowanie\\Chef\\Images_Tests\\Test.jpg", Convert.FromBase64String(recipeImgBase64));
                return recipeImgBase64;

            }
            catch (Exception)
            {

                throw;
            }

        }



    }
}
