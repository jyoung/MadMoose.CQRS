namespace MadMoose.CQRS.Specifications
{
    using System.Threading.Tasks;
    using FluentValidation.Results;

    public class FakeCommandValidator : ICommandValidator<FakeCommand>
    {
        public async Task<ValidationResult> ValidateAsync(FakeCommand command)
        {
            return await Task.FromResult(new ValidationResult());
        }
    }
}