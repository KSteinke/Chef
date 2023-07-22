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

        public LoginController(IChefRepository chefRepository, ITokenManager tokenManager)
        {
            _chefRepository = chefRepository;
            _tokenManager = tokenManager;
        }

        [HttpPost]
        [Route ("/Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto userCredentials)
        {
            try
            {
                if(! await _chefRepository.veryfiLoginCredentials(userCredentials))  
                {
                    ModelState.AddModelError("Unauthorized", "You are not authorized");
                    return Unauthorized(ModelState);
                }
                else
                {
                    
                    return  Ok(new { Token = _tokenManager.GenerateToken(userCredentials.UserName) }); //sending ok with token inside
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
