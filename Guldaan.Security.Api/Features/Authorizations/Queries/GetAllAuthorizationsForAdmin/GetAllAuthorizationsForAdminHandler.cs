using Guldaan.Security.Api.Data.Models;
using Guldaan.Security.Api.Features.Authorizations.Services;

namespace Guldaan.Security.Api.Features.Authorizations.Queries.GetAllAuthorizationsForAdmin
{
    public class GetAllAuthorizationsForAdminHandler(AuthorizationQueryService query)
    {
        private readonly AuthorizationQueryService _query = query;
        public async Task<IEnumerable<AuthorizationModel>> Handle()
        {
            return await _query.GetAllAuthorizationsAsync();
        }
    }
}
