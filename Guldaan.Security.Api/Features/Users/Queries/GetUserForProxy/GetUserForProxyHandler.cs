using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Guldaan.Common.Errors;
using Guldaan.Security.Api.Data;
using Guldaan.Security.Api.Data.Models;
using Guldaan.Security.Api.Features.Users.Services;
using Guldaan.Security.Api.Features.Users.Services.Poco;

namespace Guldaan.Security.Api.Features.Users.Queries.GetUserForProxy
{
    public class GetUserForProxyHandler(UserQueryService query)
    {
        private readonly UserQueryService _query = query;

        public async Task<Either<IFeatureError,UserWithSubscriptionInfo>> 
            Handle(string oAuthId)
        {
            return await _query.GetUserByAuthId(oAuthId)
                    .BindAsync(_query.IsActivatedInSelectedSubscription)
                    .BindAsync(_query.FillOwnedSubscribtionIdsAsync)
                    .BindAsync(_query.GetTenantRolesAndAuthorizationsAsync);
                    
        }
    }
}
