using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<DhtSensor> DhtSensors { get; set; }
        public DbSet<Event> Events { get; set; } // Add this line

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DhtSensor>(entity =>
            {
                entity.ToTable("DhtSensors");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Timestamp)
                      .IsRequired();

                entity.Property(e => e.Temperature)
                      .IsRequired()
                      .HasColumnType("decimal(5,2)");

                entity.Property(e => e.Humidity)
                      .IsRequired()
                      .HasColumnType("decimal(5,2)");
            });

            modelBuilder.Entity<Event>(entity => // Add this block
            {
                entity.ToTable("Events");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Message)
                      .IsRequired();

                entity.Property(e => e.Timestamp)
                      .IsRequired();
            });
        }
    }
}
