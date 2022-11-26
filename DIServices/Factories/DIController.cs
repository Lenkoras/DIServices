namespace Services.Factories
{
    public class DIController : IDIController
    {
        private HashSet<Type> dependencies;

        public DIController(IEnumerable<Type> dependencies)
        {
            if (dependencies == null)
            {
                throw new ArgumentNullException(nameof(dependencies), $"A {nameof(dependencies)} collection must be not null.");
            }
            this.dependencies = dependencies.ToHashSet();
        }

        public bool Contains(Type type) =>
            dependencies.Contains(type);
    }
}