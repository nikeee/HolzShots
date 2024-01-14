namespace HolzShots.Composition;

[Serializable]
public class PluginLoadingFailedException : Exception
{
    public PluginLoadingFailedException()
        : base()
    { }
    public PluginLoadingFailedException(string message)
        : base(message)
    { }
    public PluginLoadingFailedException(Exception innerException)
        : this("Failed to load plugins", innerException)
    { }
    public PluginLoadingFailedException(string message, Exception innerException)
        : base(message, innerException)
    { }
}
