using Chef_API.Data;
using Chef_API.Entities;
using Chef_Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Chef_API.Repositories.Interfaces
{
    public interface IRecipeRepository
    {
        Task<IEnumerable<GetRecipeDto>> GetRecipes(int siteNumber, string searchValue, string category, string dietCategory, bool lunchbox);
        Task<GetRecipeDto> GetRecipe(int recipeId);
        Task<GetRecipeDto> UploadRecipe(PostRecipeDto postRecipeDto, string recipePhotoUrl, string userName);
        Task<string> GetRecipeImgName(int recipeId);
        //Task UploadRecipeIngredient(RecipeIngredient recipeIngredient);


    }
}
