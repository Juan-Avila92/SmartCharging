using System.Linq.Expressions;

namespace SmartCharging.API.Data.Repository.Contracts
{
    public interface IRepository
    {
        void Create<TEntity>(TEntity entity) where TEntity : class;
        TEntity GetById<TEntity>(Guid id) where TEntity : class;
        List<TEntity> GetAll<TEntity>() where TEntity : class;
        void Update<TEntity>(TEntity entity) where TEntity : class;
        Task DeleteById<TEntity>(Guid id) where TEntity : class;
        Task<bool> ExistAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        Task SaveChangesAsync();
    }
}
