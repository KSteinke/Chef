using Chef_API.Extentions;
using Chef_API.Repositories.Interfaces;
using Chef_Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Chef_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IChefRepository _chefRepository;
        public RecipeController(IRecipeRepository recipeRepository, IChefRepository chefRepository)
        {
            _recipeRepository = recipeRepository;
            _chefRepository = chefRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeDto>>> GetRecipes()
        {
            try
            {
                var recipes = await _recipeRepository.GetRecipes();
                var chefs = await _chefRepository.GetChefs();
                if(recipes == null)
                {
                    return NotFound();
                }
                else
                {
                    var recipesDto = recipes.ConvertoToDto(chefs);
                    return Ok(recipesDto);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
            
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<RecipeDto>>> UploadRecipe(RecipeDto uploadedRecipe)
        {

        }

    }
}
