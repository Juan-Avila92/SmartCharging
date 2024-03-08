using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SmartCharging.API.Data.Repository.Contracts;
using SmartCharging.API.Domain.Models;
using System.Linq.Expressions;

namespace SmartCharging.API.Data.Repository.Repos
{
    public class ChargeStationRepository : IChargeStationRepository
    {
        protected readonly SmartChargingDbContext _dbContext;
        public ChargeStationRepository(SmartChargingDbContext context)
        {
            _dbContext = context;
            _dbContext.Database.EnsureCreated();
        }

        public ChargeStation GetChargeStationByIdWithConnectors(Guid id)
        {
            var chargeStationWithConnectors = _dbContext.Set<ChargeStation>()
                .Include(x => x.Connectors)
                .Single(chargeStation => chargeStation.ChargeStationId.Equals(id));

            if (chargeStationWithConnectors == null)
                throw new ArgumentNullException(nameof(chargeStationWithConnectors));

            return chargeStationWithConnectors;
        }

        public List<ChargeStation> GetAllWithConnectors()
        {
            var entities = _dbContext.Set<ChargeStation>()
                .Include(x => x.Connectors)
                .ToList();

            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            return entities;
        }
    }
}
