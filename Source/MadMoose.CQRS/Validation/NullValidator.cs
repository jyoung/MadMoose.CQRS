namespace MadMoose.CQRS.Validation
{
    using FluentValidation;

    /// <summary>
    /// NullValidator that is used when a validator is not supplied for a type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NullValidator<T> : AbstractValidator<T>
    {
        public NullValidator()
        {
            // no validation rules
        }
    }
}