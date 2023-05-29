using Chef_API.Entities;
using Chef_Models.Dtos;

namespace Chef_API.Extentions
{
    public static class DtoConvertions
    {
        /// <summary>
        /// Converts IEnumerable that cointains Recipe objects to IEnumerable that contains RecipeDto objects
        /// </summary>
        /// <param name="recipies">IEnumerable list of Recipe objects</param>
        /// <returns></returns>
        public static IEnumerable<RecipeDto> ConvertoToDto(this IEnumerable<Recipe> recipies)
        {
            IEnumerable<RecipeDto> recipesDto = new List<RecipeDto>();


            return (from recipe in recipies
                    select new RecipeDto
                    {
                        Id = recipe.Id,
                        Name = recipe.Name,
                        Description = recipe.Description,
                        Category = recipe.Category,
                        LunchBox = recipe.LunchBox,
                        Diet_Category = recipe.Diet_Category,
                        RecipePhotoURL = recipe.RecipePhotoURL,
                        Author = recipe.Category
                    }).ToList();

        }
    }
}
