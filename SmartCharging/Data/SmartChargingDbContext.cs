using Microsoft.EntityFrameworkCore;
using SmartCharging.API.Domain.Models;

namespace SmartCharging.API.Data
{
    public class SmartChargingDbContext : DbContext
    {
        public SmartChargingDbContext(DbContextOptions<SmartChargingDbContext> options) : base(options)
        {
        }
        public DbSet<SmartGroup> SmartGroups { get; set; }
        public DbSet<ChargeStation> ChargeStations { get; set; }
        public DbSet<Connector> Connectors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define unique constraint for Connector identifier within a ChargeStation
            modelBuilder.Entity<Connector>()
                .HasIndex(c => new { c.ChargeStationId, c.ConnectorId })
                .IsUnique();

            // Additional configuration if needed

            base.OnModelCreating(modelBuilder);
        }
    }
}
