using FluentValidation;
using Guldaan.Security.Contracts.Subscriptions.Commands;

namespace Guldaan.Security.Api.Features.Subscriptions.Commands.AddTenantInSubscriptionForSubOwner
{
    public class AddTenantInSubscriptionForSubOwnerValidator : AbstractValidator<AddSubscriptionLinkedTenantCommand>
    {
        public AddTenantInSubscriptionForSubOwnerValidator()
        {
            RuleFor(x => x.Label)
                .NotEmpty()
                .WithMessage("Label is required.")
                .MaximumLength(100)
                .WithMessage("Label must not exceed 100 characters.");

            RuleFor(x => x.SubscriptionId)
                .NotEmpty()
                .WithMessage("SubscriptionId is required.");
        }
    }
}
