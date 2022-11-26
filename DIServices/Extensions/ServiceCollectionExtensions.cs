using Services.Descriptors;
using Services.Factories;

namespace Services
{
    public static class ServiceCollectionExtensions
    {
        #region GetRequiredService
        public static TService GetRequiredService<TService>(this IServiceCollection services) =>
            (TService)services.GetRequiredService(typeof(TService));

        public static object GetRequiredService(this IServiceCollection services, Type serviceType)
        {
            var service = services.GetService(serviceType);
            if (service is null)
            {
                throw new InvalidOperationException($"The type {serviceType.Name} can not be resolve. Service descriptor not found or returned value is null.");
            }
            return service;
        }
        #endregion GetRequiredService

        #region AddSingleton
        public static IServiceCollection AddSingleton(this IServiceCollection services, Type serviceType, IImplementationFactory implementationFactory) =>
            services.Add(serviceType, new SingletonServiceDescriptor(implementationFactory));

        public static IServiceCollection AddSingleton<TService>(this IServiceCollection services, IImplementationFactory implementationFactory) =>
            services.AddSingleton(typeof(TService), implementationFactory);

        public static IServiceCollection AddSingleton<TService>(this IServiceCollection services) =>
            services.AddSingleton<TService, TService>();

        public static IServiceCollection AddSingleton<TService, TImplementation>(this IServiceCollection services) =>
            services.AddSingleton<TService>(typeof(TImplementation));

        public static IServiceCollection AddSingleton<TService>(this IServiceCollection services, Type implementationType) =>
            services.AddSingleton(typeof(TService), implementationType);

        public static IServiceCollection AddSingleton(this IServiceCollection services, Type serviceType, Type implementationType) =>
            services.AddSingleton(serviceType, new ConstructorImplementationFactory(services, implementationType));

        public static IServiceCollection AddSingleton(this IServiceCollection services, Type serviceType, Func<IServiceProvider, object?> implementationFactory) =>
            services.AddSingleton(serviceType, new FuncImplementationFactory<object>(implementationFactory));

        public static IServiceCollection AddSingleton<TService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> implementationFactory) =>
            services.AddSingleton(typeof(TService), new FuncImplementationFactory<TImplementation>(implementationFactory));

        public static IServiceCollection AddSingleton<TService>(this IServiceCollection services, Func<IServiceProvider, TService?> implementationFactory) =>
            services.AddSingleton<TService, TService>(implementationFactory!);
        #endregion AddSingleton

        #region AddTransient
        public static IServiceCollection AddTransient(this IServiceCollection services, Type serviceType, IImplementationFactory implementationFactory) =>
            services.Add(serviceType, new TransientServiceDescriptor(implementationFactory));

        public static IServiceCollection AddTransient<TService>(this IServiceCollection services, IImplementationFactory implementationFactory) =>
            services.AddTransient(typeof(TService), implementationFactory);

        public static IServiceCollection AddTransient<TService>(this IServiceCollection services) =>
            services.AddTransient<TService, TService>();

        public static IServiceCollection AddTransient<TService, TImplementation>(this IServiceCollection services) =>
            services.AddTransient<TService>(typeof(TImplementation));

        public static IServiceCollection AddTransient<TService>(this IServiceCollection services, Type implementationType) =>
            services.AddTransient(typeof(TService), implementationType);

        public static IServiceCollection AddTransient(this IServiceCollection services, Type serviceType, Type implementationType) =>
            services.AddTransient(serviceType, new ConstructorImplementationFactory(services, implementationType));

        public static IServiceCollection AddTransient(this IServiceCollection services, Type serviceType, Func<IServiceProvider, object?> implementationFactory) =>
            services.AddTransient(serviceType, new FuncImplementationFactory<object>(implementationFactory));

        public static IServiceCollection AddTransient<TService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> implementationFactory) =>
            services.AddTransient(typeof(TService), new FuncImplementationFactory<TImplementation>(implementationFactory));

        public static IServiceCollection AddTransient<TService>(this IServiceCollection services, Func<IServiceProvider, TService?> implementationFactory) =>
            services.AddTransient<TService, TService>(implementationFactory!);
        #endregion AddTransient

        /// <summary>
        /// Adds a new service to the collection that returns specified <paramref name="constantService"/> by the <typeparamref name="TService"/> type.
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="services"></param>
        /// <param name="constantService">Value that will return by type the <typeparamref name="TService"/>.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddValue<TService>(this IServiceCollection services, TService? constantService) =>
            services.AddTransient<TService>(new ConstantImplementationFactory(constantService));
    }
}