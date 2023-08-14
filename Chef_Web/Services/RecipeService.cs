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
        public async Task<List<PostRecipeDto>> GetRecipes(int siteNumber, string category, string dietCategory, bool lunchbox, string? searchValue)
        {
            try
            {
                var uriBuilder = new UriBuilder("https://localhost:44355/api/v1/Recipe/GetRecipes");

                // Add query parameters to the UriBuilder
                var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);
                query["siteNumber"] = siteNumber.ToString();
                query["category"] = category;
                query["dietCategory"] = dietCategory;
                query["lunchbox"] = lunchbox.ToString();
                if (!string.IsNullOrEmpty(searchValue))
                {
                    query["searchValue"] = searchValue;
                }
                uriBuilder.Query = query.ToString();

                // Get the full endpoint address with query parameters
                var endpointUrl = uriBuilder.ToString();

                var response = await this._httpClient.GetAsync(endpointUrl);
                
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return new List<PostRecipeDto>();
                    }
                    var x = await response.Content.ReadFromJsonAsync<List<PostRecipeDto>>();
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

        public async Task<GetRecipeDto> GetRecipe(int recipeId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/v1/Recipe/GetRecipe/{recipeId}");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return null;
                    }

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var content = await response.Content.ReadFromJsonAsync<GetRecipeDto>();
                        return content;
                    }
                    else
                    {
                        return null;
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

        public async Task<int> UploadRecipe(PostRecipeDto newPostRecipeDto, byte[] newRecipeImg)
        {
                try
                {
                    var content = new MultipartFormDataContent();

                    var imageStream = new MemoryStream(newRecipeImg);
                    // Create a StreamContent instance from the image stream
                    var streamContent = new StreamContent(imageStream);

                    // Set the content type of the stream (e.g., "image/jpeg")
                    streamContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");

                    // Add the StreamContent to the MultipartFormDataContent as a file part
                    content.Add(
                        content: streamContent,
                        name: "\"RecipeImg\"", 
                        fileName: "RecipeImg"); // Add as a part named "file" with filename "image.jpg"
                    
                                                                     //var fileContent = new StreamContent(newRecipeImg.OpenReadStream());

                //    fileContent.Headers.ContentType = new MediaTypeHeaderValue(newRecipeImg.ContentType);

                //    content.Add(
                //    content: fileContent,
                //    name: "\"RecipeImg\"",
                //    fileName: "RecipeImg");

                string recipeDtoJson = System.Text.Json.JsonSerializer.Serialize(newPostRecipeDto);

                    content.Add(new StringContent(recipeDtoJson), "RecipeDtoJson");

                    var token = await _tokenManager.GetTokenAsync();
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

                    var response = await _httpClient.PostAsync("api/v1/Recipe/Upload", content);
                    await imageStream.DisposeAsync();
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
