using System.ComponentModel;
using System.Windows.Forms;
using HolzShots.Composition;
using Semver;
using System.Diagnostics;

namespace HolzShots.Windows.Forms.Controls
{
    public partial class PluginItem : UserControl
    {
        // We'd love to use WinForm's data binding here
        // But it somehow inconvenient, since we cannot (???) disable a link label if a property is not set.
        // Also, we don't care about two-way-databinding. We're only interested in displaying data of a single item that does not change.
        private readonly IPluginMetadata _model;

        public PluginItem(IPluginMetadata info)
        {
            InitializeComponent();
            SuspendLayout();

            InitializeModel(_model = DesignMode ? new DummyMetadata() : info);

            ResumeLayout();
        }

        private void InitializeModel(IPluginMetadata model)
        {
            pluginVersion.Text = model.Version.ToString();
            pluginName.Text = model.Name;
            pluginAuthor.Text = model.Author;

            authorWebsite.Enabled = model.Website != null;
            reportBug.Enabled = model.BugsUrl != null;
        }


        private void pluginSettings_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // TODO
        }

        private void authorWebSite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => OpenUrlIfPresent(_model?.Website);
        private void reportBug_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => OpenUrlIfPresent(_model?.BugsUrl);

        private static void OpenUrlIfPresent(string /* ? */ url)
        {
            if (url == null)
                return;

            try
            {
                Process.Start(url);
            }
            catch
            {
                Trace.WriteLine("Failed to open url");
            }
        }

        private class DummyMetadata : IPluginMetadata
        {
            public string Name => "Cool plugin";
            public string Author => "Even cooler author";
            public SemVersion Version => new SemVersion(1);
            public string Website => "https://holzshots.net";
            public string BugsUrl => "https://github.com/nikeee/holzshots/issues";
            public string Contact => "https://github.com/nikeee/holzshots/issues";
            public string Description => "A very cool plugin";
        }
    }
}
