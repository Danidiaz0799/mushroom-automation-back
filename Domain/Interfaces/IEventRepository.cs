using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAllAsync(int page, int pageSize);
        Task<Event?> GetByIdAsync(int id);
        Task AddAsync(Event sensor);
        Task UpdateAsync(Event sensor);
        Task DeleteAsync(int id);
    }
}
