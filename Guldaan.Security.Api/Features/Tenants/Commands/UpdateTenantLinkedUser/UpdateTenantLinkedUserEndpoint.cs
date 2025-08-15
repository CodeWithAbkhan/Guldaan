using LanguageExt;
using Guldaan.Common.Api;
using Guldaan.Common.Errors;
using Guldaan.Security.Api.Features.Subscriptions.Services.Poco;
using Guldaan.Security.Api.Features.Subscriptions.Services;
using Guldaan.Security.Contracts.Subscriptions.Commands;
using Guldaan.Security.Api.Features.Tenants.Services;
using Guldaan.Security.Contracts.Users.Commands;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Http.HttpResults;
using Guldaan.Common.Http;
using Guldaan.Security.Api.Features.Subscriptions.Commands.UpdateTenantInSubscriptionForSubOwner;
using Guldaan.Security.Contracts.Tenants.Results;
using Microsoft.AspNetCore.Mvc;
using LanguageExt.Pipes;
using System;

namespace Guldaan.Security.Api.Features.Tenants.Commands.UpdateTenantLinkedUser
{
    public class UpdateTenantLinkedUserEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("tenants/{id:guid}/users/{userId:guid}/update-roles",
                async Task<Results<Ok<bool>, ProblemHttpResult>> (
                    [FromBody] UpdateUserWithTenantRoleIdsCommand command,
                    [FromRoute] Guid id,
                    [FromRoute] Guid userId,
                    [FromServices] UpdateTenantLinkedUserHandler handler) =>
                {
                    var result = await handler.Handle(command, id, userId);

                    return result.Match<Results<Ok<bool>, ProblemHttpResult>>(
                        ok => TypedResults.Ok(ok),
                        err => CustomTypedResults.Problem(err));

                })
            .WithSummary("Update the roles of the user in a tenant")
            .WithDescription("This endpoint update the attached roles to a user in a tenant.")
            .WithTags("Tenants")
            .ProducesProblem(400)
            .ProducesProblem(404);
        }
    }
}


