using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RedisKeySpaceNotification.Core.Extensions;
using RedisKeySpaceNotification.Core.Services;

namespace RedisKeySpaceNotification.Console
{
    class Program
    {
        private static IKeySpaceNotificationService KeySpaceNotificationService { get; set; }
        private static IServiceProvider ServiceProvider { get; set; }

        static async Task Main(string[] args)
        {
            RegisterDependencies();

            KeySpaceNotificationService = ServiceProvider.GetRequiredService<IKeySpaceNotificationService>();

            await KeySpaceNotificationService.ListenExpiredKeysChannel((channel, value) =>
            {
                System.Console.WriteLine($"channel: {channel} key: {value}");
            });

            System.Console.Read();
        }

        private static void RegisterDependencies()
        {
            ServiceProvider = new ServiceCollection()
                .AddRedisKeySpaceNotification(redisConfiguration =>
                {
                    redisConfiguration.ConnectionString = "localhost:6379";
                })
                .BuildServiceProvider();
        }
    }
}
