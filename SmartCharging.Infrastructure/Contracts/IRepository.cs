namespace SmartCharging.Infrastructure.Contracts
{
    public  interface IRepository
    {
        void Create<TEntity>(TEntity entity) where TEntity : class;

        Task SaveChangesAsync();
    }
}
