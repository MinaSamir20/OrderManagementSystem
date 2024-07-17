using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OrderManagementSystem.Application.Behaviers;
using OrderManagementSystem.Application.Services.Auth;
using OrderManagementSystem.Application.Services.Customers;
using OrderManagementSystem.Application.Services.Discounts;
using OrderManagementSystem.Application.Services.Invoices;
using OrderManagementSystem.Application.Services.Orders;
using OrderManagementSystem.Application.Services.Products;
using System.Reflection;

namespace OrderManagementSystem.Application
{
    public static class ApplicationDependencies
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<DiscountService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IInvoiceService, InvoiceService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }
    }
}
