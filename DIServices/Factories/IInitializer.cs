namespace Services.Factories
{
    internal interface IInitializer<TResult>
    {
        TResult Initialize();
    }
}