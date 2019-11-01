using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ahr
{
    public static class IServiceCollectionExtensions
    {
        public static T GetService<T>(this Microsoft.Extensions.DependencyInjection.IServiceCollection services)
        {
            return services.BuildServiceProvider().GetService<T>();
        }
    }
}
