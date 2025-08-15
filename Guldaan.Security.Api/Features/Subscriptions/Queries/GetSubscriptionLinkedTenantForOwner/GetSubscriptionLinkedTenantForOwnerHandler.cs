using LanguageExt;
using Guldaan.Common.Api;
using Guldaan.Common.Errors;
using Guldaan.Security.Api.Features.Subscriptions.Services.Poco;
using Guldaan.Security.Api.Features.Subscriptions.Services;
using Guldaan.Security.Contracts.Subscriptions.Commands;

namespace Guldaan.Security.Api.Features.Subscriptions.Queries.GetSubscriptionLinkedTenantForOwner
{
    public class GetSubscriptionLinkedTenantForOwnerHandler(SubscriptionQueryService queryService, ICurrentUser currentUser)
    {
        private readonly SubscriptionQueryService _queryService = queryService;
        private readonly ICurrentUser _currentUser = currentUser;

        public async Task<Either<IFeatureError, TenantWithLinkedUsers>> Handle(Guid tenantId)
        {
            return await _queryService.GetSelectedSubscriptionForOwnerAsync(_currentUser.Id)
                .BindAsync(s => _queryService.GetSubscriptionLinkedTenantAsync(s.Id,tenantId));
        }
    }
}
