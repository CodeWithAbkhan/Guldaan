using MassTransit;
using Guldaan.Proxy.Services;
using Guldaan.Security.Contracts.Tenants.Events;

namespace Guldaan.Proxy.Consumer
{
    public class CleanUsersCacheTenantDeleted(UserService userService) : IConsumer<CleanCacheTenantDeleted>
    {
        private readonly UserService _userService = userService;

        public async Task Consume(ConsumeContext<CleanCacheTenantDeleted> msg)
        {
            await _userService.RemoveAllUserInfoFromCache(msg.Message.TenantId);
        }
    }
}
