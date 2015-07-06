namespace MadMoose.CQRS
{
    public sealed class Nothing
    {
        private readonly static Nothing atAll = new Nothing() ;

        private Nothing() {}

        public static Nothing AtAll { get { return atAll; } }
    }
}