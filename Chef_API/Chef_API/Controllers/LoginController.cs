using Chef_API.Repositories;
using Chef_API.Repositories.Interfaces;
using Chef_API.TokenAuthentication.Interfaces;
using Chef_Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chef_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController] 
    public class LoginController : ControllerBase
    {
        private readonly IChefRepository _chefRepository;
        private readonly ITokenManager _tokenManager;

        public LoginController(IChefRepository chefRepository)
        {
            _chefRepository = chefRepository;
        }

        [HttpPost]
        [Route ("/Login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginDto userCredentials)
        {
            try
            {
                if(! await _chefRepository.veryfiLoginCredentials(userCredentials))  //TODO - refactor veryfiLoginCredentials Method that it will return Chef model based on given credentials. Then generate token based on them. If chef doesn't exist in db return null.
                {
                    return Unauthorized();
                }
                else
                {
                    return  _tokenManager.GenerateToken();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
