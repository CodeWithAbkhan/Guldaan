using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Guldaan.Common.Errors;
using Guldaan.Common.Http;
using Guldaan.Security.Contracts.Authorizations.Commands;

namespace Guldaan.Security.Api.Features.Authorizations.Commands.BatchDeleteAuthorizationForAdmin
{
    public class BatchDeleteAuthorizationForAdminEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("admin/authorizations/commands/batch-delete",
                async Task<Results<NoContent, ProblemHttpResult>> (
                    [FromBody] BatchDeleteAuthorizationCommand command,
                    [FromServices] BatchDeleteAuthorizationForAdminHandler handler) =>
                {
                    var result = await handler.Handle(command);

                    return result.Match<Results<NoContent, ProblemHttpResult>>(
                        _ => TypedResults.NoContent(),
                        err => CustomTypedResults.Problem(err));
                })
            .WithSummary("Delete multiple authorizations (For admin)")
            .WithDescription("This endpoint deletes multiple authorizations by Ids from the system. (For admin)")
            .WithTags("Authorizations")
            .ProducesProblem(400);
        }
    }
}
