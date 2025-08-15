using MassTransit;
using Guldaan.Common.Frontend.Auth;
using Guldaan.Security.Contracts.Tenants.Events;
using Guldaan.Security.Contracts.Users.Events;

namespace Guldaan.Security.UI.Consumers
{
    public class CleanUsersCacheTenantUpdated(UserAndTokenCache cache) : IConsumer<CleanCacheTenantUpdated>
    {
        private readonly UserAndTokenCache _cache = cache;

        public async Task Consume(ConsumeContext<CleanCacheTenantUpdated> msg)
        {
            await _cache.RemoveAllUserInfoInCacheForTenantAsync(msg.Message.TenantId);
        }
    }
}

