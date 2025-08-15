using MassTransit;
using Guldaan.Common.Frontend.Auth;
using Guldaan.Proxy.Services;
using Guldaan.Security.Contracts.Users.Events;

namespace Guldaan.Proxy.Consumer
{
    public class CleanUserCache(UserService userService) : IConsumer<CleanCacheForUserRequestSent>
    {
        private readonly UserService _userService = userService;

        public async Task Consume(ConsumeContext<CleanCacheForUserRequestSent> msg)
        {
            await _userService.RemoveUserInfoFromCacheAsync(msg.Message.AuthId);
        }
    }
}
