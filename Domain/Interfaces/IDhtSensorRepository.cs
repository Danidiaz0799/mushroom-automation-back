using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IDhtSensorRepository
    {
        Task<IEnumerable<DhtSensor>> GetAllAsync(int page, int pageSize);
        Task<DhtSensor?> GetByIdAsync(int id);
        Task AddAsync(DhtSensor sensor);
        Task UpdateAsync(DhtSensor sensor);
        Task DeleteAsync(int id);
    }
}
