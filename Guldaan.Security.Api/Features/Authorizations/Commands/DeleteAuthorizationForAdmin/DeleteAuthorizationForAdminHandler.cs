using LanguageExt;
using Guldaan.Common.Errors;
using Guldaan.Security.Api.Features.Authorizations.Services;

namespace Guldaan.Security.Api.Features.Authorizations.Commands.DeleteAuthorizationForAdmin
{
    public class DeleteAuthorizationForAdminHandler(AuthorizationCommandService commandService)
    {
        private readonly AuthorizationCommandService _commandService = commandService;

        public async Task<Either<IFeatureError, bool>> Handle(Guid id)
        {
            return await _commandService.ValidateIfExistsIdAsync(id)
                    .BindAsync(_commandService.DeleteAuthorizationInDbAsync);
        }
    }
}
