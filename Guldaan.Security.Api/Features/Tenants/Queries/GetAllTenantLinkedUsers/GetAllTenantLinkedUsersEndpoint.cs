using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Guldaan.Common.Errors;
using Guldaan.Common.Http;
using Guldaan.Security.Api.Features.Tenants.Queries.GetTenantForAdmin;
using Guldaan.Security.Api.Mappers;
using Guldaan.Security.Contracts.Tenants.Results;
using Guldaan.Security.Contracts.Users.Results;

namespace Guldaan.Security.Api.Features.Tenants.Queries.GetAllTenantLinkedUsers
{
    public class GetAllTenantLinkedUsersEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("tenants/{id:guid}/users",
                async Task<Results<Ok<IEnumerable<UserWithTenantRoleIdsResult>>, ProblemHttpResult>> (
                    [FromRoute] Guid id,
                    [FromServices] GetAllTenantLinkedUsersHandler handler) =>
                {
                    var result = await handler.Handle(id);

                    return result.Match<Results<Ok<IEnumerable<UserWithTenantRoleIdsResult>>, ProblemHttpResult>>(
                         ok => TypedResults.Ok(ok.MapToUserWithTenantRoleIdsResult()),
                         err => CustomTypedResults.Problem(err));
                })
            .WithSummary("Get users in a tenant with linked roles Ids")
            .WithDescription("This endpoint get all user in a tenant with their role ids.")
            .WithTags("Tenants")
            .ProducesProblem(404);
        }
    }
}
