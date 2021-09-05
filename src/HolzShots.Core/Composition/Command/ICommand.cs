namespace HolzShots.Composition.Command
{
    public interface ICommand<TSettings>
    {
        Task Invoke(IReadOnlyDictionary<string, string> parameters, TSettings settingsContext);
    }
}
