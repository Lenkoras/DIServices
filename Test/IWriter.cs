public interface IWriter<T>
{
    void Append(T value);

    void Write();
}
