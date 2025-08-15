using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Guldaan.Common.Errors;
using Guldaan.Common.Http;
using Guldaan.Security.Api.Mappers;
using Guldaan.Security.Contracts.Tenants.Results;

namespace Guldaan.Security.Api.Features.Subscriptions.Queries.GetSubscriptionLinkedTenantForOwner
{
    public class GetSubscriptionLinkedTenantForOwnerEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("subowner/subscriptions/selected/tenants/{id:Guid}",
                async Task<Results<Ok<TenantSubOwnerResult>, ProblemHttpResult>> (
                    [FromRoute] Guid id,
                    [FromServices] GetSubscriptionLinkedTenantForOwnerHandler handler) =>
                {
                    var result = await handler.Handle(id);

                    return result.Match<Results<Ok<TenantSubOwnerResult>, ProblemHttpResult>>(
                        ok => TypedResults.Ok(ok.MapToTenantSubOwnerResult()),
                        err => CustomTypedResults.Problem(err));
                })
            .WithSummary("Get tenant linked to a subscription for owner")
            .WithDescription("This endpoint retrieves a tenant linked to a subscription if the user is the owner.")
            .WithTags("Subscriptions")
            .ProducesProblem(404)
            .ProducesProblem(400);
        }
    }
}
