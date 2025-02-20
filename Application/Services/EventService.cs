using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _repository;

        public EventService(IEventRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Event>> GetAllAsync(int page, int pageSize) =>
            await _repository.GetAllAsync(page, pageSize);

        public async Task<Event?> GetByIdAsync(int id) =>
            await _repository.GetByIdAsync(id);

        public async Task AddAsync(EventDto dto)
        {
            var eventLog = new Event
            {
                Message = dto.Message,
                Timestamp = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time")) // Set timestamp here
            };
            await _repository.AddAsync(eventLog);
        }

        public async Task UpdateAsync(int id, EventDto dto)
        {
            var existingEvent = await _repository.GetByIdAsync(id);
            if (existingEvent != null)
            {
                existingEvent.Message = dto.Message;
                await _repository.UpdateAsync(existingEvent);
            }
        }

        public async Task DeleteAsync(int id) =>
            await _repository.DeleteAsync(id);
    }
}
