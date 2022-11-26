namespace Services.Descriptors
{
    internal sealed class SingletonServiceDescriptor : ServiceDescriptor
    {
        private object? value;

        public SingletonServiceDescriptor(IImplementationFactory implementationFactory) : base(implementationFactory) { }

        public override object? Get(IServiceProvider services) =>
            value is null ? (value = base.Get(services)) : value;
    }
}