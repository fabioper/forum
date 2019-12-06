using Forum.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Domain.Interfaces
{
    public interface IUsersRepository : IRepository<User>
    {
        Task<User> GetCurrentUser(ClaimsPrincipal user);
    }
}
