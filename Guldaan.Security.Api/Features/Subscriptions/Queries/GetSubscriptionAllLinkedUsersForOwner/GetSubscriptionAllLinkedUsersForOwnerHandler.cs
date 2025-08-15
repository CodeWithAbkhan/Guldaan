using LanguageExt;
using Guldaan.Common.Api;
using Guldaan.Common.Errors;
using Guldaan.Security.Api.Data.Models;
using Guldaan.Security.Api.Features.Subscriptions.Services;
using Guldaan.Security.Api.Features.Subscriptions.Services.Poco;

namespace Guldaan.Security.Api.Features.Subscriptions.Queries.GetSubscriptionAllLinkedUsersForOwner
{
    public class GetSubscriptionAllLinkedUsersForOwnerHandler(SubscriptionQueryService queryService, ICurrentUser currentUser)
    {
        private readonly SubscriptionQueryService _queryService = queryService;
        private readonly ICurrentUser _currentUser = currentUser;

        public async Task<Either<IFeatureError, List<UserWithLinkedTenants>>> Handle()
        {
            return await _queryService.GetSelectedSubscriptionForOwnerAsync(_currentUser.Id)
                .BindAsync(s => _queryService.GetSubscriptionAllLinkedUsersAsync(s.Id));
        }
    }
}
