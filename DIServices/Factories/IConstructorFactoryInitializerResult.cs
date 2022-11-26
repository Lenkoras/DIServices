using System.Reflection;

namespace Services.Factories
{
    internal interface IConstructorFactoryInitializerResult
    {
        ConstructorInfo ConstructorInfo { get; }

        IEnumerable<IServiceDescriptor>? Descriptors { get; }
    }
}