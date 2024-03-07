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
    }
}
