using System.Drawing;

namespace HolzShots.Windows.Forms;

public class BackgroundForm : System.Windows.Forms.Form
{
    public BackgroundForm(Point location, Size size)
    {
        StartPosition = System.Windows.Forms.FormStartPosition.Manual;
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        BackColor = Color.Black;
        Size = size;
        Location = location;
        ShowInTaskbar = false;
    }
}
