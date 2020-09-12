using System.ComponentModel;
using System.Windows.Forms;
using HolzShots.Composition;
using Semver;
using System.Diagnostics;

namespace HolzShots.Windows.Forms.Controls
{
    public partial class PluginItem : UserControl
    {
        private IPluginMetadata _model = new DummyMetadata();

        public PluginItem() => InitializeComponent();

        [Bindable(true)]
        public IPluginMetadata DataSource
        {
            get => _model;
            set
            {
                _model = value ?? new DummyMetadata();
                modelBindingSource.DataSource = _model;
            }
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
