using Microsoft.AspNetCore.Mvc;

namespace VinylAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody]RegisterUserDto dto)
        {

            
        }
    }
}
