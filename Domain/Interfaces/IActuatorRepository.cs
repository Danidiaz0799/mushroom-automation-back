using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IActuatorRepository
    {
        Task<IEnumerable<Actuator>> GetAllAsync(int page, int pageSize);
        Task<Actuator?> GetByIdAsync(int id);
        Task AddAsync(Actuator actuator);
        Task UpdateAsync(Actuator actuator);
        Task DeleteAsync(int id);
    }
}
