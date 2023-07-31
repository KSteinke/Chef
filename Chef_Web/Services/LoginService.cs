using Chef_Models.Dtos;
using Chef_Web.Services.Contracts;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Chef_Web.Services
{
    
    public class LoginService : ILoginService
    {
        private readonly HttpClient _httpClient;
        private readonly ICookieManager _cookieManager;
        public IAuthManager _jwtAuthManager;
        public AuthenticationStateProvider _authStateProvider;
        private readonly ITokenManager _tokenManager;
        public LoginService(HttpClient httpClient, ICookieManager cookieManager, IAuthManager jwtAuthManager, AuthenticationStateProvider authStateProvider, ITokenManager tokenManager)
        {
            _httpClient = httpClient;
            _cookieManager = cookieManager;
            _jwtAuthManager = jwtAuthManager;
            _authStateProvider = authStateProvider;
            _tokenManager = tokenManager;
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
                        await _tokenManager.SetTokenAsync(token);
                        ((AuthStateProviderService)_authStateProvider).NotifyUserAuthentication(token);
                        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
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

        public async Task Logout()
        {
            await _tokenManager.DisposeToken();
            ((AuthStateProviderService)_authStateProvider).NotifyUserLogout();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }





    }
}
