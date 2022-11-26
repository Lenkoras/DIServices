namespace Services
{
    public partial class ServiceCollection
    {
        /// <summary>
        /// The wrapper of original service item that gets and store <see cref="IServiceItem.ServiceType"/> the hash code.
        /// </summary>
        private struct ServiceItemWrapper : IServiceItem
        {
            private int hashCode;
            private IServiceItem serviceItem;

            /// <summary>
            /// Initializes a new instance of the <see cref="ServiceItemWrapper"/> class that gets and store <see cref="IServiceItem.ServiceType"/> the hash code.
            /// </summary>
            /// <param name="serviceItem">The original service item.</param>
            public ServiceItemWrapper(IServiceItem serviceItem)
            {
                this.serviceItem = serviceItem;
                hashCode = serviceItem.ServiceType.GetHashCode();
            }

            public Type ServiceType => serviceItem.ServiceType;

            public IServiceDescriptor Descriptor => serviceItem.Descriptor;

            /// <summary>
            /// Returns the hash code for the <see cref="IServiceItem.ServiceType"/> of the original service item.
            /// </summary>
            /// <inheritdoc/>
            public override int GetHashCode() =>
                hashCode;
        }
    }
}