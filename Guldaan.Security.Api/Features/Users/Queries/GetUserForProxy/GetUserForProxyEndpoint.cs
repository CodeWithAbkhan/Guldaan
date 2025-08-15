using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Guldaan.Common.Errors;
using Guldaan.Common.Http;
using Guldaan.Security.Api.Mappers;
using Guldaan.Security.Contracts.Users.Results;

namespace Guldaan.Security.Api.Features.Users.Queries.GetUserForProxy
{
    public class GetUserForProxyEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("proxy/users",
                async Task<Results<Ok<UserProxyResult>, ProblemHttpResult>> (
                    [FromQuery] string authid,
                    [FromServices] GetUserForProxyHandler handler) =>
                {
                    var result = await handler.Handle(authid);

                    return result.Match<Results<Ok<UserProxyResult>, ProblemHttpResult>>(
                         ok => TypedResults.Ok(ok.MapToUserProxyResult()),
                         err => CustomTypedResults.Problem(err));
                })
            .WithSummary("Get a user by authid (proxy)")
            .WithDescription("This endpoint get user information (exclusively reserved for proxy calls).")
            .WithTags("Users")
            .ProducesProblem(404);
        }
    }
}
