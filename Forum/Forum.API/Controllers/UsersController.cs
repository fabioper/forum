using Forum.API.ViewModels;
using Forum.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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

            return Ok(new ResponseMessage
            {
                Code = 200,
                Data = new
                {
                    user_id = currentUser.Id,
                    avatar_uri = currentUser.AvatarUri,
                    username = currentUser.UserName,
                    email = currentUser.Email,
                    topics = currentUser.Topics,
                    created_at = currentUser.CreatedAt,
                    updated_at = currentUser.UpdatedAt
                }
            });
        }
    }
}