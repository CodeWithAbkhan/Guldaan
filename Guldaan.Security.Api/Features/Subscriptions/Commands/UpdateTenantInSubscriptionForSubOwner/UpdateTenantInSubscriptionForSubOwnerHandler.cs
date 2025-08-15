using LanguageExt;
using Guldaan.Common.Api;
using Guldaan.Common.Errors;
using Guldaan.Security.Api.Features.Subscriptions.Services.Poco;
using Guldaan.Security.Api.Features.Subscriptions.Services;
using Guldaan.Security.Contracts.Subscriptions.Commands;
using Guldaan.Security.Api.Mappers;

namespace Guldaan.Security.Api.Features.Subscriptions.Commands.UpdateTenantInSubscriptionForSubOwner
{
    public class UpdateTenantInSubscriptionForSubOwnerHandler(SubscriptionCommandService commandService, ICurrentUser currentUser)
    {
        private readonly SubscriptionCommandService _commandService = commandService;
        private readonly ICurrentUser _currentUser = currentUser;

        public async Task<Either<IFeatureError, TenantWithLinkedUsers>> Handle(UpdateSubscriptionLinkedTenantCommand command, Guid currentId)
        {
            var current = command.MapToTenant(currentId);

            return await _commandService.GetSelectedSubscriptionForOwnerAsync(_currentUser.Id)
                .BindAsync(s=> _commandService.GetTenantInSubscriptionAsyc(current.Id,current.SubscriptionId,s))
                .BindAsync(ts => _commandService.ValidateTenantLimitForSubscriptionAsync(ts.Tenant, ts.Subscription, true))
                .BindAsync(t => _commandService.MapTenantInDbContextAsync(t, current))
                .BindAsync(t => _commandService.PrepareUserIdsForAttachAndDetachFromTenantAsync(t, command.LinkedUsersIds, true))
                .BindAsync(tu => _commandService.UpdateTenantWithLinkedUsersInDbAsync(tu.Tenant, tu.UserIdsForInsert, tu.UserIdsForDel));
        }
    }
}
