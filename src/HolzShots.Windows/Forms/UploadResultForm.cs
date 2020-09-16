using System;
using System.Windows.Forms;

namespace HolzShots.Windows.Forms
{
    public partial class UploadResultForm : Form
    {
        public UploadResultForm()
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
