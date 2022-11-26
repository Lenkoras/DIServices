namespace Services.Factories
{
    internal abstract class FactoryInitializer<TResult> : IInitializer<TResult>
    {
        protected IServiceDescriptorProvider DescriptorProvider { get; }

        public FactoryInitializer(IServiceDescriptorProvider provider)
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider), $"A {nameof(provider)} can not be null.");
            }
            DescriptorProvider = provider;
        }

        public abstract TResult Initialize();
    }
}