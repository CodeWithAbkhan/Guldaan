using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Guldaan.Common.Errors;
using Guldaan.Common.Http;
using Guldaan.Security.Api.Features.Users.Commands.RegisterUser;
using Guldaan.Security.Contracts.Users.Commands;
using Guldaan.Security.Contracts.Users.Results;

namespace Guldaan.Security.Api.Features.Users.Commands.OnboardMeSimple
{
    public class OnboardMeSimpleEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("me/onboarding",
                async Task<Results<Ok<bool>, ProblemHttpResult>> (
                    [FromBody] OnboardMeSimpleCommand command,
                    [FromServices] OnboardMeSimpleHandler handler) =>
                {
                    var result = await handler.Handle(command);

                    return result.Match<Results<Ok<bool>, ProblemHttpResult>>(
                   ok => TypedResults.Ok(true),
                   err => CustomTypedResults.Problem(err));

                })
            .WithSummary("Onboard me endpoint (very simple use case)")
            .WithDescription("Onboard, it creates a linked sub and tenant with the base tenant role, if the activation code is correct.")
            .WithTags("Users")
            .ProducesProblem(400)
            .ProducesProblem(404);
        }
    }
}
