using LanguageExt;
using Guldaan.Common.Errors;
using Guldaan.Security.Api.Data.Models;
using Guldaan.Security.Api.Features.Tenants.Services;
using Guldaan.Security.Api.Mappers;
using Guldaan.Security.Contracts.Tenants.Commands;


namespace Guldaan.Security.Api.Features.Tenants.Commands.AddTenantForAdmin
{
    public class AddTenantForAdminHandler(TenantCommandService commandService)
    {
        private readonly TenantCommandService _commandService = commandService;

        public async Task<Either<IFeatureError, TenantModel>> Handle(AddTenantCommand command)
        {
            var tenant = command.MapToTenant();

            return await _commandService.ValidateIfSubscriptionExistsAsync(tenant)
                    .BindAsync(ts => _commandService.ValidateTenantLimitForSubscriptionAsync(ts.Item1, ts.Item2,false))
                    .BindAsync(_commandService.AddTenantInDbAsync);
        }
    }
}
