using Microsoft.AspNetCore.Components;
using Guldaan.Common.Frontend.Auth;
using Guldaan.Common.RazorUI.Components;
using Guldaan.Security.UI.Shared.Httpclients;

namespace Guldaan.Security.UI.Components.Tenant
{
    public partial class TenantUpdateUserRolesPage(ClientUserService userService, IHttpSecurityClient securityClient)
        : Basepage(userService, securityClient)
    {
        [Parameter]
        public Guid Id { get; set; }

        private bool _isTenantManager = false;

        protected override List<BreadcrumbListItem> BreadcrumbItems
        {
            get
            {
                return _breadcrumbItems;
            }
        }

        private bool _isSubscriptionOwner = false;

        private static readonly List<BreadcrumbListItem> _breadcrumbItems = [
       new BreadcrumbListItem()
            {
                Position =1,
                Url = "/",
                Name = "Home"
            },
            new BreadcrumbListItem()
            {
                Position =2,
                Url = "/tenant",
                Name = "Tenant"
            }];

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            _isTenantManager = CanAccessIfHasAuthorizations(["tenant:read", "user:read", "tenant-user-role:write", "tenant-role:read"]);
            _isSubscriptionOwner = User?.IsSubOwnerOfTheSelectedTenant ?? false;
        }
    }
}
