using LanguageExt;
using Guldaan.Common.Api;
using Guldaan.Common.Errors;
using Guldaan.Security.Api.Data.Models;
using Guldaan.Security.Api.Features.Subscriptions.Services;
using Guldaan.Security.Api.Features.Tenants.Services;

namespace Guldaan.Security.Api.Features.Tenants.Queries.GetAllTenantsMe
{
    public class GetAllTenantsMeHandler(TenantQueryService queryService, ICurrentUser currentUser)
    {
        private readonly TenantQueryService _queryService = queryService;
        private readonly ICurrentUser _currentUser = currentUser;

        public async Task<IEnumerable<TenantModel>> Handle()
        {
            return await _queryService.GetAllTenantsForUserAsync(_currentUser.Id);
        }
    }
}
