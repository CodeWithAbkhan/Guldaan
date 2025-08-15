using FluentValidation;
using Guldaan.Security.Contracts.Users.Commands;

namespace Guldaan.Security.Api.Features.Users.Commands.UpdateUserSettingsMe
{
    public class UpdateUserSettingsMeValidator : AbstractValidator<SetSettingsUserMeCommand>
    {
        public UpdateUserSettingsMeValidator()
        {
            RuleFor(x => x.TenantId)
                .NotEmpty().WithMessage("TenantId is required.");
        }
    }
}
