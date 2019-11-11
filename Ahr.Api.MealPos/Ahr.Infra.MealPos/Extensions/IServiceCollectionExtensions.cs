using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ahr
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// 使用時機--例如在 service 中,直接叫用 AutoMapper
        /// var mapper = services.GetService<IMapper>();
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static T GetService<T>(this Microsoft.Extensions.DependencyInjection.IServiceCollection services)
        {
            return services.BuildServiceProvider().GetService<T>();
        }
    }
}
