using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chef_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class TestController : ControllerBase
    {
        public TestController() 
        { 

        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok();
        }

        [HttpGet]
        [Route("/get2")]
        [Authorize]
        public async Task<IActionResult> Get2()
        {
            return Ok(); 
        }
    }
}
