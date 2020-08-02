using System.Threading.Tasks;

namespace HolzShots.Composition.Command
{
    public interface ICommand
    {
        Task Invoke(params string[] parameters);
    }
}
