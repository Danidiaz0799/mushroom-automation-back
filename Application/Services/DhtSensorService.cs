using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class DhtSensorService : IDhtSensorService
    {
        private readonly IDhtSensorRepository _repository;

        public DhtSensorService(IDhtSensorRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<DhtSensor>> GetAllAsync(int page, int pageSize) =>
            await _repository.GetAllAsync(page, pageSize);

        public async Task<DhtSensor?> GetByIdAsync(int id) =>
            await _repository.GetByIdAsync(id);

        public async Task AddAsync(DhtSensorDto dto)
        {
            var sensor = new DhtSensor
            {
                Temperature = dto.Temperature,
                Humidity = dto.Humidity,
                Timestamp = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time")) // Set timestamp here
            };
            await _repository.AddAsync(sensor);
        }

        public async Task UpdateAsync(int id, DhtSensorDto dto)
        {
            var existingSensor = await _repository.GetByIdAsync(id);
            if (existingSensor != null)
            {
                existingSensor.Temperature = dto.Temperature;
                existingSensor.Humidity = dto.Humidity;
                await _repository.UpdateAsync(existingSensor);
            }
        }

        public async Task DeleteAsync(int id) =>
            await _repository.DeleteAsync(id);
    }
}
