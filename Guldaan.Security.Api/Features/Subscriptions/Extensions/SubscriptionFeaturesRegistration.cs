using Guldaan.Security.Api.Features.Subscriptions.Commands.AddTenantInSubscriptionForSubOwner;
using Guldaan.Security.Api.Features.Subscriptions.Queries.GetAllSubscriptionsMe;
using Guldaan.Security.Api.Features.Subscriptions.Queries.GetSubscriptionForOwner;
using Guldaan.Security.Api.Features.Subscriptions.Queries.GetSubscriptionAllLinkedTenantsForOwner;
using Guldaan.Security.Api.Features.Subscriptions.Queries.GetSubscriptionAllLinkedUsersForOwner;
using Guldaan.Security.Api.Features.Subscriptions.Services;
using Guldaan.Security.Api.Features.Subscriptions.Queries.GetSubscriptionLinkedTenantForOwner;
using Guldaan.Security.Api.Features.Subscriptions.Commands.UpdateTenantInSubscriptionForSubOwner;
using Guldaan.Security.Api.Features.Subscriptions.Commands.DeleteTenantInSubscriptionForSubOwner;
using Guldaan.Security.Api.Features.Subscriptions.Commands.UpdateUserInSubscriptionForSubOwner;
using Guldaan.Security.Api.Features.Subscriptions.Queries.GetSubscriptionLinkedUserForOwner;

namespace Guldaan.Security.Api.Features.Subscriptions.Extensions
{
    public static class SubscriptionFeaturesRegistration
    {
        public static void AddSubscriptionFeatures(this IServiceCollection services)
        {
            services.AddScoped<SubscriptionQueryService>();
            services.AddScoped<SubscriptionCommandService>();

            services.AddScoped<GetAllSubscriptionsMeHandler>();
            services.AddScoped<GetSubscriptionForOwnerHandler>();
            services.AddScoped<GetSubscriptionAllLinkedTenantsForOwnerHandler>();
            services.AddScoped<GetSubscriptionLinkedTenantForOwnerHandler>();
            services.AddScoped<GetSubscriptionLinkedUserForOwnerHandler>();
            services.AddScoped<GetSubscriptionAllLinkedUsersForOwnerHandler>();

            services.AddScoped<AddTenantInSubscriptionForSubOwnerHandler>();
            services.AddScoped<AddTenantInSubscriptionForSubOwnerValidator>();

            services.AddScoped<UpdateTenantInSubscriptionForSubOwnerHandler>();
            services.AddScoped<UpdateTenantInSubscriptionForSubOwnerValidator>();
            services.AddScoped<UpdateUserInSubscriptionForSubOwnerHandler>();
            services.AddScoped<UpdateUserInSubscriptionForSubOwnerValidator>();

            services.AddScoped<DeleteTenantInSubscriptionForSubOwnerHandler>();
        }
    }
}
