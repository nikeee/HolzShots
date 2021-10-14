using System.Windows.Forms;

namespace HolzShots.Windows.Forms
{
    public partial class FlyoutForm : Form
    {
        public FlyoutForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.Manual;
        }

        private void FormLoad(object sender, EventArgs e)
        {
            MaximumSize = Size;
            MinimumSize = Size;
        }
    }
}
