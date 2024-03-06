using Microsoft.EntityFrameworkCore;
using SmartCharging.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SmartCharging.Infrastructure.Data
{
    public class SmartChargingContext : DbContext
    {
        public SmartChargingContext(DbContextOptions<SmartChargingContext> options) : base(options)
        {
        }
        public DbSet<SmartGroup> SmartGroups { get; set; }
        public DbSet<ChargeStation> ChargeStations { get; set; }
    }
}
