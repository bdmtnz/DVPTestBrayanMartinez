using DoubleVPartners.BackEnd.Domain.Common.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace DoubleVPartners.BackEnd.Infrastructure.Common.Persistence.DbContexts
{
    public class PgDbContext(DbContextOptions<PgDbContext> opts) : DbContext(opts)
    {
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfigurationsFromAssembly(
                typeof(PgDbContext).Assembly,
                type => type.Namespace is not null && type.Namespace.StartsWith("DoubleVPartners.BackEnd.Infrastructure.PgsDbContext"));

            ApplySoftDeleteGlobalQueryFilter(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void ApplySoftDeleteGlobalQueryFilter(ModelBuilder modelBuilder)
        {
            Expression<Func<ISoftDeletable, bool>> filterExpr = bm => bm.DeletedOnUtc == null;
            foreach (var mutableEntityType in modelBuilder.Model.GetEntityTypes())
            {
                if (mutableEntityType.ClrType.IsAssignableTo(typeof(ISoftDeletable)))
                {
                    var parameter = Expression.Parameter(mutableEntityType.ClrType);
                    var body = ReplacingExpressionVisitor.Replace(filterExpr.Parameters.First(), parameter, filterExpr.Body);
                    var lambdaExpression = Expression.Lambda(body, parameter);

                    mutableEntityType.SetQueryFilter(lambdaExpression);
                }
            }
        }

    }
}
