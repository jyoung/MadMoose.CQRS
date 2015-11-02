namespace MadMoose.CQRS.Specifications.Fakes
{
    using FluentValidation;

    public class FakeQueryValidator : AbstractValidator<FakeQuery>
    {
        public FakeQueryValidator()
        {
            RuleFor(x => x.Parameter)
                .NotEmpty();
        }
    }
}