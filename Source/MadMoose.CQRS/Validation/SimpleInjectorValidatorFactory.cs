namespace MadMoose.CQRS.Validation
{
    using System;
    using FluentValidation;
    using SimpleInjector;

    public class SimpleInjectorValidatorFactory : ValidatorFactoryBase
    {
        private readonly Container container;
        public SimpleInjectorValidatorFactory(Container container)
        {
            this.container = container;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            return container.GetInstance(validatorType) as IValidator;
        }
    }
}