using Chef_API.Entities;

namespace Chef_API.Repositories.Interfaces
{
    public interface IIngredientRepository
    {
        IQueryable<Ingredient> GetIngredients();
        Task<IEnumerable<Ingredient>> GetIngredientsAsync();
    }
}
