using Application.Dtos;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IActuatorService
    {
        Task<IEnumerable<Actuator>> GetAllAsync(int page, int pageSize);
        Task<Actuator?> GetByIdAsync(int id);
        Task AddAsync(ActuatorDto dto);
        Task UpdateAsync(int id, ActuatorDto dto);
        Task DeleteAsync(int id);
    }
}


