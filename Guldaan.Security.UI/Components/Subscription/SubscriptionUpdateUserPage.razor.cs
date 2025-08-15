using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Guldaan.Common.Frontend.Auth;
using Guldaan.Common.RazorUI.Components;
using Guldaan.Security.Contracts.Subscriptions.Results;
using Guldaan.Security.UI.Shared.Httpclients;

namespace Guldaan.Security.UI.Components.Subscription
{
    public partial class SubscriptionUpdateUserPage(ClientUserService userService, IHttpSecurityClient securityClient)
        : Basepage(userService, securityClient)
    {
        protected override List<BreadcrumbListItem> BreadcrumbItems
        {
            get
            {
                return _breadcrumbItems;
            }
        }

        [Parameter]
        public Guid Id { get; set; }

        private bool _isSubscriptionOwner = false;
        private Guid _selectedSubscriptionId = default;

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
                Url = "/subscription",
                Name = "Subscription"
            }];

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            var (IsOwner, SelectedSubscriptionId) = await IsOwnerOfTheSelectedSubscription();

            _isSubscriptionOwner = IsOwner;
            if (SelectedSubscriptionId != null)
                _selectedSubscriptionId = (Guid)SelectedSubscriptionId;
        }
    }
}
