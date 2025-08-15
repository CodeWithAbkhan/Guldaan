using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Guldaan.Common.Frontend.Auth;
using Guldaan.Common.RazorUI.Components;
using Guldaan.Security.UI.Shared.Httpclients;

namespace Guldaan.Security.UI.Components.Settings
{
    public partial class SettingsPage(ClientUserService userService, IHttpSecurityClient securityClient)
        : Basepage(userService, securityClient)
    {
        protected override List<BreadcrumbListItem> BreadcrumbItems
        {
            get
            {
                return _breadcrumbItems;
            }
        }

        private static readonly List<BreadcrumbListItem> _breadcrumbItems = [
            new BreadcrumbListItem()
            {
                Position =1,
                Url = "/",
                Name = "Home"
            }];
    }
}
