using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Guldaan.Common.Errors;
using Guldaan.Common.Http;
using Guldaan.Security.Api.Features.Tenants.Queries.GetTenantForAdmin;
using Guldaan.Security.Api.Mappers;
using Guldaan.Security.Contracts.Tenants.Results;

namespace Guldaan.Security.Api.Features.Tenants.Queries.GetTenant
{
    public class GetTenantEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("tenants/{id:guid}",
                async Task<Results<Ok<TenantStandardResult>, ProblemHttpResult>> (
                    [FromRoute] Guid id,
                    [FromServices] GetTenantForAdminHandler handler) =>
                {
                    var result = await handler.Handle(id);

                    return result.Match<Results<Ok<TenantStandardResult>, ProblemHttpResult>>(
                         ok => TypedResults.Ok(ok.MapToTenantStandardResult()),
                         err => CustomTypedResults.Problem(err));
                })
            .WithSummary("Get a tenant by id")
            .WithDescription("This endpoint get a tenant by id. The user need to be linked to this tenant with his current connection.")
            .WithTags("Tenants")
            .ProducesProblem(404);
        }
    }
}
