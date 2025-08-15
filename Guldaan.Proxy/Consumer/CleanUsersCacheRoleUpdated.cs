using MassTransit;
using Guldaan.Proxy.Services;
using Guldaan.Security.Contracts.Roles.Events;
using Guldaan.Security.Contracts.Tenants.Events;

namespace Guldaan.Proxy.Consumer
{
    public class CleanUsersCacheRoleUpdated(UserService userService) : IConsumer<CleanCacheRoleUpdated>
    {
        private readonly UserService _userService = userService;

        public async Task Consume(ConsumeContext<CleanCacheRoleUpdated> msg)
        {
            var op = msg.Message;

            if (op.TenantId == null)
            {
                await _userService.RemoveAllUserInfoFromCache();
            }
            else
            {
                await _userService.RemoveAllUserInfoFromCache((Guid)op.TenantId);
            }
        }
    }
}
