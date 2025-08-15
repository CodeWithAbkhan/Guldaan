using LanguageExt;
using Guldaan.Common.Errors;
using Guldaan.Security.Api.Data.Models;
using Guldaan.Security.Api.Features.Authorizations.Services;
using Guldaan.Security.Api.Mappers;
using Guldaan.Security.Contracts.Authorizations.Commands;

namespace Guldaan.Security.Api.Features.Authorizations.Commands.UpdateAuthorizationForAdmin
{
    public class UpdateAuthorizationForAdminHandler(AuthorizationCommandService commandService)
    {
        private readonly AuthorizationCommandService _commandService = commandService;

        public async Task<Either<IFeatureError, AuthorizationModel>> Handle(UpdateAuthorizationCommand command, Guid currentId)
        {
            var upd = command.MapToAuthorization(currentId);
            
            return await _commandService.ValidateIfExistsIdAsync(currentId)
                .BindAsync(x=> _commandService.MapInDbContextAsync(x,upd))
                .BindAsync(_commandService.ValidateIfNotAlreadyExistsWithOtherIdAsync)
                .BindAsync(_commandService.UpdateAuthorizationInDbAsync);
        }
    }
}
