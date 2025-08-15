using Guldaan.Security.Api.Features.Roles.Commands.AddRoleForAdmin;
using Guldaan.Security.Api.Features.Roles.Commands.BatchDeleteRoleForAdmin;
using Guldaan.Security.Api.Features.Roles.Commands.DeleteRoleForAdmin;
using Guldaan.Security.Api.Features.Roles.Commands.UpdateRoleForAdmin;
using Guldaan.Security.Api.Features.Roles.Queries.GetAllRolesForAdmin;
using Guldaan.Security.Api.Features.Roles.Queries.GetRoleForAdmin;
using Guldaan.Security.Api.Features.Roles.Services;

namespace Guldaan.Security.Api.Features.Roles.Extensions
{
    public static class RoleFeaturesRegistration
    {
        public static void AddRoleFeatures(this IServiceCollection services)
        {
            services.AddScoped<RoleQueryService>();
            services.AddScoped<RoleCommandService>();

            services.AddScoped<AddRoleForAdminValidator>();
            services.AddScoped<AddRoleForAdminHandler>();

            services.AddScoped<UpdateRoleForAdminValidator>();
            services.AddScoped<UpdateRoleForAdminHandler>();

            services.AddScoped<DeleteRoleForAdminHandler>();

            services.AddScoped<BatchDeleteRoleForAdminHandler>();

            services.AddScoped<GetRoleForAdminHandler>();
            services.AddScoped<GetAllRolesForAdminHandler>();
        }
    }
}
