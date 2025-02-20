using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class ActuatorService : IActuatorService
    {
        private readonly IActuatorRepository _repository;

        public ActuatorService(IActuatorRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Actuator>> GetAllAsync(int page, int pageSize) =>
            await _repository.GetAllAsync(page, pageSize);

        public async Task<Actuator?> GetByIdAsync(int id) =>
            await _repository.GetByIdAsync(id);

        public async Task AddAsync(ActuatorDto dto)
        {
            var actuator = new Actuator
            {
                Name = dto.Name,
                State = dto.State,
                Timestamp = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time")) // Set timestamp here
            };
            await _repository.AddAsync(actuator);
        }

        public async Task UpdateAsync(int id, ActuatorDto dto)
        {
            var existingActuator = await _repository.GetByIdAsync(id);
            if (existingActuator != null)
            {
                existingActuator.Name = dto.Name;
                existingActuator.State = dto.State;
                await _repository.UpdateAsync(existingActuator);
            }
        }

        public async Task DeleteAsync(int id) =>
            await _repository.DeleteAsync(id);
    }
}


