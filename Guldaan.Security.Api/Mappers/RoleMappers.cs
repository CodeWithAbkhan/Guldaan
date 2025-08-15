using MassTransit;
using Guldaan.Security.Api.Data.Models;
using Guldaan.Security.Api.Features.Roles.Services.Poco;
using Guldaan.Security.Contracts.Authorizations.Commands;
using Guldaan.Security.Contracts.Authorizations.Results;
using Guldaan.Security.Contracts.Roles.Commands;
using Guldaan.Security.Contracts.Roles.Results;

namespace Guldaan.Security.Api.Mappers
{
    public static class RoleMappers
    {
        public static RoleAdminResult MapToRolesAdminResult(this RoleWithAuthorizationIds role)
        {
            return new RoleAdminResult
            {
                Id = role.Id,
                Code = role.Code,
                Description = role.Description,
                AuthorizationIds = role.AuthorizationIds,
                Version = role.Version
            };
        }

        public static IEnumerable<RoleAdminResult> MapToRolesAdminResults(this IEnumerable<RoleWithAuthorizationIds> roles)
        {
            return roles.Select(MapToRolesAdminResult);
        }

        public static (RoleModel Role, List<Guid> AuthorizationIds) MapToRoleWithAuthIds(this AddRoleCommand entity)
        {
            var role = new RoleModel
            {
                Id = NewId.NextGuid(),
                Code = entity.Code,
                Description = entity.Description,
                TenantId = entity.TenantId,
            };

            var authorizationIds = entity.AuthorizationIds;

            return (role, authorizationIds);
        }

        public static RoleModel MapToRole(this UpdateRoleCommand entity, Guid currentId)
        {
            return new RoleModel
            {
                Id = currentId,
                Code = entity.Code,
                Description = entity.Description,
                Version = entity.Version,
                TenantId = entity.TenantId
            };
        }

        public static RoleModel MapToRole(this RoleModel forUpd, RoleModel model)
        {
            model.Id = forUpd.Id;
            model.Code = forUpd.Code;
            model.Description = forUpd.Description;
            model.Version = forUpd.Version;
            model.TenantId = forUpd.TenantId;

            return model;
        }
        public static RoleLightResult MapToRoleLightResult(this RoleModel current)
        {
            return new RoleLightResult
            {
                Id = current.Id,
                Code = current.Code
            };
        }

        public static IEnumerable<RoleLightResult> MapToRoleLightResults(this IEnumerable<RoleModel> current)
        {
            return current.Select(MapToRoleLightResult);
        }


    }
}
