using FluentValidation;

namespace Users.Application.Common.Models;

public abstract class BaseValidator<T> : AbstractValidator<T> { }
