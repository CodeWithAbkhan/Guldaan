using LanguageExt;
using Guldaan.Common.Errors;
using Guldaan.Security.Api.Data.Models;
using Guldaan.Security.Api.Features.Tenants.Services;

namespace Guldaan.Security.Api.Features.Tenants.Queries.GetTenantForAdmin
{
    public class GetTenantForAdminHandler(TenantQueryService query)
    {
        private readonly TenantQueryService _query = query;

        public async Task<Either<IFeatureError, TenantModel>> Handle(Guid id)
        {
            return await _query.GetTenantByIdForAdminAsync(id);
        }
    }
}
