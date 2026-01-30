using System.Diagnostics;
using HolzShots.Drawing.Tools;

namespace HolzShots.UI;

public class PanelActivator(Control panel)
{
    private readonly Control _panel = panel ?? throw new ArgumentNullException(nameof(panel));

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
        List<Control> toRemove = [.. _panel.Controls.OfType<Control>()];

        _panel.Controls.Clear();

        foreach (var c in toRemove)
            c.Dispose();
    }
}
