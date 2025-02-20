using Application.Dtos;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IDhtSensorService
    {
        Task<IEnumerable<DhtSensor>> GetAllAsync(int page, int pageSize);
        Task<DhtSensor?> GetByIdAsync(int id);
        Task AddAsync(DhtSensorDto dto);
        Task UpdateAsync(int id, DhtSensorDto dto);
        Task DeleteAsync(int id);
    }
}
