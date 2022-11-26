namespace Services
{
    public interface IImplementationFactory
    {
        object? Invoke(IServiceProvider services);
    }
}