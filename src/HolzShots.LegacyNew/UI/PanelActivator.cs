using System.Diagnostics;
using HolzShots.Drawing.Tools;

namespace HolzShots.UI;

public class PanelActivator
{
    private readonly Control _panel;
    public PanelActivator(Control panel)
    {
        ArgumentNullException.ThrowIfNull(panel);
        _panel = panel;
    }

    public void CreateSettingsPanel(ITool<ToolSettingsBase> tool)
    {
        Debug.Assert(tool.SettingsControl != null);
        Debug.Assert(_panel != null);

        var controlRaw = tool.SettingsControl;
        Debug.Assert(controlRaw != null);
        Debug.Assert(controlRaw is UserControl);

        var control = (Control)controlRaw;
        control.Dock = DockStyle.Fill;

        _panel.Controls.Add(control);

        _panel.Visible = true;
        _panel.BringToFront();
    }

    public void ClearSettingsPanel()
    {
        var toRemove = new List<Control>();
        foreach (var i in _panel.Controls)
        {
            if (i is Control c)
                toRemove.Add(c);
        }

        _panel.Controls.Clear();

        foreach (var c in toRemove)
            c.Dispose();
    }
}
