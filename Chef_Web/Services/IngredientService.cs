using Chef_Models.Dtos;
using Chef_Web.Services.Contracts;

namespace Chef_Web.Services
{
    public class IngredientService : IIngredientService
    {
        public List<IngredientDto> GetIngredients() //TO DO - Create API endpoint and HTTP GETIngredients
        {

            //Mockup
            List<IngredientDto> ingredients = new List<IngredientDto>();

            for (int i = 1; i <= 10; i++)
            {
                ingredients.Add(new IngredientDto
                {
                    Id = i,
                    Name = "Ingredient " + i,
                    Countable = i % 2 == 0, // Przykładowa logika ustalająca, czy składnik jest liczalny
                    
                });
            }

            return ingredients;
        }
    }
}
