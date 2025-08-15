using LanguageExt;
using Guldaan.Common.Api;
using Guldaan.Common.Errors;
using Guldaan.Security.Api.Data.Models;
using Guldaan.Security.Api.Features.Users.Services;
using Guldaan.Security.Api.Features.Users.Services.Poco;

namespace Guldaan.Security.Api.Features.Users.Queries.GetUserMe
{
    public class GetUserMeHandler(UserQueryService query, ICurrentUser currentUser)
    {
        private readonly UserQueryService _query = query;
        public async Task<Either<IFeatureError, UserWithSubscriptionInfo>> Handle()
        {
            return await _query.GetUserById(currentUser.Id)
                    .BindAsync(_query.IsActivatedInSelectedSubscription)
                    .BindAsync(_query.FillOwnedSubscribtionIdsAsync)
                    .BindAsync(_query.GetTenantRolesAndAuthorizationsAsync);
        }
    }
}
