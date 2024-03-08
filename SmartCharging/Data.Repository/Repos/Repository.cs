using Microsoft.EntityFrameworkCore;
using SmartCharging.API.Data.Repository.Contracts;
using System.Linq.Expressions;

namespace SmartCharging.API.Data.Repository.Repos
{
    public class Repository : IRepository
    {
        protected readonly SmartChargingDbContext _dbContext;
        public Repository(SmartChargingDbContext context)
        {
            _dbContext = context;
            _dbContext.Database.EnsureCreated();
        }

        public TEntity Create<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return _dbContext.Set<TEntity>().Add(entity).Entity;
        }

        public TEntity GetById<TEntity>(Guid id) where TEntity : class
        {
            var entity = _dbContext.Set<TEntity>().Find(id);

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return entity;
        }
        public TEntity GetById<TEntity>(int id) where TEntity : class
        {
            var entity = _dbContext.Set<TEntity>().Find(id);

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return entity;
        }

        public List<TEntity> GetAll<TEntity>() where TEntity : class
        {
            var entities = _dbContext.Set<TEntity>().ToList();

            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            return entities;
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbContext.Set<TEntity>().Update(entity);
        }

        public async Task DeleteById<TEntity>(Guid id) where TEntity : class
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbContext.Set<TEntity>().Remove(entity);
        }

        public async Task DeleteById<TEntity>(int id) where TEntity : class
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbContext.Set<TEntity>().Remove(entity);
        }

        public async Task<bool> ExistAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return await _dbContext.Set<TEntity>().AnyAsync(predicate);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
