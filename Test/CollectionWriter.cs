public class CollectionWriter<T> : ICollectionWriter
{
    private ICollection<T> collection { get; }

    private IWriter<T> writer;

    public CollectionWriter(ICollection<T> collection, IWriter<T> writer)
    {
        this.collection = collection;
        this.writer = writer;
    }

    public void WriteAll()
    {
        foreach (var user in collection)
        {
            writer.Append(user);
        }
        writer.Write();
    }
}
