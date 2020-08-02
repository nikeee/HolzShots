using System.Threading.Tasks;

namespace HolzShots.Composition.Command
{
    public interface ICommand
    {
        public abstract Task Invoke();
    }
}
