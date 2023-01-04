
namespace HolzShots.IO.Naming;

[Serializable]
public class PatternSyntaxException : FormatException
{
    public PatternSyntaxException()
        : base()
    { }
    public PatternSyntaxException(string message)
        : base(message)
    { }
    public PatternSyntaxException(string message, Exception innerException)
        : base(message, innerException)
    { }
}
