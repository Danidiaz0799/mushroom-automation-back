using Application.Dtos;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetAllAsync(int page, int pageSize);
        Task<Event?> GetByIdAsync(int id);
        Task AddAsync(EventDto dto);
        Task UpdateAsync(int id, EventDto dto);
        Task DeleteAsync(int id);
    }
}

