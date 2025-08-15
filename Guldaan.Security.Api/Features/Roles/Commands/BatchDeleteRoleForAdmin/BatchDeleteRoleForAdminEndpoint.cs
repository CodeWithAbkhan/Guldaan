using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Guldaan.Common.Errors;
using Guldaan.Common.Http;
using Guldaan.Security.Api.Features.Authorizations.Commands.BatchDeleteAuthorizationForAdmin;
using Guldaan.Security.Contracts.Authorizations.Commands;
using Guldaan.Security.Contracts.Roles.Commands;

namespace Guldaan.Security.Api.Features.Roles.Commands.BatchDeleteRoleForAdmin
{
    public class BatchDeleteRoleForAdminEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("admin/roles/commands/batch-delete",
                async Task<Results<NoContent, ProblemHttpResult>> (
                    [FromBody] BatchDeleteRoleCommand command,
                    [FromServices] BatchDeleteRoleForAdminHandler handler) =>
                {
                    var result = await handler.Handle(command);

                    return result.Match<Results<NoContent, ProblemHttpResult>>(
                        _ => TypedResults.NoContent(),
                        err => CustomTypedResults.Problem(err));
                })
            .WithSummary("Delete multiple roles (For admin)")
            .WithDescription("This endpoint deletes multiple roles by Ids from the system. (For admin)")
            .WithTags("Roles")
            .ProducesProblem(400);
        }
    }
}
