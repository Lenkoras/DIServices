namespace Services.Factories
{
    internal sealed class ConstantImplementationFactory : IImplementationFactory
    {
        private readonly object? constantValue;

        public ConstantImplementationFactory(object? constantValue)
        {
            this.constantValue = constantValue;
        }

        public object? Invoke(IServiceProvider services) =>
            constantValue;
    }
}