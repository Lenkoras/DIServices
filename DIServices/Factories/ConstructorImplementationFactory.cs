using System.Reflection;

namespace Services.Factories
{
    internal sealed class ConstructorImplementationFactory : ImplementationFactory, IInitializable
    {
        private ConstructorInfo? ctor;
        private IServiceDescriptor[] parameterDescriptors;
        private IInitializer<IConstructorFactoryInitializerResult> factoryInitializer;

        public ConstructorImplementationFactory(IServiceDescriptorProvider provider, Type implementationType)
        {
            if (implementationType is null)
            {
                throw new ArgumentNullException(nameof(implementationType), $"A {nameof(implementationType)} can not be null.");
            }
            if (implementationType.IsAbstract || implementationType.IsInterface)
            {
                throw new ArgumentException(nameof(implementationType), $"The {implementationType.Name} is invalid implementation type. Abstract classes and interfaces can not be created.");
            }
            factoryInitializer = new ConstructorFactoryInitializer(provider, implementationType);
            parameterDescriptors = Array.Empty<IServiceDescriptor>();
        }

        public override object Invoke(IServiceProvider services) =>
            ctor?.Invoke(
                parameterDescriptors.Length < 1
                ?
                null
                :
                parameterDescriptors
                    .Select(descriptor => descriptor.Get(services))
                    .ToArray())
            ?? ThrowNotInitializedException<object>();

        public void Initialize()
        {
            if (factoryInitializer is not null && (ctor is null || parameterDescriptors is null))
            {
                var result = factoryInitializer.Initialize();
                factoryInitializer = null!;
                ThrowIfInvalidInitializationResult(ref result);

                ctor = result.ConstructorInfo;
                if (result.Descriptors?.Count() > 0)
                {
                    parameterDescriptors = result.Descriptors.ToArray();
                }
            }
            else
            {
#if !RELEASE
                throw new Exception(
                    "Unnecessary initialization invocation. The implementation factory already contain all needed components, initialization is unnecessary.");
#endif
            }
        }
        public override IDIController GetController() =>
            ctor != null ?
            new ConstructorDIController(ctor) :
            ThrowNotInitializedException<IDIController>();

        private static void ThrowIfInvalidInitializationResult(ref IConstructorFactoryInitializerResult result)
        {
            if (result.ConstructorInfo is null)
            {
                throw new ArgumentNullException(
                    $"Invalid initialization result. The {nameof(IConstructorFactoryInitializerResult.ConstructorInfo)} was null!");
            }
        }

        private TResult ThrowNotInitializedException<TResult>() =>
            throw new InvalidOperationException(
                $"The {nameof(IImplementationFactory)} is not initialized. To get the {typeof(TResult).Name} use a {nameof(IInitializable.Initialize)} method to invoke factory initialization.");

    }
}