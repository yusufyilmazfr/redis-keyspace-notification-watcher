using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace RedisKeySpaceNotification.Core.Services
{
    public interface IKeySpaceNotificationService
    {
        Task ListenExpiredKeysChannel(Action<RedisChannel, RedisValue> handler, int databaseId = 0);
    }
}
