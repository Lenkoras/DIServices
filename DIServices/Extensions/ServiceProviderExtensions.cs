namespace Services
{
    public static class ServiceProviderExtensions
    {
        /// <summary>
        /// Gets the service object of the specified <typeparamref name="TService"/> type.
        /// </summary>
        /// <typeparam name="TService">A type of service object to get.</typeparam>
        /// <param name="services">Service provider.</param>
        /// <returns>A service object of <see langword="typeof"/> <typeparamref name="TService"/>. -or- null if there is no service object of type serviceType.</returns>
        public static TService? GetService<TService>(this IServiceProvider services) =>
            (TService?)services.GetService(typeof(TService));
    }
}