namespace DoubleVPartners.BackEnd.Domain.Common.Contracts.Persistence
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync(CancellationToken cancellationToken);
        Task BeginTransaction();
        Task CommitAsync();
        Task RollbackAsync(CancellationToken cancellationToken);
        IGenericRepository<TD> GenericRepository<TD>() where TD : class;
    }
}
