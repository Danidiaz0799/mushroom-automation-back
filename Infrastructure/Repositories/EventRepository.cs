using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context;

        public EventRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetAllAsync(int page, int pageSize)
        {
            return await _context.Events
                .OrderByDescending(eventLog => eventLog.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Event?> GetByIdAsync(int id) =>
            await _context.Events.FindAsync(id);

        public async Task AddAsync(Event eventLog)
        {
            _context.Events.Add(eventLog);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Event eventLog)
        {
            _context.Events.Update(eventLog);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var eventLog = await _context.Events.FindAsync(id);
            if (eventLog != null)
            {
                _context.Events.Remove(eventLog);
                await _context.SaveChangesAsync();
            }
        }
    }
}
