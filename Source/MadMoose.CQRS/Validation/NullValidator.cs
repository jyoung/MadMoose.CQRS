namespace MadMoose.CQRS.Validation
{
    using FluentValidation;

    public class NullValidator<T> : AbstractValidator<T>
    {
        public NullValidator()
        {
            // no validation rules
        }
    }
}