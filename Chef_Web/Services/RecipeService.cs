using Chef_Models.Dtos;
using Chef_Web.Services.Contracts;
using System.Net.Http.Json;

namespace Chef_Web.Services
{
    public class RecipeService:IRecipeService
    {
        private readonly HttpClient _httpClient;

        public RecipeService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }
        public async Task<IEnumerable<PostRecipeDto>> GetRecipes()
        {
            try
            {
                var response = await this._httpClient.GetAsync("api/v1/Recipe");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<PostRecipeDto>();
                    }
                    var x = await response.Content.ReadFromJsonAsync<IEnumerable<PostRecipeDto>>();
                    return x;
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


        public async Task<IEnumerable<PostRecipeDto>> GetUserRecipes(string UserName)
        {
            try
            {

                var response = await this._httpClient.GetAsync($"api/v1/Recipe/{UserName}");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<PostRecipeDto>();
                    }
                    var recipes = await response.Content.ReadFromJsonAsync<IEnumerable<PostRecipeDto>>();
                    return recipes;
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

        public async Task UploadRecipe(MultipartFormDataContent content)
        {
            var response = await _httpClient.PostAsync("api/v1/Recipe/Upload", content);
        }
    }
}
