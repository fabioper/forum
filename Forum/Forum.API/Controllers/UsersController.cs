using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Forum.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            var user = HttpContext.User;

            return Ok(new
            {
                message = "ok"
            });
        }
    }
}