using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Guldaan.Common.Errors;
using Guldaan.Common.Http;
using Guldaan.Security.Api.Mappers;
using Guldaan.Security.Contracts.Tenants.Commands;
using Guldaan.Security.Contracts.Tenants.Results;

namespace Guldaan.Security.Api.Features.Tenants.Commands.AddTenantForAdmin
{
    public class AddTenantForAdminEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("admin/tenants",
                async Task<Results<Created<TenantStandardResult>, ProblemHttpResult>> (
                    [FromBody] AddTenantCommand command,
                    [FromServices] AddTenantForAdminHandler handler,
                    [FromServices] AddTenantForAdminValidator validator) =>
                {
                    var validationResult = await validator.ValidateAsync(command);
                    if (!validationResult.IsValid)
                    {
                        return CustomTypedResults.Problem(validationResult.ToDictionary());
                    }

                    var result = await handler.Handle(command);

                    return result.Match<Results<Created<TenantStandardResult>, ProblemHttpResult>>(
                        ok => TypedResults.Created($"/admin/tenants/{ok.Id}", ok.MapToTenantStandardResult()),
                        err => CustomTypedResults.Problem(err));

                })
            .WithSummary("Add a tenant (For admin)")
            .WithDescription("This endpoint add a new tenant in the system. (For admin)")
            .WithTags("Tenants")
            .ProducesProblem(400);

        }
    }
}
