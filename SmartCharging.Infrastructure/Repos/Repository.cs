using SmartCharging.Infrastructure.Contracts;
using SmartCharging.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCharging.Infrastructure.Repos
{
    public class Repository : IRepository
    {
        protected readonly SmartChargingContext _dbContext;
        public Repository(SmartChargingContext context) 
        { 
            _dbContext = context;
            _dbContext.Database.EnsureCreated();
        }

        public void Create<TEntity>(TEntity entity) where TEntity : class
        {
            if(entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbContext.Set<TEntity>().Add(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
