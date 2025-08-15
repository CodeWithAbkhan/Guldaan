using LanguageExt;
using Guldaan.Common.Api;
using Guldaan.Common.Errors;
using Guldaan.Security.Api.Features.Tenants.Services.Poco;
using Guldaan.Security.Api.Features.Tenants.Services;

namespace Guldaan.Security.Api.Features.Tenants.Queries.GetTenantLinkedUser
{
    public class GetTenantLinkedUserHandler(TenantQueryService query, ICurrentUser currentUser)
    {
        private readonly TenantQueryService _query = query;
        private readonly ICurrentUser _currentUser = currentUser;

        public async Task<Either<IFeatureError, UserWithLinkedRoles>> Handle(Guid tenantId, Guid userId)
        {
            return _currentUser.TenantId != tenantId
                ? new ResourceNotFoundError("Tenant", new Dictionary<string, string>()
                {
                    { "Id", tenantId.ToString() }
                })
                : await _query.GetTenantByIdAsync(tenantId)
                .BindAsync(t=>_query.GetTenantUserWithRolesAsync(t,userId));
        }
    }
}
