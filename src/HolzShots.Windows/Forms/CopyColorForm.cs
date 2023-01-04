using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace HolzShots.Windows.Forms;

public partial class CopyColorForm : Form
{
    private static readonly IReadOnlyList<Func<Color, string>> _formats = new List<Func<Color, string>>()
    {
        c => $"rgb({c.R}, {c.G}, {c.B})",
        c => $"#{c.R:x2}{c.G:x2}{c.B:x2}",
        c => $"{c.R:x2}{c.G:x2}{c.B:x2}",
        c => $"R:{c.R} G:{c.G} B:{c.B}",
        c => $"{c.R}, {c.G}, {c.B}",
        c => $"rgba({c.R}, {c.G}, {c.B}, {c.A / 255f:0.###})",
    };

    public CopyColorForm(Color color, Point invocationOrigin, int indexUnderMouse = 1)
    {
        InitializeComponent();
        SuspendLayout();

        colorBox.Color = color;

        StartPosition = FormStartPosition.Manual;

        var lastLabelPosition = new Point(12, 42);

        for (int i = 0; i < _formats.Count; ++i)
        {
            var format = _formats[i];
            var formattedColor = format(color);
            var l = new CopyCodeLinkLabel()
            {
                Text = formattedColor,
                Location = new Point(
                    lastLabelPosition.X,
                    lastLabelPosition.Y + 18 + 3
                ),
                Padding = new Padding(0, 0, 0, 10),
                AutoSize = true,
            };

            l.Click += (s, e) =>
            {
                ClipboardEx.SetText(formattedColor);
                Close();
            };

            Controls.Add(l);

            if (i == indexUnderMouse)
            {
                Location = new Point(
                    invocationOrigin.X - (l.Location.X + 20),
                    invocationOrigin.Y - (l.Location.Y + 20)
                );
            }

            lastLabelPosition = l.Location;
        }

        ResumeLayout(true);
    }
}
