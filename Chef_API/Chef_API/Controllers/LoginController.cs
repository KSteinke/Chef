using Chef_API.Repositories;
using Chef_API.Repositories.Interfaces;
using Chef_API.TokenAuthentication.Interfaces;
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
        /// <summary>
        /// Api end point for login function,
        /// user credentials check
        /// if passed token is generated
        /// </summary>
        /// <param name="userCredentials"></param>
        /// <returns></returns>
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
                    //Response.Cookies.Append("Token", token, new CookieOptions { HttpOnly = true, Secure = true, SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None });
                   //Response.Cookies.Append("User", userCredentials.UserName, new CookieOptions { HttpOnly = false, Secure = false, SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None });
                    
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
