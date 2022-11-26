namespace Services.Descriptors
{
    internal sealed class TransientServiceDescriptor : ServiceDescriptor
    {
        public TransientServiceDescriptor(IImplementationFactory implementationFactory) : base(implementationFactory) { }
    }
}