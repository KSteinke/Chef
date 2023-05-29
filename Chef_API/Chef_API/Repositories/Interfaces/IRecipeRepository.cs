using Chef_API.Entities;

namespace Chef_API.Repositories.Interfaces
{
    public interface IRecipeRepository
    {
        Task<IEnumerable<Recipe>> GetRecipes(); 
    }
}
