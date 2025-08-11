using User.Application.Common.Models;
using User.Application.Features.Accounts.Commands;
using FluentValidation;

namespace User.Application.Features.Accounts.Validators;

//public sealed class RegisterUserValidator : BaseValidator<RegisterUserCommand>
//{
//    public RegisterUserValidator() {

//        RuleFor(x => x.FirstName)
//            .NotEmpty()
//            .MaximumLength(100)
//            .WithName("نام");

//        RuleFor(x => x.LastName)
//            .NotEmpty()
//            .MaximumLength(200)
//            .WithName("نام خانوادگی");

//        RuleFor(x => x.NationalId)
//            .NotEmpty()
//            .Length(10)
//            .WithName("کدملی");

//        RuleFor(x => x.PhoneNumber)
//            .NotEmpty()
//            .Length(11)
//            .WithName("شماره موبایل");

//        RuleFor(x => x.Email)
//            .NotEmpty()
//            .MinimumLength(2)
//            .MaximumLength(100)
//            .EmailAddress()
//            .WithName("ایمیل");

//        RuleFor(x => x.Password)
//            .NotEmpty()
//            .MinimumLength(4)
//            .MaximumLength(20)
//            .WithName("رمز عبور");

//        RuleFor(x => x.RePassword)
//            .NotEmpty()
//            .MinimumLength(4)
//            .MaximumLength(20)
//            .Matches(x => x.Password)
//            .WithName("رمز عبور");
//    }
//}
