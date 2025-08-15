using LanguageExt;
using Guldaan.Common.Api;
using Guldaan.Common.Errors;
using Guldaan.Security.Api.Data.Models;
using Guldaan.Security.Api.Features.Subscriptions.Services;
using Guldaan.Security.Api.Mappers;
using Guldaan.Security.Contracts.Subscriptions.Commands;
using Guldaan.Security.Contracts.Users.Results;

namespace Guldaan.Security.Api.Features.Subscriptions.Commands.UpdateUserInSubscriptionForSubOwner
{
    public class UpdateUserInSubscriptionForSubOwnerHandler(SubscriptionCommandService commandService, ICurrentUser currentUser)
    {
        private readonly SubscriptionCommandService _commandService = commandService;
        private readonly ICurrentUser _currentUser = currentUser;

        public async Task<Either<IFeatureError, (UserModel User, SubscriptionUserModel SubUser)>> 
            Handle(UpdateSubscriptionLinkedUserCommand command, Guid currentId)
        {
            var forUpd = command.MapToUserForUpd();

            return await _commandService.GetSelectedSubscriptionForOwnerAsync(_currentUser.Id)
                .BindAsync(s => _commandService.GetUserInfoInSubscription(currentId, s))
                .BindAsync(usus => _commandService.MapUserInfoInDbContextAsync(usus.User, usus.SubUser, usus.Subscription, forUpd))
                .BindAsync(usus => _commandService.ValidateUserInfoForSubscriptionAsync(usus.User,usus.SubUser,usus.Subscription,true))
                .BindAsync(usu => _commandService.UpdateSubscriptionUserInfoInDbAsync(usu.User,usu.SubUser));
        }
    }
}
