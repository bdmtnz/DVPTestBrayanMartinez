using DoubleVPartners.BackEnd.Domain.Common.Contracts.Persistence;
using DoubleVPartners.BackEnd.Infrastructure.Common.Interceptors;
using DoubleVPartners.BackEnd.Infrastructure.Common.Persistence;
using DoubleVPartners.BackEnd.Infrastructure.Common.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DoubleVPartners.BackEnd.Infrastructure
{
    public static class Dependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddPersistence(configuration);

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var persistenceSettingsSection = configuration.GetSection(nameof(PersistenceSettings));
            var persistenceSettings = persistenceSettingsSection.Get<PersistenceSettings>();

            ArgumentNullException.ThrowIfNull(persistenceSettings, nameof(persistenceSettings));

            //services.Configure<PersistenceSettings>(persistenceSettingsSection);

            services.AddDbContext<PgDbContext>(opts =>
            {
                opts.UseNpgsql(persistenceSettings.PgConnection, opt => opt.SetPostgresVersion(17, 0))
                    .AddInterceptors(new SoftDeleteInterceptor(), new UpdateAuditableInterceptor());
            });

            return services;
        }
    }
}
