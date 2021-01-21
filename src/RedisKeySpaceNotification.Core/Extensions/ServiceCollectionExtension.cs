using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using RedisKeySpaceNotification.Core.Configuration;
using RedisKeySpaceNotification.Core.Server;
using RedisKeySpaceNotification.Core.Services;

namespace RedisKeySpaceNotification.Core.Extensions
{
    /// <summary>
    /// It provides to register RedisKeySpaceNotification's dependencies to IServiceCollection
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Register RedisKeySpaceNotification's dependencies.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRedisKeySpaceNotification(this IServiceCollection services, Action<IRedisConfiguration> configuration)
        {
            var redisConfiguration = new RedisConfiguration();
            configuration.Invoke(redisConfiguration);

            services.AddSingleton(typeof(IRedisConfiguration), redisConfiguration);

            services.AddSingleton<IRedisServer, RedisServer>();
            services.AddSingleton<IKeySpaceNotificationService, KeySpaceNotificationService>();

            return services;
        }
    }
}
