namespace MadMoose.CQRS.Tests
{
    using System.Reflection;
    using SimpleInjector;
    using Xunit;

    public class RegistryTests 
    {
        [Fact]
        public void When_registering_assembly_components_it_verifys()
        {
            // ARRAGE
            var container = new Container();

            // register objects with the container
            var assemblies = new[] {Assembly.GetExecutingAssembly()};

            // ACT
            CQRSRegistry.Register(container, assemblies);

            // ASSERT
            container.Verify();
        }
       
    }
}
