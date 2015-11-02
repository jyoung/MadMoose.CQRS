namespace MadMoose.CQRS
{
    using System.Threading.Tasks;
    using FluentValidation.Results;

    public class NullCommandValidator<TCommand> : ICommandValidator<TCommand> where TCommand : ICommand
    {
        public async Task<ValidationResult> ValidateAsync(TCommand command)
        {
            return await Task.FromResult(new ValidationResult());
        }
    }
}