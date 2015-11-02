namespace MadMoose.CQRS
{
    /// <summary>
    /// Sealed class that represents a "void" type
    /// </summary>
    public sealed class Nothing
    {
        private readonly static Nothing atAll = new Nothing() ;

        private Nothing() {}

        public static Nothing AtAll { get { return atAll; } }
    }
}