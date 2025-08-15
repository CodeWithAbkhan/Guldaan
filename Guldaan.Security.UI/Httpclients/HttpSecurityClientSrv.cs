using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guldaan.Common.Frontend.Auth;
using Guldaan.Security.UI.Shared.Httpclients;

namespace Guldaan.Security.UI.Httpclients
{
    public class HttpSecurityClientSrv(HttpClient httpClient, ClientUserService userService) : HttpSecurityClientBase(httpClient)
    {
        private readonly ClientUserService _userService = userService;

        protected override async Task<HttpResponseMessage> SendAsync(Func<Task<HttpResponseMessage>> httpRequest)
        {
            await SetSecruityHeaderAsync();
            return await base.SendAsync(httpRequest);
        }

        private async Task SetSecruityHeaderAsync()
        {
            var usertoken = await _userService.GetCurrentUserTokenAsync();

            if (string.IsNullOrEmpty(usertoken))
                throw new ApplicationException("Cannot retrieve info, refresh your app");

            Client.DefaultRequestHeaders.Clear();
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {usertoken}");
        }

    }
}

