using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace HolzShots.Windows.Forms
{
    public partial class PluginForm : Form
    {
        private static Lazy<PluginForm> _instance = new Lazy<PluginForm>(() => new PluginForm());
        public static PluginForm Instance => _instance.Value;

        public PluginForm()
        {
            InitializeComponent();
        }
    }
}
