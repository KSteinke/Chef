using Chef_Models.Dtos;
using Chef_Web.Services.Contracts;
using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace Chef_Web.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenManager _tokenManager;

        public RecipeService(HttpClient httpClient, ITokenManager tokenManager)
        {
            this._httpClient = httpClient;
            _tokenManager = tokenManager;
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

        public async Task<int> UploadRecipe(PostRecipeDto newPostRecipeDto, IBrowserFile newRecipeImg)
        {
            



                try
                {

                    var content = new MultipartFormDataContent();
                    var fileContent = new StreamContent(newRecipeImg.OpenReadStream());

                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(newRecipeImg.ContentType);

                    content.Add(
                    content: fileContent,
                    name: "\"RecipeImg\"",
                    fileName: "RecipeImg");

                    string recipeDtoJson = System.Text.Json.JsonSerializer.Serialize(newPostRecipeDto);

                    content.Add(new StringContent(recipeDtoJson), "RecipeDtoJson");

                    var token = await _tokenManager.GetTokenAsync();
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

                    var response = await _httpClient.PostAsync("api/v1/Recipe/Upload", content);

                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        {
                            return 0;
                        }
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var uploadedRecipeId = await response.Content.ReadFromJsonAsync<int>();
                            return uploadedRecipeId;
                        }
                        else
                        {
                            var message = await response.Content.ReadAsStreamAsync();
                            throw new Exception($"Http status: {response.StatusCode} Message-{message}");
                        }
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
