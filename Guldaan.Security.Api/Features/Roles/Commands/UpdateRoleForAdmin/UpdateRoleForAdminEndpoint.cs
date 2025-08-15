using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Guldaan.Common.Errors;
using Guldaan.Common.Http;
using Guldaan.Security.Api.Features.Roles.Services.Poco;
using Guldaan.Security.Api.Mappers;
using Guldaan.Security.Contracts.Authorizations.Results;
using Guldaan.Security.Contracts.Roles.Commands;
using Guldaan.Security.Contracts.Roles.Results;

namespace Guldaan.Security.Api.Features.Roles.Commands.UpdateRoleForAdmin
{
    public class UpdateRoleForAdminEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("admin/roles/{id:guid}",
                async Task<Results<Ok<RoleAdminResult>, ProblemHttpResult>> (
                    [FromRoute] Guid id,
                    [FromBody] UpdateRoleCommand command,
                    [FromServices] UpdateRoleForAdminHandler handler,
                    [FromServices] UpdateRoleForAdminValidator validator) =>
                {
                    var validationResult = await validator.ValidateAsync(command);
                    if (!validationResult.IsValid)
                    {
                        return CustomTypedResults.Problem(validationResult.ToDictionary());
                    }

                    var result = await handler.Handle(command, id);

                    return result.Match<Results<Ok<RoleAdminResult>, ProblemHttpResult>>(
                        ok => TypedResults.Ok(ok.MapToRolesAdminResult()),
                        err => CustomTypedResults.Problem(err));
                })
            .WithSummary("Update a role (For admin)")
            .WithDescription("This endpoint updates an existing role in the system. (For admin)")
            .WithTags("Roles")
            .ProducesProblem(400)
            .ProducesProblem(409);
        }
    }
}
