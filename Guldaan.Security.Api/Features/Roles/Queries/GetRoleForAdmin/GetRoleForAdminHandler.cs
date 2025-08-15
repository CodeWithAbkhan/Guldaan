using LanguageExt;
using Guldaan.Common.Errors;
using Guldaan.Security.Api.Data.Models;
using Guldaan.Security.Api.Features.Authorizations.Services;
using Guldaan.Security.Api.Features.Roles.Services;
using Guldaan.Security.Api.Features.Roles.Services.Poco;

namespace Guldaan.Security.Api.Features.Roles.Queries.GetRoleForAdmin
{
    public class GetRoleForAdminHandler(RoleQueryService query)
    {
        private readonly RoleQueryService _query = query;
        public async Task<Either<IFeatureError, RoleWithAuthorizationIds>> Handle(Guid id)
        {
            return await _query.GetRoleForAdminAsync(id);
        }
    }
}
