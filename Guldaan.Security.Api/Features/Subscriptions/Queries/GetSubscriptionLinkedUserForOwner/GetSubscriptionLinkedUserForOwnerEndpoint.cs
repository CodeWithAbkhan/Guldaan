using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Guldaan.Common.Errors;
using Guldaan.Common.Http;
using Guldaan.Security.Api.Features.Subscriptions.Queries.GetSubscriptionLinkedTenantForOwner;
using Guldaan.Security.Api.Mappers;
using Guldaan.Security.Contracts.Tenants.Results;
using Guldaan.Security.Contracts.Users.Results;

namespace Guldaan.Security.Api.Features.Subscriptions.Queries.GetSubscriptionLinkedUserForOwner
{
    public class GetSubscriptionLinkedUserForOwnerEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("subowner/subscriptions/selected/users/{id:Guid}",
                async Task<Results<Ok<UserWithInfoSubOwnerResult>, ProblemHttpResult>> (
                    [FromRoute] Guid id,
                    [FromServices] GetSubscriptionLinkedUserForOwnerHandler handler) =>
                {
                    var result = await handler.Handle(id);

                    return result.Match<Results<Ok<UserWithInfoSubOwnerResult>, ProblemHttpResult>>(
                        ok => TypedResults.Ok(ok.MapToUserWithInfoSubOwnerResult()),
                        err => CustomTypedResults.Problem(err));
                })
            .WithSummary("Get user linked to a subscription for owner")
            .WithDescription("This endpoint retrieves a user linked to a subscription if the user is the owner.")
            .WithTags("Subscriptions")
            .ProducesProblem(404)
            .ProducesProblem(400);
        }
    }
}
