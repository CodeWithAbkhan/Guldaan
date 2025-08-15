using Microsoft.AspNetCore.Components;

namespace Guldaan.Security.UI.Client.Components.Subscription
{
    public partial class SubscriptionInfo
    {
        [Parameter]
        public bool IsMainLoading { get; set; } = false;

        [Parameter]
        public SubscriptionUiObj? Subscription { get; set; }
    }
}
