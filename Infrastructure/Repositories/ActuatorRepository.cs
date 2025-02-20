using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ActuatorRepository : IActuatorRepository
    {
        private readonly ApplicationDbContext _context;

        public ActuatorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Actuator>> GetAllAsync(int page, int pageSize)
        {
            return await _context.Actuators
                .OrderByDescending(actuator => actuator.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Actuator?> GetByIdAsync(int id) =>
            await _context.Actuators.FindAsync(id);

        public async Task AddAsync(Actuator actuator)
        {
            _context.Actuators.Add(actuator);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Actuator actuator)
        {
            var existingActuator = await _context.Actuators.FindAsync(actuator.Id);
            if (existingActuator != null)
            {
                existingActuator.Name = actuator.Name;
                existingActuator.State = actuator.State;
                existingActuator.Timestamp = actuator.Timestamp;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var actuator = await _context.Actuators.FindAsync(id);
            if (actuator != null)
            {
                _context.Actuators.Remove(actuator);
                await _context.SaveChangesAsync();
            }
        }
    }

}
