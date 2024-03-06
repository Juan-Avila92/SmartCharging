namespace SmartCharging.Infrastructure.Contracts
{
    public  interface IRepository
    {
        void Create<TEntity>(TEntity entity) where TEntity : class;
        Task DeleteById<TEntity>(Guid id) where TEntity : class;
        Task SaveChangesAsync();
    }
}
