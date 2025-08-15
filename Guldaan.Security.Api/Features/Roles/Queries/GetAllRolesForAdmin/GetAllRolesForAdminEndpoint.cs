using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Guldaan.Common.Http;
using Guldaan.Security.Api.Mappers;
using Guldaan.Security.Contracts.Roles.Results;

namespace Guldaan.Security.Api.Features.Roles.Queries.GetAllRolesForAdmin
{
    public class GetAllRolesForAdminEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("admin/roles",
                async Task<Ok<IEnumerable<RoleAdminResult>>> (
                    [FromServices] GetAllRolesForAdminHandler handler) =>
                {
                    var result = await handler.Handle();
                    return TypedResults.Ok(result.MapToRolesAdminResults());
                })
            .WithSummary("Get all roles (admin)")
            .WithDescription("This endpoint get all roles registred in the system (for the admin).")
            .WithTags("Roles");
        }
    }
}
