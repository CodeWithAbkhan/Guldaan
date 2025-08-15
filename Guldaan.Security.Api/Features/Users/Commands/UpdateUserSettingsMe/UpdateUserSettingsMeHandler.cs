using LanguageExt;
using Guldaan.Common.Api;
using Guldaan.Common.Errors;
using Guldaan.Security.Api.Data.Models;
using Guldaan.Security.Api.Features.Users.Services;
using Guldaan.Security.Contracts.Users.Commands;

namespace Guldaan.Security.Api.Features.Users.Commands.UpdateUserSettingsMe
{
    public class UpdateUserSettingsMeHandler(UserCommandService userCommandService, ICurrentUser currentUser)
    {
        private readonly UserCommandService _userCommandService = userCommandService;
        private readonly ICurrentUser _currentUser = currentUser;

        public async Task<Either<IFeatureError, UserModel>> Handle(SetSettingsUserMeCommand command)
        {
            return await _userCommandService.GetUserIfTenantLinkExistsAndValidAsync(_currentUser.Id, command.TenantId)
                .BindAsync(user => _userCommandService.SaveAndPublishNewSelectedTenantForUser(user, command.TenantId));
        }
    }
}
