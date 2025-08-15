using Guldaan.Security.Api.Data.Models;
using Guldaan.Security.Api.Features.Tenants.Services;

namespace Guldaan.Security.Api.Features.Tenants.Queries.GetAllTenantsForAdmin
{
    public class GetAllTenantsForAdminHandler(TenantQueryService query)
    {
        private readonly TenantQueryService _query = query;
        public async Task<IEnumerable<TenantModel>> Handle()
        {
            return await _query.GetAllTenantsAsync();
        }
    }
}
