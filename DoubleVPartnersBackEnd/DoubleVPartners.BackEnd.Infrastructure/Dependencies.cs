using DoubleVPartners.BackEnd.Domain.Common.Contracts.Persistence;
using DoubleVPartners.BackEnd.Domain.Common.Contracts.Security;
using DoubleVPartners.BackEnd.Infrastructure.Common.Interceptors;
using DoubleVPartners.BackEnd.Infrastructure.Common.Persistence;
using DoubleVPartners.BackEnd.Infrastructure.Common.Persistence.DbContexts;
using DoubleVPartners.BackEnd.Infrastructure.Security;
using DoubleVPartners.BackEnd.Infrastructure.Security.Providers;
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
                .AddPersistence(configuration)
                .AddAuthentication()
                .AddMiscellaneous(configuration);

            return services;
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var persistenceSettingsSection = configuration.GetSection(nameof(PersistenceSettings));
            var persistenceSettings = persistenceSettingsSection.Get<PersistenceSettings>();

            ArgumentNullException.ThrowIfNull(persistenceSettings, nameof(persistenceSettings));

            services.AddDbContext<PgDbContext>(opts =>
            {
                opts.UseNpgsql(persistenceSettings.PgConnection, opt => opt.SetPostgresVersion(17, 0))
                    .AddInterceptors(new SoftDeleteInterceptor(), new UpdateAuditableInterceptor());
            });

            return services;
        }

        private static IServiceCollection AddAuthentication(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<JwtHandler>();

            services.AddScoped<IHttpProvider, HttpProvider>();
            services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();

            return services;
        }

        private static IServiceCollection AddMiscellaneous(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettingsSection = configuration.GetSection(nameof(JwtSettings));
            services.Configure<JwtSettings>(jwtSettingsSection);

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<JwtHandler>();

            return services;
        }
    }
}
