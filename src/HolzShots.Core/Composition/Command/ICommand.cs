using System.Threading.Tasks;

namespace HolzShots.Composition.Command
{
    public interface ICommand
    {
        // string Name { get; }
        Task Invoke();
    }
}
