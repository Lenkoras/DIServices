namespace Services
{
    /// <summary>
    /// Defines a mechanism for retrieving the <see cref="IServiceDescriptor"/>.
    /// </summary>
    public interface IServiceDescriptorProvider
    {
        /// <summary>
        /// Gets the <see cref="IServiceDescriptor"/> by a specified <paramref name="serviceType"/>.
        /// </summary>
        /// <param name="serviceType">An object that specifies the type of the <see cref="IServiceDescriptor"/> to get.</param>
        /// <returns></returns>
        IServiceDescriptor? GetServiceDescriptor(Type serviceType);
    }
}