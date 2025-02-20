using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class DhtSensorRepository : IDhtSensorRepository
    {
        private readonly ApplicationDbContext _context;

        public DhtSensorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DhtSensor>> GetAllAsync() =>
            await _context.DhtSensors.ToListAsync();

        public async Task<IEnumerable<DhtSensor>> GetAllAsync(int page, int pageSize)
        {
            return await _context.DhtSensors
                .OrderByDescending(sensor => sensor.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<DhtSensor?> GetByIdAsync(int id) =>
            await _context.DhtSensors.FindAsync(id);

        public async Task AddAsync(DhtSensor sensor)
        {
            _context.DhtSensors.Add(sensor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DhtSensor sensor)
        {
            _context.DhtSensors.Update(sensor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var sensor = await _context.DhtSensors.FindAsync(id);
            if (sensor != null)
            {
                _context.DhtSensors.Remove(sensor);
                await _context.SaveChangesAsync();
            }
        }
    }
}
