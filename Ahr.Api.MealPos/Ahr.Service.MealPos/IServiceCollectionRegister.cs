using Ahr.Service.MealPos;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ahr.Service.MealPos
{
    public static class IServiceCollectionRegister
    {
        public static IServiceCollection AppServiceRegister(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IMealService, MealService>();
            services.AddScoped<IOrderService, OrderService>();
            return services;
        }
    }
}
