using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Guldaan.Common.Http;
using Guldaan.Security.Api.Mappers;
using Guldaan.Security.Contracts.Tenants.Results;

namespace Guldaan.Security.Api.Features.Tenants.Queries.GetAllTenantsForAdmin
{
    public class GetAllTenantsForAdminEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("admin/tenants",
                async Task<Ok<IEnumerable<TenantStandardResult>>>(
                    [FromServices] GetAllTenantsForAdminHandler handler) =>
                {
                    var tenants = await handler.Handle();
                    return TypedResults.Ok(tenants.MapToTenantStandardResults());
                })
            .WithSummary("Get all tenants (admin)")
            .WithDescription("This endpoint get all tenants registred in the system.")
            .WithTags("Tenants");
        }
    }
}
