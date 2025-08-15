using Guldaan.Common.Api;
using Guldaan.Security.Api.Data.Models;
using Guldaan.Security.Api.Features.Subscriptions.Services;

namespace Guldaan.Security.Api.Features.Subscriptions.Queries.GetAllSubscriptionsMe
{
    public class GetAllSubscriptionsMeHandler(SubscriptionQueryService queryService, ICurrentUser currentUser)
    {
        private readonly SubscriptionQueryService _queryService = queryService;
        private readonly ICurrentUser _currentUser = currentUser;

        public async Task<IEnumerable<SubscriptionModel>> Handle()
        {
            return await _queryService.GetAllSubscriptionsForUserAsync(_currentUser.Id);
        }
    }
}
