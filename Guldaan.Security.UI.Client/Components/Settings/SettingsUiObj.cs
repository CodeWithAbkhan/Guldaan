using System.ComponentModel.DataAnnotations;
using Guldaan.Security.Contracts.Authorizations.Results;
using Guldaan.Security.Contracts.Subscriptions.Results;
using Guldaan.Security.UI.Client.Components.Authorizations;
using Guldaan.Security.UI.Client.Components.Subscription;

namespace Guldaan.Security.UI.Client.Components.Settings
{
    public class SettingsUiObj
    {
        [Required(ErrorMessage = "Subscription Id is required.")]
        public string? SelectedSubscriptionId { get; set; }

        [Required(ErrorMessage = "Tenant Id is required.")]
        public string? SelectedTenantId { get; set; }
    }

    public static class SubscriptionMappers
    {
        public static SubscriptionUiObj MapToSubscriptionUiObj(this SubscriptionOwnerResult result)
        {
            return new SubscriptionUiObj
            {
                Id = result.Id,
                Label = result.Label,
                PlanName = result.PlanName,
                MaxUsers = result.MaxUsers,
                MaxTenants = result.MaxTenants,
                Version = result.Version
            };
        }
    }
}
