namespace MadMoose.CQRS.Tests.Fakes
{
    using Commands;

    public class FakeCommand : ICommand<Nothing>
    {
        public string Message { get; set; }
    }
}