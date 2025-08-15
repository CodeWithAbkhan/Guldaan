using LanguageExt;
using Guldaan.Common.Errors;
using Guldaan.Security.Api.Data.Models;
using Guldaan.Security.Api.Features.Authorizations.Services;
using Guldaan.Security.Api.Features.Roles.Services;
using Guldaan.Security.Api.Features.Roles.Services.Poco;
using Guldaan.Security.Api.Mappers;
using Guldaan.Security.Contracts.Authorizations.Commands;
using Guldaan.Security.Contracts.Roles.Commands;

namespace Guldaan.Security.Api.Features.Roles.Commands.UpdateRoleForAdmin
{
    public class UpdateRoleForAdminHandler(RoleCommandService commandService)
    {
        private readonly RoleCommandService _commandService = commandService;

        public async Task<Either<IFeatureError, RoleWithAuthorizationIds>> Handle(UpdateRoleCommand command, Guid currentId)
        {
            var upd = command.MapToRole(currentId);

            return await _commandService.ValidateIfExistsIdAsync(currentId)
                .BindAsync(r => _commandService.MapInDbContextAsync(r, upd))
                .BindAsync(_commandService.ValidateIfNotAlreadyExistsWithOtherIdForAdminAsync)
                .BindAsync(r => _commandService.ValidateAuthorizationIdsList(r, command.AuthorizationIds))
                .BindAsync(ra => _commandService.PrepareAuthorizationIdsForAttachAndDetachForUpdAsync(ra.Role, ra.AuthorizationIds))
                .BindAsync(rad => _commandService.UpdateRoleForAdminAsync(rad.Role, rad.AttachIds, rad.DetachIds));
        }
    }
}
