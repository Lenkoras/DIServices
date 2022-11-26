namespace Services
{
    /// <summary>
    /// Represents a item collection of the <see cref="IServiceCollection"/>.
    /// </summary>
    public interface IServiceItem
    {
        /// <summary>
        /// Instance of the <see cref="Type"/> that used as key in the <see cref="IServiceCollection"/>.
        /// </summary>
        Type ServiceType { get; }

        /// <summary>
        /// The <see cref="IServiceDescriptor"/> of the <see cref="ServiceType"/>.
        /// </summary>
        IServiceDescriptor Descriptor { get; }
    }
}
