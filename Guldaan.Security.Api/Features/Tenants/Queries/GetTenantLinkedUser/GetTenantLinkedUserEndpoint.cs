using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Guldaan.Common.Errors;
using Guldaan.Common.Http;
using Guldaan.Security.Api.Features.Tenants.Queries.GetAllTenantLinkedUsers;
using Guldaan.Security.Api.Mappers;
using Guldaan.Security.Contracts.Users.Results;

namespace Guldaan.Security.Api.Features.Tenants.Queries.GetTenantLinkedUser
{
    public class GetTenantLinkedUserEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("tenants/{id:guid}/users/{userId:guid}",
                async Task<Results<Ok<UserWithTenantRoleIdsResult>, ProblemHttpResult>> (
                    [FromRoute] Guid id,
                    [FromRoute] Guid userId,
                    [FromServices] GetTenantLinkedUserHandler handler) =>
                {
                    var result = await handler.Handle(id,userId);

                    return result.Match<Results<Ok<UserWithTenantRoleIdsResult>, ProblemHttpResult>>(
                         ok => TypedResults.Ok(ok.MapToUserWithTenantRoleIdsResult()),
                         err => CustomTypedResults.Problem(err));
                })
            .WithSummary("Get a user in a tenant with linked roles Ids")
            .WithDescription("This endpoint get a user in a tenant with his role ids.")
            .WithTags("Tenants")
            .ProducesProblem(404);
        }
    }
}
