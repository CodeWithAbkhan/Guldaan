using Microsoft.AspNetCore.Components;
using Guldaan.Security.Contracts.Tenants.Results;
using Guldaan.Security.UI.Client.Components.Subscription;

namespace Guldaan.Security.UI.Client.Components.Tenant
{
    public partial class TenantInfo
    {
        [Parameter]
        public bool IsMainLoading { get; set; } = false;

        [Parameter]
        public TenantStandardResult? Tenant { get; set; }
    }
}
