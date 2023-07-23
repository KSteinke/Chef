using Chef_Models.Dtos;
using Chef_Web.Services.Contracts;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Chef_Web.Services
{
    
    public class LoginService : ILoginService
    {
        private readonly HttpClient _httpClient;
        private readonly ISiteState _siteState;
        private readonly ICookieManager _cookieManager;

        public LoginService(HttpClient httpClient, ISiteState siteState, ICookieManager cookieManager)
        {
            _httpClient = httpClient;
            _siteState = siteState;
            _cookieManager = cookieManager;
        }

        public async Task Login(LoginDto userCredentials)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync<LoginDto>("api/v1/Login", userCredentials); //TODO - Add api endpoint path
                if(response.IsSuccessStatusCode)
                {
                    if(response.StatusCode == HttpStatusCode.NoContent)
                    {
                        var message = await response.Content.ReadAsStreamAsync();
                        throw new Exception($"Http status: {response.StatusCode} Message-{message}");
                    }

                    if(response.StatusCode == HttpStatusCode.OK)
                    {
                        var token = await response.Content.ReadAsStringAsync();
                        
                        _cookieManager.SetValueAsync("Authorization", token);
                        _cookieManager.SetValueAsync("UserName", userCredentials.UserName);
                        _siteState.LoginUserCheck();
                        
                    }
                    else
                    {
                        var message = await response.Content.ReadAsStreamAsync();
                        throw new Exception($"Http status: {response.StatusCode} Message-{message}");
                    }

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

            
        }


        public async Task<bool> Test()
        {
            try
            {
                
                var token = await _cookieManager.GetValueAsync("Authorization");
                //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                //var response = await _httpClient.GetAsync("/get2");
                var request = new HttpRequestMessage(HttpMethod.Get, "/get2");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        var message = await response.Content.ReadAsStreamAsync();
                        throw new Exception($"Http status: {response.StatusCode} Message-{message}");
                    }

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        
                        return true;
                    }
                    else
                    {
                        var message = await response.Content.ReadAsStreamAsync();
                        throw new Exception($"Http status: {response.StatusCode} Message-{message}");
                    }

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


        }



    }
}
