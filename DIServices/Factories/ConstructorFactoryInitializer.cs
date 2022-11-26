using System.Reflection;

namespace Services.Factories
{
    internal sealed class ConstructorFactoryInitializer : FactoryInitializer<IConstructorFactoryInitializerResult>
    {
        private Type implementationType;

        public ConstructorFactoryInitializer(IServiceDescriptorProvider provider, Type implementationType) : base(provider)
        {
            if (implementationType is null)
            {
                throw new ArgumentNullException(nameof(implementationType), $"A {nameof(implementationType)} can not be null.");
            }
            this.implementationType = implementationType;
        }

        /// <summary>
        /// Initializer need initialization too.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public override IConstructorFactoryInitializerResult Initialize()
        {
            ConstructorInfo ctor = FindBestConstructor(implementationType, DescriptorProvider, out IEnumerable<IServiceDescriptor>? parameterDescriptors);
            return new Result(ctor, parameterDescriptors);
        }

        private static ConstructorInfo FindBestConstructor(Type implementationType, IServiceDescriptorProvider provider, out IEnumerable<IServiceDescriptor>? parameterDescriptors)
        {
            ConstructorInfo[] constructors = implementationType.GetConstructors();
            if (constructors.Length < 1)
            {
                throw new InvalidOperationException($"The {implementationType.Name} does not contain public constructors, so it can not be constructed.");
            }
            else if (constructors.Length > 1) // if class contains only one (1) constructor, then sort invocation is unnecessary
            {
                Array.Sort(constructors, CompareByParametersCountDescending); // sorting constructors by descending to start iteration from constructor with highest parameters count
            }

            foreach (var ctor in constructors)
            {
                if (TryBuildDescriptors(provider, ctor, out parameterDescriptors))
                {
                    return ctor;
                }
            }

            throw new InvalidOperationException($"Can not initialize implementation factory. Same type in constructor of the {implementationType.Name} can not be resolved.");
        }

        private static bool TryBuildDescriptors(IServiceDescriptorProvider provider, ConstructorInfo ctor, out IEnumerable<IServiceDescriptor>? parameterDescriptors)
        {
            var parameters = ctor.GetParameters();
            if (parameters.Length < 1)
            {
                parameterDescriptors = null;
                return true;
            }
            IServiceDescriptor[] descriptors = new IServiceDescriptor[parameters.Length];

            for (int parameterIndex = 0; parameterIndex < parameters.Length; parameterIndex++)
            {
                Type serviceType = parameters[parameterIndex].ParameterType;

                IServiceDescriptor? descriptor = provider.GetServiceDescriptor(serviceType);
                if (descriptor is null) // if descriptor not found
                {

                    // interrupt array filling and return not found
                    parameterDescriptors = null!;
                    return false;
                }
                descriptors[parameterIndex] = descriptor;
            }

            parameterDescriptors = descriptors;
            return true;
        }

        private static int CompareByParametersCountDescending(ConstructorInfo a, ConstructorInfo b) =>
            b.GetParameters().Length.CompareTo(a.GetParameters().Length);

        private readonly record struct Result(ConstructorInfo ConstructorInfo, IEnumerable<IServiceDescriptor>? Descriptors) : IConstructorFactoryInitializerResult;
    }
}