using Chef_Models.Dtos;

namespace Chef_Web.Services.Contracts
{
    public interface IIngredientService
    {
        public List<IngredientDto> GetIngredients();
    }
}
