using Guldaan.Security.Api.Features.Tenants.Commands.AddTenantForAdmin;
using Guldaan.Security.Api.Features.Tenants.Commands.UpdateTenantForAdmin;
using Guldaan.Security.Api.Features.Tenants.Queries.GetAllTenantsForAdmin;
using Guldaan.Security.Api.Features.Tenants.Queries.GetAllTenantsMe;
using Guldaan.Security.Api.Features.Tenants.Queries.GetTenant;
using Guldaan.Security.Api.Features.Tenants.Queries.GetTenantForAdmin;
using Guldaan.Security.Api.Features.Tenants.Queries.GetTenantLinkedUser;
using Guldaan.Security.Api.Features.Tenants.Queries.GetAllTenantLinkedUsers;
using Guldaan.Security.Api.Features.Tenants.Services;
using Guldaan.Security.Api.Features.Users.Queries.GetUserForProxy;
using Guldaan.Security.Api.Features.Tenants.Commands.UpdateTenantLinkedUser;
using Guldaan.Security.Api.Features.Tenants.Queries.GetTenantRoles;

namespace Guldaan.Security.Api.Features.Tenants.Extensions
{
    public static class TenantFeaturesRegistration
    {
        public static void AddTenantFeatures(this IServiceCollection services)
        {
            services.AddScoped<TenantCommandService>();
            services.AddScoped<TenantQueryService>();
            services.AddScoped<AddTenantForAdminHandler>();
            services.AddScoped<AddTenantForAdminValidator>();
            services.AddScoped<UpdateTenantForAdminHandler>();
            services.AddScoped<UpdateTenantForAdminValidator>();
            services.AddScoped<GetAllTenantsForAdminHandler>();
            services.AddScoped<GetTenantForAdminHandler>();
            services.AddScoped<GetAllTenantsMeHandler>();
            services.AddScoped<GetTenantHandler>();
            services.AddScoped<GetAllTenantLinkedUsersHandler>();
            services.AddScoped<GetTenantLinkedUserHandler>();
            services.AddScoped<UpdateTenantLinkedUserHandler>();
            services.AddScoped<GetTenantRolesHandler>();
        }
    }
}
