using Services.Factories;
using System.Collections;
using static Services.ServiceCollection;

namespace Services
{
    /// <summary>
    /// Represents a default <see cref="IServiceCollection"/>.
    /// </summary>
    public partial class ServiceCollection : IServiceCollection
    {
        private readonly HashSet<IServiceItem> services;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceCollection"/> class.
        /// </summary>
        public ServiceCollection()
        {
            services = new()
            {
                ServiceItem.From<IServiceProvider>(new Descriptors.TransientServiceDescriptor(new ConstantImplementationFactory(new ServiceProvider(this)))) // reference to service provider
            };
        }

        /// <inheritdoc/>
        public object? GetService(Type serviceType) =>
            GetServiceDescriptor(serviceType)?.Get(this);

        /// <inheritdoc/>
        public IServiceDescriptor? GetServiceDescriptor(Type serviceType)
        {
            if (serviceType is null)
            {
                throw new ArgumentNullException(nameof(serviceType), $"A {nameof(serviceType)} can not be null.");
            }
            int hashCode = serviceType.GetHashCode();

            foreach (var item in services)
            {
                if (item.GetHashCode().Equals(hashCode))
                {
                    return item.Descriptor;
                }
            }
            return null;
        }

        /// <summary>
        /// Adds a <paramref name="descriptor"/> to the service collection with the <typeparamref name="TService"/> type.
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        public IServiceCollection Add<TService>(IServiceDescriptor descriptor) =>
            Add(typeof(TService), descriptor);

        /// <inheritdoc/>
        public IServiceCollection Add(Type serviceType, IServiceDescriptor descriptor)
        {
            Add(new ServiceItem(serviceType, descriptor));
            return this;
        }

        /// <summary>
        /// Adds an <paramref name="item"/> to the service collection.
        /// </summary>
        /// <param name="item">The service item to add to the service collection.</param>
        /// <remarks>
        /// <para>
        /// The <see cref="ArgumentNullException"/> throws if a <paramref name="item"/> is <see langword="null"/>.
        /// </para>
        /// <para>
        /// The <see cref="ArgumentException"/> throws if a <paramref name="item"/> <see cref="IServiceItem.ServiceType"/> or <see cref="IServiceItem.Descriptor"/> is <see langword="null"/>.
        /// </para>
        /// </remarks>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void Add(IServiceItem item)
        {
            ThrowIfNull(item);

            services.Add(
                item.ServiceType.GetHashCode().Equals(item.GetHashCode()) ? // adds original item if it correct overrides GetHashCode
                item :
                new ServiceItemWrapper(item)); // else uses the service item wrapper
        }

        /// <summary>
        /// Initializes all service descriptors in the service collection.
        /// </summary>
        public void Initialize()
        {
            Dictionary<Type, IDIController> controllerMap = new();
            foreach (IServiceItem serviceItem in services)
            {
                serviceItem.Descriptor.Initialize();
                if (serviceItem.Descriptor is IDIConstructor ctor)
                {
                    controllerMap.Add(serviceItem.ServiceType, ctor.GetController());
                }
            }

            
        }


        /// <inheritdoc/>
        public void Clear() =>
            services.Clear();

        /// <inheritdoc/>
        public bool Contains(IServiceItem item) =>
            services.Contains(item);

        /// <inheritdoc/>
        public void CopyTo(IServiceItem[] array, int arrayIndex) =>
            services.CopyTo(array, arrayIndex);

        /// <inheritdoc/>
        public bool Remove(IServiceItem item) =>
            services.Remove(item);

        /// <inheritdoc/>
        public IEnumerator<IServiceItem> GetEnumerator() =>
            services.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        /// <inheritdoc/>
        public int Count => services.Count;

        bool ICollection<IServiceItem>.IsReadOnly => false;

        #region ThrowHandlers
        private static void ThrowIfNull(IServiceItem item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item),
                    $"A {nameof(item)} can not be null.");
            }
            if (item.ServiceType is null)
            {
                throw new ArgumentException($"The {nameof(IServiceItem.ServiceType)} in a {nameof(item)} was null, but it's unacceptable.",
                    nameof(item));
            }
            if (item.Descriptor is null)
            {
                throw new ArgumentException($"The {nameof(IServiceItem.Descriptor)} in a {nameof(item)} was null, but it's unacceptable.",
                    nameof(item));
            }
        }
        #endregion ThrowHandlers

        private class ServiceProvider : IServiceProvider
        {
            private IServiceCollection Collection { get; }

            public ServiceProvider(IServiceCollection collection)
            {
                if (collection == null)
                {
                    throw new ArgumentNullException(nameof(collection));
                }
                Collection = collection;
            }

            public object? GetService(Type serviceType) =>
                Collection.GetService(serviceType);
        }
    }
}