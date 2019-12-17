using Forum.API.ViewModels;
using Forum.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Azure.Storage.Blob
using Microsoft.Azure.Storage;

namespace Forum.API.Controllers
{
    [Route("api/")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;

        public UsersController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        [Route("profile")]
        public async Task<IActionResult> Profile()
        {
            var currentUser = await _usersRepository.GetCurrentUser(HttpContext.User);

            return Ok(currentUser);
        }

        [HttpPut("profile")]
        public async Task<IActionResult> Profile(EditUserViewModel vm)
        {
            return Ok();
        }
    }
}
