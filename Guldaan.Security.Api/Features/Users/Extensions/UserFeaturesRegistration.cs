using Guldaan.Security.Api.Features.Users.Commands.OnboardMeSimple;
using Guldaan.Security.Api.Features.Users.Commands.RegisterUser;
using Guldaan.Security.Api.Features.Users.Commands.UpdateUserSettingsMe;
using Guldaan.Security.Api.Features.Users.Queries.GetMyHubToken;
using Guldaan.Security.Api.Features.Users.Queries.GetUserForProxy;
using Guldaan.Security.Api.Features.Users.Queries.GetUserMe;
using Guldaan.Security.Api.Features.Users.Services;

namespace Guldaan.Security.Api.Features.Users.Extensions
{
    public static class UserFeaturesRegistration
    {
        public static void AddUserFeatures(this IServiceCollection services)
        {
            services.AddScoped<GetUserForProxyHandler>();
            services.AddScoped<GetUserMeHandler>();

            services.AddScoped<UpdateUserSettingsMeHandler>();
            services.AddScoped<UpdateUserSettingsMeValidator>();

            services.AddScoped<UserQueryService>();
            services.AddScoped<UserCommandService>();

            services.AddScoped<RegisterUserHandler>();
            services.AddScoped<RegisterUserValidator>();

            services.AddScoped<OnboardMeSimpleHandler>();

            services.AddScoped<GetMyHubTokenHandler>();
        }
    }
}
