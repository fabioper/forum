using Forum.Domain.Entities;
using Forum.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Infra.Repositories
{
    public class TopicsRepository : ITopicsRepository
    {
        public Task<Topic> AddAsync(Topic entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Topic>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Topic>> GetByAsync(string keyword)
        {
            throw new NotImplementedException();
        }

        public Task<Topic> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Topic> UpdateAsync(Topic entity)
        {
            throw new NotImplementedException();
        }
    }
}
