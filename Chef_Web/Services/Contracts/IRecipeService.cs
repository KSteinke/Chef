using Chef_Models.Dtos;
using Microsoft.AspNetCore.Components.Forms;

namespace Chef_Web.Services.Contracts
{
    public interface IRecipeService
    {
        Task<List<GetRecipeDto>> GetRecipes(int siteNumber, string category, string dietCategory, bool lunchbox, string? searchValue);
        Task<GetRecipeDto> GetRecipe(int recipeId);
        Task<int> UploadRecipe(PostRecipeDto newPostRecipeDto, byte[] newRecipeImg);
    }
}
