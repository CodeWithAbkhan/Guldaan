using LanguageExt;
using Guldaan.Common.Errors;
using Guldaan.Security.Api.Data.Models;
using Guldaan.Security.Api.Features.Authorizations.Services;
using Guldaan.Security.Api.Features.Roles.Services;
using Guldaan.Security.Api.Features.Roles.Services.Poco;
using Guldaan.Security.Api.Mappers;
using Guldaan.Security.Contracts.Authorizations.Commands;
using Guldaan.Security.Contracts.Roles.Commands;

namespace Guldaan.Security.Api.Features.Roles.Commands.AddRoleForAdmin
{
    public class AddRoleForAdminHandler(RoleCommandService commandService)
    {
        private readonly RoleCommandService _commandService = commandService;

        public async Task<Either<IFeatureError, RoleWithAuthorizationIds>> Handle(AddRoleCommand command)
        {
            var current = command.MapToRoleWithAuthIds();

            return await _commandService.ValidateIfNotAlreadyExistsForAdminAsync(current.Role)
                    .BindAsync(r => _commandService.ValidateAuthorizationIdsList(r, current.AuthorizationIds))
                    .BindAsync(ra => _commandService.AddRoleForAdminInDbAsync(ra.Role,ra.AuthorizationIds));
        }
    }
}
