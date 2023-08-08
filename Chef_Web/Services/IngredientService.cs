using Chef_Models.Dtos;
using Chef_Web.Services.Contracts;
using System.Net.Http.Json;

namespace Chef_Web.Services
{

    public class IngredientService : IIngredientService
    {
        //public List<IngredientDto> GetIngredients() //TO DO - Create API endpoint and HTTP GETIngredients
        //{

        //    //Mockup
        //    List<IngredientDto> ingredients = new List<IngredientDto>();

        //    for (int i = 1; i <= 10; i++)
        //    {
        //        ingredients.Add(new IngredientDto
        //        {
        //            Id = i,
        //            Name = "Ingredient " + i,
        //            Countable = i % 2 == 0, // Przykładowa logika ustalająca, czy składnik jest liczalny

        //        });
        //    }

        //    return ingredients;
        //}

        private readonly HttpClient _httpClient;

        public IngredientService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }


        public async Task<IEnumerable<IngredientDto>> GetIngredients()
        {
            try
            {
                var response = await this._httpClient.GetAsync("api/v1/Ingredient");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<IngredientDto>();
                    }
                    var ingredients = await response.Content.ReadFromJsonAsync<IEnumerable<IngredientDto>>();
                    return ingredients;
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
