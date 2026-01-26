using FluentValidation;

namespace newApp.Infrastructure.Common
{
    public abstract class BaseValidator<T> : AbstractValidator<T>
    {
        protected BaseValidator()
        {
            // Common validation rules can be added here
        }
    }
}