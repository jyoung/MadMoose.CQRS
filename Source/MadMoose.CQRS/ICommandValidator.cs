using System.Threading.Tasks;
using FluentValidation.Results;

namespace MadMoose.CQRS
{
    public interface ICommandValidator<in TCommand> where TCommand : ICommand
    {
        Task<ValidationResult> ValidateAsync(TCommand command);
    }
}