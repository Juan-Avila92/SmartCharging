using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SmartCharging.API.Data.Repository.Contracts;
using SmartCharging.API.Domain.Models;
using System.Linq.Expressions;

namespace SmartCharging.API.Data.Repository.Repos
{
    public class SmartGroupsRepository : ISmartGroupsRepository
    {
        protected readonly SmartChargingDbContext _dbContext;
        public SmartGroupsRepository(SmartChargingDbContext context)
        {
            _dbContext = context;
            _dbContext.Database.EnsureCreated();
        }

        public List<SmartGroup> GetAllWithChargeStations()
        {
            var entities = _dbContext.Set<SmartGroup>()
                .Include(x => x.ChargeStations)
                .ThenInclude(x => x.Connectors)
                .ToList();

            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            return entities;
        }

        public List<ChargeStation> GetChargeStationsById(Guid id)
        {
            var entities = _dbContext.Set<ChargeStation>()
                .Where(x => x.SmartGroupId == id)
                .ToList();

            if (entities == null)
                return new List<ChargeStation>();

            return entities;
        }

        public int GetSumOfAllConnectorsAmpValuesById(Guid id)
        {
            var value = _dbContext.Set<SmartGroup>()
                .Include(x => x.ChargeStations)
                .ThenInclude(x => x.Connectors)
                .Single(x => x.SmartGroupId.Equals(id))
                .ChargeStations
                .SelectMany(x => x.Connectors)
                .Sum(x => x.MaxCurrentInAmps);
                
            return value;
        }
    }
}
