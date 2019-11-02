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
            //使用時機--例如在 service 中,直接叫用 AutoMapper 
            //var mapper = services.GetService<IMapper>();
            return services.BuildServiceProvider().GetService<T>();
        }
    }
}
