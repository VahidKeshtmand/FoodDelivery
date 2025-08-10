using FluentValidation;

namespace User.Application.Common.Models;

public abstract class BaseValidator<T> : AbstractValidator<T> { }
