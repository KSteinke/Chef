using Chef_Models.Dtos;

namespace Chef_Web.Services.Contracts
{
    public interface IIngredientService
    {
        Task<IEnumerable<IngredientDto>> GetIngredients();
    }
}
