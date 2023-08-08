using Chef_Models.Dtos;

namespace Chef_Web.Services.Contracts
{
    public interface IRecipeService
    {
        Task<IEnumerable<PostRecipeDto>> GetRecipes();
        Task UploadRecipe(MultipartFormDataContent content);
    }
}
