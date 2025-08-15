using Guldaan.Security.Api.Features.Authorizations.Commands.AddAuthorizationForAdmin;
using Guldaan.Security.Api.Features.Authorizations.Commands.BatchDeleteAuthorizationForAdmin;
using Guldaan.Security.Api.Features.Authorizations.Commands.DeleteAuthorizationForAdmin;
using Guldaan.Security.Api.Features.Authorizations.Commands.UpdateAuthorizationForAdmin;
using Guldaan.Security.Api.Features.Authorizations.Queries.GetAllAuthorizationsForAdmin;
using Guldaan.Security.Api.Features.Authorizations.Queries.GetAuthorizationForAdmin;
using Guldaan.Security.Api.Features.Authorizations.Services;
using Guldaan.Security.Api.Features.Subscriptions.Queries.GetAllSubscriptionsMe;

namespace Guldaan.Security.Api.Features.Authorizations.Extensions
{
    public static class AuthorizationFeaturesRegistration
    {
        public static void AddAuthorizationFeatures(this IServiceCollection services)
        {
            services.AddScoped<AuthorizationCommandService>();
            services.AddScoped<AuthorizationQueryService>();

            services.AddScoped<GetAllAuthorizationsForAdminHandler>();
            services.AddScoped<GetAuthorizationForAdminHandler>();

            services.AddScoped<AddAuthorizationForAdminHandler>();
            services.AddScoped<AddAuthorizationForAdminValidator>();

            services.AddScoped<DeleteAuthorizationForAdminHandler>();
            services.AddScoped<BatchDeleteAuthorizationForAdminHandler>();

            services.AddScoped<UpdateAuthorizationForAdminHandler>();
            services.AddScoped<UpdateAuthorizationForAdminValidator>();
        }
    }
}
