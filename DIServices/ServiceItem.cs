namespace Services
{
    public struct ServiceItem : IServiceItem
    {
        public Type ServiceType { get; }

        public IServiceDescriptor Descriptor { get; }

        private int hashCode;

        public ServiceItem(Type serviceType, IServiceDescriptor descriptor)
        {
            if (serviceType is null)
            {
                throw new ArgumentNullException(nameof(serviceType), $"A {nameof(serviceType)} can not be null.");
            }
            ServiceType = serviceType;
            hashCode = ServiceType.GetHashCode();
            Descriptor = descriptor;
        }

        public override int GetHashCode() =>
            hashCode;

        public static ServiceItem From<TService>(IServiceDescriptor descriptor) =>
            new ServiceItem(typeof(TService), descriptor);
    }
}