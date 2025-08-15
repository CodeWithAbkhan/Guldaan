using LanguageExt;
using Guldaan.Common.Errors;
using Guldaan.Security.Api.Features.Roles.Services;
using Guldaan.Security.Contracts.Roles.Commands;

namespace Guldaan.Security.Api.Features.Roles.Commands.BatchDeleteRoleForAdmin
{
    public class BatchDeleteRoleForAdminHandler(RoleCommandService commandService)
    {
        private readonly RoleCommandService _commandService = commandService;

        public async Task<Either<IFeatureError, bool>> Handle(BatchDeleteRoleCommand command)
        {
            return await _commandService.GetRolesByIdsForAdmin(command.RoleIds)
                .BindAsync(_commandService.DeleteRolesRangeInDbAsync);
        }
    }
}
