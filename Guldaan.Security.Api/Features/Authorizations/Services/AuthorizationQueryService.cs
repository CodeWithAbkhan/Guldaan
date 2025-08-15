using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Guldaan.Common.Errors;
using Guldaan.Security.Api.Data;
using Guldaan.Security.Api.Data.Models;

namespace Guldaan.Security.Api.Features.Authorizations.Services
{
    public class AuthorizationQueryService(SecurityDbContext ctx)
    {
        public async Task<IEnumerable<AuthorizationModel>> GetAllAuthorizationsAsync()
        {
            return await ctx.Authorizations.ToListAsync();
        }

        public async Task<Either<IFeatureError, AuthorizationModel>> GetAuthorizationAsync(Guid Id)
        {
            var result = await ctx.Authorizations.FindAsync(Id);

            return result == null
                ? new ResourceNotFoundError("Authorization", new Dictionary<string, string>()
                    {
                        { "Id", Id.ToString() }
                    })
                : result;
        }
    }
}
