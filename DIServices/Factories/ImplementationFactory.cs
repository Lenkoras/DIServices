namespace Services.Factories
{
    public abstract class ImplementationFactory : IImplementationFactory, IDIConstructor
    {
        public virtual IDIController GetController() => EmptyDIController.Instance;

        public abstract object? Invoke(IServiceProvider services);
    }
}