using System;
using System.Collections.Generic;
using System.Text;
using StackExchange.Redis;

namespace RedisKeySpaceNotification.Core.Server
{
    /// <summary>
    /// Redis server.
    /// </summary>
    public interface IRedisServer
    {
        /// <summary>
        /// Get specified database instance from current database id.
        /// </summary>
        /// <param name="databaseId">database id, id must greater than or equal to 0 and less than or equal to 15.</param>
        /// <returns></returns>
        IDatabase GetDatabase(int databaseId);
        /// <summary>
        /// Get current ConnectionMultiplexer instance.
        /// </summary>
        /// <returns></returns>
        ConnectionMultiplexer ConnectionMultiplexer { get; }
    }
}
