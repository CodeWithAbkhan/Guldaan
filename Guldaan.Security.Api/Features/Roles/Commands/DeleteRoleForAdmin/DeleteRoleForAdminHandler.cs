using LanguageExt;
using Guldaan.Common.Errors;
using Guldaan.Security.Api.Features.Roles.Services;

namespace Guldaan.Security.Api.Features.Roles.Commands.DeleteRoleForAdmin
{
    public class DeleteRoleForAdminHandler(RoleCommandService commandService)
    {
        private readonly RoleCommandService _commandService = commandService;

        public async Task<Either<IFeatureError, bool>> Handle(Guid id)
        {
            return await _commandService.ValidateIfExistsIdAsync(id)
                    .BindAsync(_commandService.DeleteRoleInDbAsync);
        }
    }
}
