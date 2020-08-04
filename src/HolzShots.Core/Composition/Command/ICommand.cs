using System.Threading.Tasks;
using System.Collections.Generic;

namespace HolzShots.Composition.Command
{
    public interface ICommand
    {
        Task Invoke(IReadOnlyDictionary<string, string> parameters);
    }
}
