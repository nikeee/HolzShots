using System.Windows.Forms;
using SingleInstanceCore;

namespace HolzShots.New
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var app = new HolzShotsApplication();
            var isFirstInstance = app.InitializeAsFirstInstance("HolzShots");
            if (isFirstInstance)
            {
                try
                {
                    app.Run(args);
                }
                finally
                {
                    SingleInstance.Cleanup();
                }
            }
        }
    }

    public class HolzShotsApplication : ISingleInstance
    {
        MainForm _form = null!;
        public void Run(string[] args)
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(_form = new MainForm(this));
        }

        public void OnInstanceInvoked(string[] args)
        {
            int.TryParse(_form.Text, out int a);
            _form.Text = (a + 1).ToString();

            if (args.Length > 0)
            {
                _form.Text = args.Length.ToString();
            }
        }
    }
}
