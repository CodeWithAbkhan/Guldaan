using MassTransit;
using Guldaan.Common.Frontend.Auth;
using Guldaan.Security.Contracts.Tenants.Events;

namespace Guldaan.Security.UI.Consumers
{
    public class CleanUsersCacheTenantDeleted(UserAndTokenCache cache) : IConsumer<CleanCacheTenantDeleted>
    {
        private readonly UserAndTokenCache _cache = cache;

        public async Task Consume(ConsumeContext<CleanCacheTenantDeleted> msg)
        {
            await _cache.RemoveAllUserInfoInCacheForTenantAsync(msg.Message.TenantId);
        }
    }
}
