using MassTransit;
using Guldaan.Common.Frontend.Auth;
using Guldaan.Security.Contracts.Roles.Events;
using Guldaan.Security.Contracts.Tenants.Events;

namespace Guldaan.Security.UI.Consumers
{
    public class CleanUsersCacheRoleUpdated(UserAndTokenCache cache) : IConsumer<CleanCacheRoleUpdated>
    {
        private readonly UserAndTokenCache _cache = cache;

        public async Task Consume(ConsumeContext<CleanCacheRoleUpdated> msg)
        {
            var op = msg.Message;

            if (op.TenantId == null)
            {
                await _cache.RemoveAllUserInfoInCacheAsync();
            }
            else
            {
                await _cache.RemoveAllUserInfoInCacheForTenantAsync((Guid)op.TenantId);
            }
        }
    }
}
