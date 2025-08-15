using FluentValidation;
using Users.Application.Features.Customers.Commands;

namespace Users.Application.Features.Customers.Validators;

public sealed class RegisterCustomerCommandValidator : AbstractValidator<RegisterCustomerCommand>
{
    public RegisterCustomerCommandValidator() {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Length(11);

        RuleFor(x => x.Email)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(100)
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(4)
            .MaximumLength(20);

        RuleFor(x => x.RePassword)
            .NotEmpty()
            .MinimumLength(4)
            .MaximumLength(20)
            .Matches(x => x.Password);

        RuleFor(x => x.Addresses)
            .NotEmpty()
            .Must(x => x.Count <= 4)
            .WithMessage("You can insert maximum 4 address.");

        RuleFor(x => x.BirthDate)
            .Must(x => !x.HasValue || (x.Value > DateTime.Now.AddYears(-100) && x.Value < DateTime.Now.AddYears(-7)))
            .WithMessage("The birth date is not valid.");
    }
}
