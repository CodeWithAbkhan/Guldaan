using LanguageExt;
using Guldaan.Common.Errors;
using Guldaan.Security.Api.Data.Models;
using Guldaan.Security.Api.Features.Authorizations.Services;
using Guldaan.Security.Api.Mappers;
using Guldaan.Security.Contracts.Authorizations.Commands;

namespace Guldaan.Security.Api.Features.Authorizations.Commands.AddAuthorizationForAdmin
{
    public class AddAuthorizationForAdminHandler(AuthorizationCommandService commandService)
    {
        private readonly AuthorizationCommandService _commandService = commandService;

        public async Task<Either<IFeatureError, AuthorizationModel>> Handle(AddAuthorizationCommand command)
        {
            var current = command.MapToAuthorization();

            return await _commandService.ValidateIfNotAlreadyExistsAsync(current)
                    .BindAsync(_commandService.AddAuthorizationInDbAsync);
        }
    }
}
