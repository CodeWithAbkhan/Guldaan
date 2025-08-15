using Guldaan.Security.Api.Data.Models;
using Guldaan.Security.Api.Features.Authorizations.Services;
using Guldaan.Security.Api.Features.Roles.Services;
using Guldaan.Security.Api.Features.Roles.Services.Poco;

namespace Guldaan.Security.Api.Features.Roles.Queries.GetAllRolesForAdmin
{
    public class GetAllRolesForAdminHandler(RoleQueryService query)
    {
        private readonly RoleQueryService _query = query;
        public async Task<IEnumerable<RoleWithAuthorizationIds>> Handle()
        {
            return await _query.GetAllRolesForAdminAsync();
        }
    }
}
