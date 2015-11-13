namespace MadMoose.CQRS.Validation
{
    using System;
    using System.Linq;
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
            var validators = container.GetAllInstances(validatorType);
            if (validators.Count() > 1)
            {
                // error
            }

            return validators.First() as IValidator;
        }
    }
}