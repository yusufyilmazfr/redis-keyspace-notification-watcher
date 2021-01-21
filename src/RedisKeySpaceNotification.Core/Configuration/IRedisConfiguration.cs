using System;
using System.Collections.Generic;
using System.Text;

namespace RedisKeySpaceNotification.Core.Configuration
{
    /// <summary>
    /// Redis server configuration.
    /// </summary>
    public interface IRedisConfiguration
    {
        /// <summary>
        /// Connection string.
        /// </summary>
        string ConnectionString { get; set; }
    }
}
