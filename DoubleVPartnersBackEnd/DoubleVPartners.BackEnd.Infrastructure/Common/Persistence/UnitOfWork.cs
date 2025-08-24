using DoubleVPartners.BackEnd.Domain.Common.Contracts.Persistence;
using DoubleVPartners.BackEnd.Infrastructure.Common.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace DoubleVPartners.BackEnd.Infrastructure.Common.Persistence
{
    public class UnitOfWork(PgDbContext context) : IUnitOfWork
    {
        private IDbContextTransaction? _transaction;

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransaction()
        {
            _transaction = await context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            if (_transaction is not null)
            {
                await _transaction.CommitAsync();
            }

            await context.SaveChangesAsync();
        }

        public async Task RollbackAsync(CancellationToken cancellationToken)
        {
            await context.Database.RollbackTransactionAsync(cancellationToken);
        }

        public IGenericRepository<TD> GenericRepository<TD>()
            where TD : class
        {
            return new GenericRepository<TD>(context);
        }
    }
}
