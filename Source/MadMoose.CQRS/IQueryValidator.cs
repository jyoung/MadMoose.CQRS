namespace MadMoose.CQRS
{
    using System.Threading.Tasks;
    using FluentValidation.Results;

    public interface IQueryValidator<in TQuery> where TQuery : IQuery
    {
        Task<ValidationResult> ValidateAsync(TQuery query);
    }
}