using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using HolzShots.Composition;
using HolzShots.IO;
using HolzShots.Windows.Forms.Controls;

namespace HolzShots.Windows.Forms
{
    public partial class PluginForm : Form
    {
        private readonly PluginFormModel _model;
        public PluginForm(PluginFormModel model)
        {
            InitializeComponent();
            Debug.Assert(model != null);

            SuspendLayout();

            _model = model;
            InitializeModel();

            ResumeLayout(true);
        }

        private void InitializeModel()
        {
            var items = _model.Plugins.Select(p => new PluginItem(p)).ToArray();
            PluginPanel.Controls.AddRange(items);
        }

        private void CloseButton_Click(object sender, EventArgs e) => Close();

        private void OpenPluginsDirectoryLabel_LinkClicked(object sender, EventArgs e)
        {
            HolzShotsPaths.OpenFolderInExplorer(_model.PluginDirectory);
        }
    }

    public record PluginFormModel(IReadOnlyList<IPluginMetadata> Plugins, string PluginDirectory);
}
