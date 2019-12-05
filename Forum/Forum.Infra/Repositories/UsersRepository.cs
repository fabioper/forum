using Forum.Data.Contexts;
using Forum.Domain.Entities;
using Forum.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Forum.Infra.Repositories
{
    public class UsersRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<User> users;

        public UsersRepository(ApplicationDbContext context)
        {
            _context = context;
            users = context.Users;
        }

        public async Task<User> AddAsync(User entity)
        {
            await users.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<User>> GetAllAsync() => await Task.FromResult(users);

        public async Task<IEnumerable<User>> GetByAsync(string keyword) => throw new NotImplementedException();

        public async Task<User> GetByIdAsync(long id) =>
            await users.FirstOrDefaultAsync(u => u.Id == id);

        public async Task RemoveAsync(long id) => throw new NotImplementedException();

        public async Task<User> UpdateAsync(User entity) => throw new NotImplementedException();
    }
}
