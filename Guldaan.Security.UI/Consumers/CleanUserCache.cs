using MassTransit;
using Guldaan.Common.Frontend.Auth;
using Guldaan.Security.Contracts.Tenants.Events;
using Guldaan.Security.Contracts.Users.Events;

namespace Guldaan.Security.UI.Consumers
{
    public class CleanUserCache(UserAndTokenCache cache) : IConsumer<CleanCacheForUserRequestSent>
    {
        private readonly UserAndTokenCache _cache = cache;

        public async Task Consume(ConsumeContext<CleanCacheForUserRequestSent> msg)
        {
            await _cache.RemoveUserInfoInCacheAsync(msg.Message.AuthId);
        }
    }
}
