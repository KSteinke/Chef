using Chef_Models.Dtos;
using Microsoft.AspNetCore.Components.Forms;

namespace Chef_Web.Services.Contracts
{
    public interface IRecipeService
    {
        Task<IEnumerable<PostRecipeDto>> GetRecipes();
        Task<GetRecipeDto> GetRecipe(int recipeId);
        Task<int> UploadRecipe(PostRecipeDto newPostRecipeDto, IBrowserFile newRecipeImg);
    }
}
