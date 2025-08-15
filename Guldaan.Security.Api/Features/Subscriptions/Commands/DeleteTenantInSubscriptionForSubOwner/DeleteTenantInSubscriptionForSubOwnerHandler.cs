using LanguageExt;
using Guldaan.Common.Api;
using Guldaan.Common.Errors;
using Guldaan.Security.Api.Features.Subscriptions.Services.Poco;
using Guldaan.Security.Api.Features.Subscriptions.Services;
using Guldaan.Security.Contracts.Subscriptions.Commands;

namespace Guldaan.Security.Api.Features.Subscriptions.Commands.DeleteTenantInSubscriptionForSubOwner
{
    public class DeleteTenantInSubscriptionForSubOwnerHandler(SubscriptionCommandService commandService, ICurrentUser currentUser)
    {
        private readonly SubscriptionCommandService _commandService = commandService;
        private readonly ICurrentUser _currentUser = currentUser;

        public async Task<Either<IFeatureError, Guid>> Handle(Guid currentId)
        {
            return await _commandService.GetSelectedSubscriptionForOwnerAsync(_currentUser.Id)
                .BindAsync(s => _commandService.GetTenantInSubscriptionAsyc(currentId,s))
                .BindAsync(tu => _commandService.DeleteTenantInDbAsync(tu.Tenant));
        }
    }
}
