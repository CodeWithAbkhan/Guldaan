using LanguageExt;
using Guldaan.Common.Api;
using Guldaan.Common.Errors;
using Guldaan.Security.Api.Data.Models;
using Guldaan.Security.Api.Features.Subscriptions.Services;

namespace Guldaan.Security.Api.Features.Subscriptions.Queries.GetSubscriptionForOwner
{
    public class GetSubscriptionForOwnerHandler(SubscriptionQueryService queryService, ICurrentUser currentUser)
    {
        private readonly SubscriptionQueryService _queryService = queryService;
        private readonly ICurrentUser _currentUser = currentUser;

        public async Task<Either<IFeatureError,SubscriptionModel>> Handle()
        {
            return await _queryService.GetSelectedSubscriptionForOwnerAsync(_currentUser.Id);
        }
    }
}
