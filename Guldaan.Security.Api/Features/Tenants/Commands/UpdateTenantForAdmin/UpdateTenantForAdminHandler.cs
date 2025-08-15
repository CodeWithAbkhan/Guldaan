using LanguageExt;
using Guldaan.Common.Errors;
using Guldaan.Security.Api.Data.Models;
using Guldaan.Security.Api.Features.Tenants.Services;
using Guldaan.Security.Api.Mappers;
using Guldaan.Security.Contracts.Tenants.Commands;

namespace Guldaan.Security.Api.Features.Tenants.Commands.UpdateTenantForAdmin
{
    public class UpdateTenantForAdminHandler(TenantCommandService commandService)
    {
        private readonly TenantCommandService _commandService = commandService;

        public async Task<Either<IFeatureError, TenantModel>> Handle(UpdateTenantCommand command, Guid currentId)
        {
            var upd = command.MapToTenant(currentId);

            return await _commandService.GetTenantByIdAsync(upd.Id)
                    .BindAsync(t => _commandService.MapInDbContextAsync(t, upd))
                    .BindAsync(_commandService.ValidateIfSubscriptionExistsAsync)
                    .BindAsync(ts => _commandService.ValidateTenantLimitForSubscriptionAsync(ts.Item1, ts.Item2,true))
                    .BindAsync(_commandService.UpdateTenantInDbAsync);
        }
    }
}
