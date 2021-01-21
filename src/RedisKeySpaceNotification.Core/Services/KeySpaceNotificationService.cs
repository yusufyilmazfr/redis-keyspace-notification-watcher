using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedisKeySpaceNotification.Core.Configuration;
using RedisKeySpaceNotification.Core.Server;
using StackExchange.Redis;

namespace RedisKeySpaceNotification.Core.Services
{
    class KeySpaceNotificationService : IKeySpaceNotificationService
    {
        private readonly IRedisServer _redisServer;
        private readonly IRedisConfiguration _redisConfiguration;

        string EXPIRED_KEYS_CHANNEL = "__keyevent@{0}__:expired";
        string EVENT_NAME = "notify-keyspace-events";
        string EVENT_VALUE = "xKE";

        public KeySpaceNotificationService(IRedisServer redisServer, IRedisConfiguration redisConfiguration)
        {
            _redisServer = redisServer;
            _redisConfiguration = redisConfiguration;
        }

        public async Task ListenExpiredKeysChannel(Action<RedisChannel, RedisValue> handler, int databaseId = 0)
        {
            SetNotifyKeySpaceEventWhenDoesNotExist();

            var subscribers = _redisServer.ConnectionMultiplexer.GetSubscriber();

            var channel = String.Format(EXPIRED_KEYS_CHANNEL, databaseId);

            await subscribers.SubscribeAsync(channel, handler);
        }

        /// <summary>
        /// It provides set notify-keyspace-events config to redis when that config does not exist.
        /// Redis throws new event when key expired.
        /// </summary>
        private void SetNotifyKeySpaceEventWhenDoesNotExist()
        {
            var server = _redisServer.ConnectionMultiplexer.GetServer(_redisConfiguration.ConnectionString);

            var keySpaceEvents = server.ConfigGet(EVENT_NAME);

            if (keySpaceEvents.Any(pair => pair.Value == EVENT_VALUE) == false)
            {
                server.ConfigSet(EVENT_NAME, EVENT_VALUE);
            }
        }
    }
}
