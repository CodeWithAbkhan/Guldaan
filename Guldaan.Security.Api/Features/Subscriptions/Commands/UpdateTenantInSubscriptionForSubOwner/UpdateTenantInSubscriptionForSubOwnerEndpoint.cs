using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Guldaan.Common.Errors;
using Guldaan.Common.Http;
using Guldaan.Security.Api.Features.Subscriptions.Commands.AddTenantInSubscriptionForSubOwner;
using Guldaan.Security.Api.Mappers;
using Guldaan.Security.Contracts.Subscriptions.Commands;
using Guldaan.Security.Contracts.Tenants.Results;

namespace Guldaan.Security.Api.Features.Subscriptions.Commands.UpdateTenantInSubscriptionForSubOwner
{
    public class UpdateTenantInSubscriptionForSubOwnerEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("subowner/subscriptions/selected/tenants/{Id:guid}",
                async Task<Results<Ok<TenantSubOwnerResult>, ProblemHttpResult>> (
                    [FromBody] UpdateSubscriptionLinkedTenantCommand command,
                    [FromRoute] Guid Id,
                    [FromServices] UpdateTenantInSubscriptionForSubOwnerHandler handler,
                    [FromServices] UpdateTenantInSubscriptionForSubOwnerValidator validator) =>
                {
                    var validationResult = await validator.ValidateAsync(command);
                    if (!validationResult.IsValid)
                    {
                        return CustomTypedResults.Problem(validationResult.ToDictionary());
                    }

                    var result = await handler.Handle(command,Id);

                    return result.Match<Results<Ok<TenantSubOwnerResult>, ProblemHttpResult>>(
                        ok => TypedResults.Ok(ok.MapToTenantSubOwnerResult()),
                        err => CustomTypedResults.Problem(err));

                })
            .WithSummary("Update a tenant to a subscription")
            .WithDescription("This endpoint update a tenant for the selected subscription of a sub owner")
            .WithTags("Subscriptions")
            .ProducesProblem(400)
            .ProducesProblem(404)
            .ProducesProblem(409);
        }
    }
}
