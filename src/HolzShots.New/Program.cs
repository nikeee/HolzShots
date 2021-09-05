using System;
using SingleInstanceCore;

namespace HolzShots.New
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            var isFirstInstance = HolzShotsApplication.Instance.InitializeAsFirstInstance("HolzShots");
            if (isFirstInstance)
            {
                try
                {
                    HolzShotsApplication.Instance.Run();
                }
                finally
                {
                    SingleInstance.Cleanup();
                }
            }
        }
    }
}
