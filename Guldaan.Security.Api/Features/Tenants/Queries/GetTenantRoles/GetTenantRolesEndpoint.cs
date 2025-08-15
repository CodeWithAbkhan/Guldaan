using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Guldaan.Common.Errors;
using Guldaan.Common.Http;
using Guldaan.Security.Api.Features.Tenants.Queries.GetTenantForAdmin;
using Guldaan.Security.Api.Mappers;
using Guldaan.Security.Contracts.Roles.Results;
using Guldaan.Security.Contracts.Tenants.Results;

namespace Guldaan.Security.Api.Features.Tenants.Queries.GetTenantRoles
{
    public class GetTenantRolesEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("tenants/{id:guid}/roles",
                async Task<Results<Ok<IEnumerable<RoleLightResult>>, ProblemHttpResult>> (
                    [FromRoute] Guid id,
                    [FromServices] GetTenantRolesHandler handler) =>
                {
                    var result = await handler.Handle(id);

                    return result.Match<Results<Ok<IEnumerable<RoleLightResult>>, ProblemHttpResult>>(
                         ok => TypedResults.Ok(ok.MapToRoleLightResults()),
                         err => CustomTypedResults.Problem(err));
                })
            .WithSummary("Get all possible roles for a tenant.")
            .WithDescription("This endpoint get a all possible roles that can be asigned to a specific tenants..")
            .WithTags("Tenants")
            .ProducesProblem(404);
        }
    }
}
