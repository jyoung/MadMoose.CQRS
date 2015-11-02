namespace MadMoose.CQRS
{
    using System;
    using FluentValidation.Results;

    public class ValidationException : Exception
    {
        public ValidationResult ValidationResult { get; set; }

        public ValidationException(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }
    }
}