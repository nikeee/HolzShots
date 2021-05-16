using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolzShots.Input.Selection
{
    /// <summary> This isn't a semaphore and not even a thread-safe. We just use this for assertions.</summary>
    public static class SelectionSemaphore
    {
        public static bool IsInAreaSelection { get; set; }
    }
}
