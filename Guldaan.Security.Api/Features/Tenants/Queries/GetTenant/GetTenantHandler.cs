using LanguageExt;
using Guldaan.Common.Api;
using Guldaan.Common.Errors;
using Guldaan.Security.Api.Data.Models;
using Guldaan.Security.Api.Features.Tenants.Services;

namespace Guldaan.Security.Api.Features.Tenants.Queries.GetTenant
{
    public class GetTenantHandler(TenantQueryService query, ICurrentUser currentUser)
    {
        private readonly TenantQueryService _query = query;
        private readonly ICurrentUser _currentUser = currentUser;

        public async Task<Either<IFeatureError, TenantModel>> Handle(Guid id)
        {
            //The simple user can only access the tenant associated with his current request (connection).
            return _currentUser.TenantId != id
                ? new ResourceNotFoundError("Tenant", new Dictionary<string,string>()
                {
                    { "Id", id.ToString() }
                })
                : await _query.GetTenantByIdAsync(id);
        }
    }
}
