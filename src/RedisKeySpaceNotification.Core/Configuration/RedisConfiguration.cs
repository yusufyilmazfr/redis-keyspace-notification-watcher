using System;
using System.Collections.Generic;
using System.Text;

namespace RedisKeySpaceNotification.Core.Configuration
{
    class RedisConfiguration : IRedisConfiguration
    {
        public string ConnectionString { get; set; }
    }
}
