using Microsoft.Extensions.DependencyInjection;
using OrderManagementSystem.Infrastructure.Repositories.UnitOfWork;

namespace OrderManagementSystem.Infrastructure
{
    public static class InfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
