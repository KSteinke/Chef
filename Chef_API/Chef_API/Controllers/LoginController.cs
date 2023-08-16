using Chef_API.Repositories;
using Chef_API.Repositories.Interfaces;
using Chef_API.Services.TokenAuthentication.Interfaces;
using Chef_Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Net;

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
        [Route ("/api/v1/Login")]
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
                    var token = _tokenManager.GenerateToken(userCredentials.UserName);
                    
                    return Ok(token);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route ("/api/v1/Register")]
        public async Task<IActionResult> Register([FromBody] LoginDto userCredentials)
        {
            try
            {
                if(userCredentials == null)
                {
                    ModelState.AddModelError("Conflict", "User alredy registred");//TO DO - use propper statuses
                    return Conflict(ModelState);
                }

                if(await _chefRepository.CheckUserExist(userCredentials))
                {
                    ModelState.AddModelError("Conflict", "User alredy registred");
                    return Conflict(ModelState);
                }

                var newUser = await _chefRepository.RegisterUser(userCredentials);
                if(newUser == null)
                {
                    ModelState.AddModelError("Conflict", "User alredy registred");
                    return Conflict(ModelState);
                }
                else
                {
                    var token = _tokenManager.GenerateToken(newUser.UserName);

                    return Ok(token);
                    
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
       
    }
}
