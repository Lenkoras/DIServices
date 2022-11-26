namespace Services.Factories
{
    internal struct EmptyDIController : IDIController
    {
        public static EmptyDIController Instance { get; }

        static EmptyDIController()
        {
            Instance = new();
        }

        public bool Contains(Type type) =>
            false;
    }
}