using Services;

IServiceCollection collection = new ServiceCollection();

collection
    .AddValue<ICollection<User>>(new List<User>() { new("Eva"), new("VaiNa"), new("Armstrong"), new("Dee") })
    .AddTransient<IWriter<User>, ConsoleUserWriter>()
    .AddTransient<ICollectionWriter, CollectionWriter<User>>()
    .Initialize();

ICollectionWriter? room = collection.GetService<ICollectionWriter>();

room?.WriteAll();
