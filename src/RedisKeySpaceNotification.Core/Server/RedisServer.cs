using System;
using System.Collections.Generic;
using System.Text;
using RedisKeySpaceNotification.Core.Configuration;
using StackExchange.Redis;

namespace RedisKeySpaceNotification.Core.Server
{
    public class RedisServer : IRedisServer
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer;

        public RedisServer(IRedisConfiguration redisConfiguration)
        {
            var configurationOption = ConfigurationOptions.Parse(redisConfiguration.ConnectionString);
            configurationOption.AllowAdmin = true;

            _connectionMultiplexer = ConnectionMultiplexer.Connect(configurationOption);

            if (!_connectionMultiplexer.IsConnected)
            {
                throw new Exception($"ConnectionMultiplexer didn't connect! Please check configuration settings in {redisConfiguration.GetType().Name}");
            }
        }

        /// <summary>
        /// Get specified database instance from current database id.
        /// </summary>
        /// <param name="databaseId">database id, id must greater than or equal to 0 and less than or equal to 15.</param>
        /// <returns></returns>
        public IDatabase GetDatabase(int databaseId)
        {
            if (databaseId < 0) throw new ArgumentException("Database id must greater than or equal to 0.");
            if (databaseId > 15) throw new ArgumentException("Database id must less than or equal to 15.");

            return _connectionMultiplexer.GetDatabase(databaseId);
        }

        public ConnectionMultiplexer ConnectionMultiplexer => _connectionMultiplexer;
    }
}
