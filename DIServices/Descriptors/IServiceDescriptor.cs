namespace Services
{
    /// <summary>
    /// Represents the descriptor of a value in the service collection.
    /// </summary>
    public interface IServiceDescriptor : IInitializable
    {
        /// <summary>
        /// Gets a value of this descriptor.
        /// </summary>
        /// <param name="services">The service provider to possible dependency resolving.</param>
        /// <returns>A value of this descriptor.</returns>
        object? Get(IServiceProvider services);

        /// <summary>
        /// Gets a value of this descriptor casted to the <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to explicit cast.</typeparam>
        /// <param name="services">The service provider to possible dependency resolving.</param>
        /// <returns>A value of this descriptor.</returns>
        T? Get<T>(IServiceProvider services) =>
            (T?)Get(services);
    }
}