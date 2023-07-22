using Chef_Models.Dtos;
using System.Net.Http.Json;
namespace Chef_Web.Services
{
    public class ButtonTestService : IButtonTestService
    {
        private readonly HttpClient _httpClient;

        public ButtonTestService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task ButtonTest()
        {
            try
            {
                var response = await this._httpClient.GetAsync("api/Test");
               
            }
            catch (Exception)
            {

                throw;
            }
            
            
        }
    }
}
