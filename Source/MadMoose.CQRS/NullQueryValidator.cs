namespace MadMoose.CQRS
{
    using System.Threading.Tasks;
    using FluentValidation.Results;

    public class NullQueryValidator<TQuery> : IQueryValidator<TQuery> where TQuery : IQuery
    {
        public async Task<ValidationResult> ValidateAsync(TQuery query)
        {
            return await Task.FromResult(new ValidationResult());
        }
    }
}