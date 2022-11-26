namespace Services
{
    /// <summary>
    /// Represents a collection of the <see cref="IServiceItem"/>.
    /// </summary>
    public interface IServiceCollection : ICollection<IServiceItem>, IServiceProvider, IServiceDescriptorProvider, IInitializable
    {
        /// <summary>
        /// Adds a new the <see cref="IServiceItem"/> with the specified <paramref name="serviceType"/> and <paramref name="descriptor"/> as the <see cref="IServiceItem.ServiceType"/> and the <see cref="IServiceItem.Descriptor"/> respectively.
        /// </summary>
        /// <param name="serviceType">The <see cref="IServiceItem.ServiceType"/>.</param>
        /// <param name="descriptor">The <see cref="IServiceItem.Descriptor"/>.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        IServiceCollection Add(Type serviceType, IServiceDescriptor descriptor);
    }
}