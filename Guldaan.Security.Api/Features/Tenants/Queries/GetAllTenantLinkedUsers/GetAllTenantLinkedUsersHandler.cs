using LanguageExt;
using Guldaan.Common.Api;
using Guldaan.Common.Errors;
using Guldaan.Security.Api.Data.Models;
using Guldaan.Security.Api.Features.Tenants.Services;
using Guldaan.Security.Api.Features.Tenants.Services.Poco;

namespace Guldaan.Security.Api.Features.Tenants.Queries.GetAllTenantLinkedUsers
{
    public class GetAllTenantLinkedUsersHandler(TenantQueryService query, ICurrentUser currentUser)
    {
        private readonly TenantQueryService _query = query;
        private readonly ICurrentUser _currentUser = currentUser;

        public async Task<Either<IFeatureError, IEnumerable<UserWithLinkedRoles>>> Handle(Guid id)
        {
            return _currentUser.TenantId != id
                ? new ResourceNotFoundError("Tenant", new Dictionary<string, string>()
                {
                    { "Id", id.ToString() }
                })
                : await _query.GetTenantByIdAsync(id)
                .BindAsync(_query.GetAllTenantUsersWithRolesAsync);
        }
    }
}
