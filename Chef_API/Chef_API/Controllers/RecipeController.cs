using Chef_API.Extentions;
using Chef_API.Repositories.Interfaces;
using Chef_Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.IO;
using System;
using Microsoft.AspNetCore.Authorization;
using Chef_API.Entities;

namespace Chef_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IChefRepository _chefRepository;
        private readonly IConfiguration _config;
        public RecipeController(IRecipeRepository recipeRepository, IChefRepository chefRepository, IConfiguration config)
        {
            _recipeRepository = recipeRepository;
            _chefRepository = chefRepository;
            _config = config;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostRecipeDto>>> GetRecipes()
        {
            //try
            //{
            //    var recipes = await _recipeRepository.GetRecipes();
            //    var chefs = await _chefRepository.GetChefs();
            //    if (recipes == null)
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        var recipesDto = recipes.ConvertoToDto(chefs);
            //        return Ok(recipesDto);

            //    }
            //}
            //catch (Exception)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            //}
            return Ok();

        }

        [HttpPost]
        [Route("Upload")]
        public async Task<ActionResult> UploadRecipe([FromForm] RecipeDtoWrapped recipeDtoWrapped)
        {

            long maxFileSize = 1024 * 500;
            
            try
            {
                if (recipeDtoWrapped.RecipeImg.Length > 0 && 
                    recipeDtoWrapped.RecipeImg.Length < maxFileSize && 
                    recipeDtoWrapped.RecipeDtoJson.Length > 0 && 
                    recipeDtoWrapped.RecipeImg.ContentType == "image/jpeg")
                {

                    var recipeDtoJson = recipeDtoWrapped.RecipeDtoJson;
                    
                    PostRecipeDto recipeDto = JsonSerializer.Deserialize<PostRecipeDto>(recipeDtoJson);
                    
                    //TO DO - add validation for recipe Dto

                    var trustedFileNameForFileStorage = Path.ChangeExtension(Path.GetRandomFileName(), "jpg");
                    var path = Path.Combine(_config.GetValue<string>("Paths:RecipeImgPath"), trustedFileNameForFileStorage);

                    await using FileStream fs = new(path, FileMode.Create);
                    await recipeDtoWrapped.RecipeImg.CopyToAsync(fs);
                    
                    if(!System.IO.File.Exists(path))
                    {
                        return BadRequest(); //TO DO - creat propper response
                    }
                    //TO DO - Get UserName, Photo Url

                    //recipeDto.RecipePhotoURL = path;
                    //var newRecipe = recipeDto.ConvertFromDto();
                    
                    return Ok();

                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }



        [HttpGet]
        [Route("TEST")]
        public async Task<IActionResult> PostRecipeTest()
        {
            var Ingredients = _recipeRepository.GetIngredients();
            Recipe recipe = new Recipe
            {
                Name = "Test",
                Description = "Test",
                Category = "Test",
                LunchBox = true,
                AuthorId = 2,
                
            };
            Recipe result = _recipeRepository.UploadRecipeTest(recipe);
            foreach(var ingredient in Ingredients)
            {
                RecipeIngredient recipeIngredient = new RecipeIngredient()
                {
                    RecipeId = result.Id,
                    IngredientId = ingredient.Id,
                    Quantity = 5
                };

                await _recipeRepository.UploadRecipeIngredient(recipeIngredient);
                
                

            }
            

            return Ok();
        }


    }
}
