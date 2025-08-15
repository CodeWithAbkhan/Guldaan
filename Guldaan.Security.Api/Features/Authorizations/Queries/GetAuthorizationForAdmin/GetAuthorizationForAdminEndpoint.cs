using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Guldaan.Common.Errors;
using Guldaan.Common.Http;
using Guldaan.Security.Api.Features.Authorizations.Commands.UpdateAuthorizationForAdmin;
using Guldaan.Security.Api.Features.Authorizations.Queries.GetAllAuthorizationsForAdmin;
using Guldaan.Security.Api.Mappers;
using Guldaan.Security.Contracts.Authorizations.Commands;
using Guldaan.Security.Contracts.Authorizations.Results;

namespace Guldaan.Security.Api.Features.Authorizations.Queries.GetAuthorizationForAdmin
{
    public class GetAuthorizationForAdminEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("admin/authorizations/{id:guid}",
                async Task<Results<Ok<AuthorizationStandardResult>, ProblemHttpResult>> (
                    [FromRoute] Guid id,
                    [FromServices] GetAuthorizationForAdminHandler handler) =>
                {

                    var result = await handler.Handle(id);

                    return result.Match<Results<Ok<AuthorizationStandardResult>, ProblemHttpResult>>(
                        ok => TypedResults.Ok(ok.MapToAuthorizationStandardResult()),
                        err => CustomTypedResults.Problem(err));
                })
            .WithSummary("Get an authorization (For admin)")
            .WithDescription("This endpoint get an existing authorization in the system. (For admin)")
            .WithTags("Authorizations")
            .ProducesProblem(404);
        }
    }
}
