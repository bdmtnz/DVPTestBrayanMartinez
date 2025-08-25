using DoubleVPartners.BackEnd.Application.Debts.Commands.Alter;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DoubleVPartners.BackEnd.Application
{
    public static class Dependencies
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AlterDebtFactory>();

            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssembly(typeof(Dependencies).Assembly);
            });

            return services;
        }
    }
}
