namespace Services.Factories
{
    internal class FuncImplementationFactory<TResult> : IImplementationFactory
    {
        private readonly Func<IServiceProvider, TResult?> implementationFactory;

        public FuncImplementationFactory(Func<IServiceProvider, TResult?> implementationFactory)
        {
            if (implementationFactory is null)
            {
                throw new ArgumentNullException(nameof(implementationFactory),
                    $"A {nameof(implementationFactory)} can not be null.");
            }
            this.implementationFactory = implementationFactory;
        }

        public TResult? Invoke(IServiceProvider services) =>
            implementationFactory.Invoke(services);

        object? IImplementationFactory.Invoke(IServiceProvider services) =>
            Invoke(services);
    }
}