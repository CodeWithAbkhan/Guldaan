using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Guldaan.Common.Errors;
using Guldaan.Common.Http;
using Guldaan.Security.Api.Features.Subscriptions.Commands.UpdateTenantInSubscriptionForSubOwner;
using Guldaan.Security.Api.Mappers;
using Guldaan.Security.Contracts.Subscriptions.Commands;
using Guldaan.Security.Contracts.Tenants.Results;
using Guldaan.Security.Contracts.Users.Results;

namespace Guldaan.Security.Api.Features.Subscriptions.Commands.UpdateUserInSubscriptionForSubOwner
{
    public class UpdateUserInSubscriptionForSubOwnerEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("subowner/subscriptions/selected/users/{Id:guid}",
                async Task<Results<Ok<UserWithInfoSubOwnerResult>, ProblemHttpResult>> (
                    [FromBody] UpdateSubscriptionLinkedUserCommand command,
                    [FromRoute] Guid Id,
                    [FromServices] UpdateUserInSubscriptionForSubOwnerHandler handler,
                    [FromServices] UpdateUserInSubscriptionForSubOwnerValidator validator) =>
                {
                    var validationResult = await validator.ValidateAsync(command);
                    if (!validationResult.IsValid)
                    {
                        return CustomTypedResults.Problem(validationResult.ToDictionary());
                    }

                    var result = await handler.Handle(command, Id);

                    return result.Match<Results<Ok<UserWithInfoSubOwnerResult>, ProblemHttpResult>>(
                        ok => TypedResults.Ok(ok.MapToUserWithInfoSubOwnerResult()),
                        err => CustomTypedResults.Problem(err));

                })
            .WithSummary("Update a user in a subscription")
            .WithDescription("This endpoint update a user for the selected subscription of a sub owner")
            .WithTags("Subscriptions")
            .ProducesProblem(400)
            .ProducesProblem(404)
            .ProducesProblem(409);
        }
    }
}
