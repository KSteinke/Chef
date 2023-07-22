using Chef_Models.Dtos;
using Chef_Web.Services.Contracts;
using System.Net;
using System.Net.Http.Json;

namespace Chef_Web.Services
{
    
    public class LoginService : ILoginService
    {
        private readonly HttpClient _httpClient;

        public LoginService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> Login(LoginDto userCredentials)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync<LoginDto>("------------HTTP POST Login endpoint ", userCredentials); //TODO - Add api endpoint path
                if(response.IsSuccessStatusCode)
                {
                    if(response.StatusCode == HttpStatusCode.NoContent)
                    {
                        var message = await response.Content.ReadAsStreamAsync();
                        throw new Exception($"Http status: {response.StatusCode} Message-{message}");
                    }
                    
                    return await response.Content.ReadFromJsonAsync<string>();
                    
                }
                else
                {
                    var message = await response.Content.ReadAsStreamAsync();
                    throw new Exception($"Http status: {response.StatusCode} Message-{message}");
                }
            }
            catch (Exception)
            {

                throw;
            }

            throw new NotImplementedException();
        }
    }
}
