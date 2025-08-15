using MassTransit;
using Guldaan.Common.Frontend.Auth;
using Guldaan.Security.Contracts.Roles.Events;

namespace Guldaan.Security.UI.Consumers
{
    public class CleanUsersCacheRoleDeleted(UserAndTokenCache cache) : IConsumer<CleanCacheRoleDeleted>
    {
        private readonly UserAndTokenCache _cache = cache;

        public async Task Consume(ConsumeContext<CleanCacheRoleDeleted> msg)
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
