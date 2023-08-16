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
using System.Security.Claims;
using System.Net.Http.Headers;
using System.IO.Pipes;
using Microsoft.IdentityModel.Tokens;
using Chef_API.Filters.ActionFilters;

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
        [Route ("GetRecipes")]
        public async Task<ActionResult<IEnumerable<GetRecipeDto>>> GetRecipes([FromQuery] int siteNumber, string category, string dietCategory, bool lunchbox, string? searchValue)
        {
            try
            {
                var recipes = await _recipeRepository.GetRecipes(siteNumber, searchValue, category, dietCategory, lunchbox);
                if (recipes == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(recipes);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet]
        [Route("GetRecipe/{recipeId}")]
        [RecipeIdGreaterThanZero]
        public async Task<ActionResult<GetRecipeDto>> GetRecipe(int recipeId)
        {
            try
            {
                if(recipeId != 0) //TO DO - delete this, validation is done by action filter
                {
                    var content = await _recipeRepository.GetRecipe(recipeId);
                    if(content != null)
                    {
                        return Ok(content);
                    }
                    else
                    {
                        return BadRequest(); //TO DO - Propper exception handling
                    }
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
        [Route("GetRecipes/{userName}")]
        [UserNameValidation]
        public async Task<ActionResult<GetRecipeDto>> GetRecipe(string userName)
        {
            try
            {
                if (!userName.IsNullOrEmpty()) //TO DO - Added filter for userName validation, here is not needed anymore
                {
                    var content = await _recipeRepository.GetRecipes(userName);
                    if (content != null)
                    {
                        return Ok(content);
                    }
                    else
                    {
                        return BadRequest(); //TO DO - Propper exception handling
                    }
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
        [Route("RecipePhoto")]
        [RecipeIdGreaterThanZero]
        public async Task<IActionResult> GetRecipeImg([FromQuery]int recipeId)
        {
            try
            {
                //TO DO - Add exception handling
                var content = new MultipartFormDataContent();
                var recipeImgName = await _recipeRepository.GetRecipeImgName(recipeId);
                var path = Path.Combine(_config.GetValue<string>("Paths:RecipeImgPath"), recipeImgName);


                var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                
                var fileContent = new StreamContent(fs);
                var contentType = "image/jpeg"; 
                var file = File(fs, contentType);
                
                return file;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("Upload")]
        [Authorize]
        [UploadRecipeValidation]
        public async Task<ActionResult<int>> UploadRecipe([FromForm] RecipeDtoWrapped recipeDtoWrapped)
        {

            long maxFileSize = 1024 * 512 * 1;
            
            try
            {
                if (recipeDtoWrapped.RecipeImg.Length > 0 && 
                    recipeDtoWrapped.RecipeImg.Length < maxFileSize && 
                    recipeDtoWrapped.RecipeDtoJson.Length > 0 && 
                    recipeDtoWrapped.RecipeImg.ContentType == "image/jpeg") //TO DO - validation added in UploadRecipeValidation ActionFilter, can be deleted
                {

                    var recipeDtoJson = recipeDtoWrapped.RecipeDtoJson;

                    PostRecipeDto recipeDto = JsonSerializer.Deserialize<PostRecipeDto>(recipeDtoJson); //TO DO - Add custom validation to PostRecipeDto
                    if(recipeDto == null)
                    {
                        return BadRequest();
                    }
                    
                    
                    //Create repository for file storage
                    var trustedFileNameForFileStorage = Path.ChangeExtension(Path.GetRandomFileName(), "jpg");
                    var path = Path.Combine(_config.GetValue<string>("Paths:RecipeImgPath"), trustedFileNameForFileStorage);

                    await using FileStream fs = new(path, FileMode.Create);
                    await recipeDtoWrapped.RecipeImg.CopyToAsync(fs);
                    
                    if(!System.IO.File.Exists(path))
                    {
                        return BadRequest(); //TO DO - creat propper response
                    }
                    var uploadedRecipeDto = await _recipeRepository.UploadRecipe(recipeDto, trustedFileNameForFileStorage, User.Identity.Name);
                    return Ok(uploadedRecipeDto.Id);
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

    }
}
