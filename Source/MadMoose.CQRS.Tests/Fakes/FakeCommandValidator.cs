namespace MadMoose.CQRS.Tests.Fakes
{
    using FluentValidation;

    public class FakeCommandValidator : AbstractValidator<FakeCommand>
    {
        public FakeCommandValidator()
        {
            RuleFor(x => x.Message)
                .NotEmpty();
        }
    }
}