using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Guldaan.Common.Errors;
using Guldaan.Common.Http;
using Guldaan.Security.Api.Mappers;
using Guldaan.Security.Contracts.Subscriptions.Results;

namespace Guldaan.Security.Api.Features.Subscriptions.Queries.GetSubscriptionForOwner
{
    public class GetSubscriptionForOwnerEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("subowner/subscriptions/selected",
                async Task<Results<Ok<SubscriptionOwnerResult>, ProblemHttpResult>> (
                    [FromServices] GetSubscriptionForOwnerHandler handler) =>
                {
                    var result = await handler.Handle();

                    return result.Match<Results<Ok<SubscriptionOwnerResult>, ProblemHttpResult>>(
                        ok => TypedResults.Ok(ok.MapToSubscriptionOwnerResult()),
                        err => CustomTypedResults.Problem(err));
                })
            .WithSummary("Get subscription for owner")
            .WithDescription("This endpoint retrieves the subscription details if the user is the owner.")
            .WithTags("Subscriptions")
            .ProducesProblem(404);
        }
    }
}
