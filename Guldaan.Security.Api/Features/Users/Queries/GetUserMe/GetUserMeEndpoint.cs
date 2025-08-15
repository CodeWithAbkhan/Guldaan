using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Guldaan.Common.Errors;
using Guldaan.Common.Http;
using Guldaan.Security.Api.Features.Users.Queries.GetUserForProxy;
using Guldaan.Security.Api.Mappers;
using Guldaan.Security.Contracts.Users.Results;

namespace Guldaan.Security.Api.Features.Users.Queries.GetUserMe
{
    public class GetUserMeEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("me/authinfo",
                async Task<Results<Ok<UserMeResult>, ProblemHttpResult>> (
                    [FromServices] GetUserMeHandler handler) =>
                {
                    var result = await handler.Handle();

                    return result.Match<Results<Ok<UserMeResult>, ProblemHttpResult>>(
                         ok => TypedResults.Ok(ok.MapToUserMeResult()),
                         err => CustomTypedResults.Problem(err));
                })
            .WithSummary("Get me user info")
            .WithDescription("This endpoint get user information (exclusively reserved for me calls).")
            .WithTags("Users")
            .ProducesProblem(404);
        }
    }
}
