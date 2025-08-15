using LanguageExt;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using Guldaan.Common.Api;
using Guldaan.Common.Db;
using Guldaan.Common.Errors;
using Guldaan.Security.Api.Data.Models;
using Guldaan.Security.Api.Features.Users.Services;
using Guldaan.Security.Contracts.Users.Commands;

namespace Guldaan.Security.Api.Features.Users.Commands.OnboardMeSimple
{
    public class OnboardMeSimpleHandler(UserCommandService commandService
        , ICurrentUser currentUser
        , IOptions<AuthRegisterAuthKey> config)
    {
        private readonly UserCommandService _commandService = commandService;
        private readonly ICurrentUser _currentUser = currentUser;
        private readonly bool _checkTrueActivation = config.Value.EmailActivationActivated;

        public async Task<Either<IFeatureError, UserModel>> Handle(OnboardMeSimpleCommand command)
        {
            return await _commandService.GetUserIfNotAlreadyOnBoardedAsync(_currentUser.Id)
                .BindAsync(u => _commandService.ActivateUserEmailInContext(u, _checkTrueActivation, command.ActivationCode))
                .BindAsync(_commandService.SimpleOnboardingAsync);
        }
    }
}
