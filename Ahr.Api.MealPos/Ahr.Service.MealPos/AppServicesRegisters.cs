using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ahr.Service.MealPos
{
    public static class AppServicesRegisters
    {
        public static IServiceCollection AppServiceRegister(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
