using Forum.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Forum.Domain.Interfaces
{
    public interface IRepository<T> where T : AbstractEntity
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(long id);

        Task<IEnumerable<T>> GetByAsync(string keyword);

        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task RemoveAsync(long id);
    }
}
