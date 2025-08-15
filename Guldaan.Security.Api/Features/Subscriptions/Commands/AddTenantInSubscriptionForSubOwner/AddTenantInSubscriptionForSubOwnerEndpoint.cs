using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Guldaan.Common.Errors;
using Guldaan.Common.Http;
using Guldaan.Security.Api.Features.Authorizations.Commands.AddAuthorizationForAdmin;
using Guldaan.Security.Api.Mappers;
using Guldaan.Security.Contracts.Authorizations.Commands;
using Guldaan.Security.Contracts.Authorizations.Results;
using Guldaan.Security.Contracts.Subscriptions.Commands;
using Guldaan.Security.Contracts.Tenants.Results;

namespace Guldaan.Security.Api.Features.Subscriptions.Commands.AddTenantInSubscriptionForSubOwner
{
    public class AddTenantInSubscriptionForSubOwnerEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("subowner/subscriptions/selected/tenants",
                async Task<Results<Created<TenantSubOwnerResult>, ProblemHttpResult>> (
                    [FromBody] AddSubscriptionLinkedTenantCommand command,
                    [FromServices] AddTenantInSubscriptionForSubOwnerHandler handler,
                    [FromServices] AddTenantInSubscriptionForSubOwnerValidator validator) =>
                {
                    var validationResult = await validator.ValidateAsync(command);
                    if (!validationResult.IsValid)
                    {
                        return CustomTypedResults.Problem(validationResult.ToDictionary());
                    }

                    var result = await handler.Handle(command);

                    return result.Match<Results<Created<TenantSubOwnerResult>, ProblemHttpResult>>(
                        ok => TypedResults.Created($"subowner/subscriptions/selected/tenants/{ok.Id}", ok.MapToTenantSubOwnerResult()),
                        err => CustomTypedResults.Problem(err));

                })
            .WithSummary("Add a tenant to a subscription")
            .WithDescription("This endpoint adds a new tenant for the selected subscription of a sub owner")
            .WithTags("Subscriptions")
            .ProducesProblem(400);
        }
    }
}
