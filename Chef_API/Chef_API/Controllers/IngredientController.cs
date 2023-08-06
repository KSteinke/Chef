using Chef_API.Entities;
using Chef_API.Extentions;
using Chef_API.Repositories.Interfaces;
using Chef_Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chef_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientRepository _ingredientRepostitory;

        public IngredientController(IIngredientRepository ingredientRepository)
        {
            _ingredientRepostitory = ingredientRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredientDto>>> GetIngredients()
        {
            try
            {
                var ingredients = await _ingredientRepostitory.GetIngredientsAsync();
                if (ingredients == null)
                {
                    return NotFound();
                }
                else
                {
                    var ingredientsDto = ingredients.ConverToDto();
                    return Ok(ingredientsDto);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }
        


    }
}
