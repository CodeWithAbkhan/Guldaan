using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Guldaan.Common.Errors;
using Guldaan.Common.Http;
using Guldaan.Security.Api.Mappers;
using Guldaan.Security.Contracts.Tenants.Results;

namespace Guldaan.Security.Api.Features.Tenants.Queries.GetAllTenantsMe
{
    public class GetAllTenantsMeEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("me/tenants",
                async Task<Results<Ok<IEnumerable<TenantStandardResult>>, ProblemHttpResult>> (
                    [FromServices] GetAllTenantsMeHandler handler) =>
                {
                    var result = await handler.Handle();

                    return TypedResults.Ok(result.MapToTenantStandardResults());
                })
            .WithSummary("Get all tenants for the current user")
            .WithDescription("This endpoint gets all tenants for the current user.")
            .WithTags("Tenants");
        }
    }
}
