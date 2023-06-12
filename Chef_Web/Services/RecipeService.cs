using Chef_Models.Dtos;
using Chef_Web.Services.Contracts;
using System.Net.Http.Json;

namespace Chef_Web.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly HttpClient httpClient;

        public RecipeService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<IEnumerable<RecipeDto>> GetRecipes()
        {
            try
            {
                var response = await this.httpClient.GetAsync("api/Recipe");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<RecipeDto>();
                    }
                    return await response.Content.ReadFromJsonAsync<IEnumerable<RecipeDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }

            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }
    }
}
