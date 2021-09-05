using System.Diagnostics;
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

    public class PluginFormModel
    {
        public IReadOnlyList<IPluginMetadata> Plugins { get; }
        public string PluginDirectory { get; }

        public PluginFormModel(IReadOnlyList<IPluginMetadata> plugins, string pluginDirectory)
        {
            Plugins = plugins ?? throw new ArgumentNullException(nameof(plugins));
            PluginDirectory = pluginDirectory ?? throw new ArgumentNullException(nameof(pluginDirectory));
        }
    }
}
