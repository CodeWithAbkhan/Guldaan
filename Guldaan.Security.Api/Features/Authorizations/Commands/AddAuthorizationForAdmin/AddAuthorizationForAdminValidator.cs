using FluentValidation;
using Guldaan.Security.Contracts.Authorizations.Commands;
using Guldaan.Security.Contracts.Tenants.Commands;

namespace Guldaan.Security.Api.Features.Authorizations.Commands.AddAuthorizationForAdmin
{
    public class AddAuthorizationForAdminValidator : AbstractValidator<AddAuthorizationCommand>
    {
        public AddAuthorizationForAdminValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage("Code is required.")
                .MaximumLength(50)
                .WithMessage("Code must not exceed 50 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("Description must not exceed 500 characters.");
        }
    }
}
