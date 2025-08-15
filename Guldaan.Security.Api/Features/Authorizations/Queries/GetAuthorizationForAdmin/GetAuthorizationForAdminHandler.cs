using LanguageExt;
using Guldaan.Common.Errors;
using Guldaan.Security.Api.Data.Models;
using Guldaan.Security.Api.Features.Authorizations.Services;

namespace Guldaan.Security.Api.Features.Authorizations.Queries.GetAuthorizationForAdmin
{
    public class GetAuthorizationForAdminHandler(AuthorizationQueryService query)
    {
        private readonly AuthorizationQueryService _query = query;
        public async Task<Either<IFeatureError,AuthorizationModel>> Handle(Guid id)
        {
            return await _query.GetAuthorizationAsync(id);
        }
    }
}
