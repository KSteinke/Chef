using Chef_Models.Dtos;
using Microsoft.AspNetCore.Components.Forms;

namespace Chef_Web.Services.Contracts
{
    public interface IRecipeService
    {
        Task<IEnumerable<PostRecipeDto>> GetRecipes();
        Task UploadRecipe(PostRecipeDto newPostRecipeDto, IBrowserFile newRecipeImg);
    }
}
