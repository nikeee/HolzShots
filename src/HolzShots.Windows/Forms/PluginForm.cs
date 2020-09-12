using System;
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

        private void CloseButton_Click(object sender, EventArgs e) => Close();
    }
}
