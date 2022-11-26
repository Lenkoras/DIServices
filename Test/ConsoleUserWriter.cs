using System.Text;

public class ConsoleUserWriter : IWriter<User>
{
    private StringBuilder builder;
    private int index;

    public ConsoleUserWriter()
    {
        builder = new();
        index = 1;
    }

    public void Append(User user)
    {
        OnAppend();
        builder
            .AppendLine()
            .Append(index++)
            .Append(". Name: ")
            .Append(user.Name);
    }

    public void Write()
    {
        OnWrite();
        Console.WriteLine(builder);
    }

    protected virtual void OnAppend()
    {
        if (builder.Length < 1)
        {
            builder.Append("Users: ");
        }
    }

    protected virtual void OnWrite()
    {
        if (builder.Length < 1)
        {
            builder.Append("Users are missing");
        }
    }
}
