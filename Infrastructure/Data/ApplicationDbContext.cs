using Microsoft.EntityFrameworkCore;
using Domain.Entities;


namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<DhtSensor> DhtSensors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DhtSensor>(entity =>
            {
                entity.ToTable("DhtSensors");

                entity.HasKey(e => e.Id); // Define la clave primaria

                entity.Property(e => e.Timestamp)
                      .IsRequired();

                entity.Property(e => e.Temperature)
                      .IsRequired()
                      .HasColumnType("decimal(5,2)");

                entity.Property(e => e.Humidity)
                      .IsRequired()
                      .HasColumnType("decimal(5,2)");
            });
        }
    }
}
