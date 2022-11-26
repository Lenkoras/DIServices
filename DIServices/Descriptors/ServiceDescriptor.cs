using Services.Factories;

namespace Services.Descriptors
{
    public abstract class ServiceDescriptor : IServiceDescriptor, IDIConstructor
    {
        private IImplementationFactory implementationFactory;

        public ServiceDescriptor(IImplementationFactory implementationFactory)
        {
            if (implementationFactory is null)
            {
                throw new ArgumentNullException(nameof(implementationFactory), $"A {nameof(implementationFactory)} can not be null.");
            }
            this.implementationFactory = implementationFactory;
        }

        public virtual void Initialize()
        {
            if (implementationFactory is IInitializable factory)
            {
                factory.Initialize();
            }
        }

        public virtual object? Get(IServiceProvider services) =>
            implementationFactory.Invoke(services);

        public virtual IDIController GetController() =>
            implementationFactory is IDIConstructor ctor ? ctor.GetController() : EmptyDIController.Instance;
    }
}