using MassTransit;
using Guldaan.Proxy.Services;
using Guldaan.Security.Contracts.Tenants.Events;
using Guldaan.Security.Contracts.Users.Events;

namespace Guldaan.Proxy.Consumer
{
    public class CleanUsersCacheTenantUpdated(UserService userService) : IConsumer<CleanCacheTenantUpdated>
    {
        private readonly UserService _userService = userService;

        public async Task Consume(ConsumeContext<CleanCacheTenantUpdated> msg)
        {
            await _userService.RemoveAllUserInfoFromCache(msg.Message.TenantId);
        }
    }
}
