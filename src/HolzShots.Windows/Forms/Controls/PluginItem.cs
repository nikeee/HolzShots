using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using HolzShots.Composition;
using HolzShots.IO;
using Semver;

namespace HolzShots.Windows.Forms.Controls
{
    public partial class PluginItem : UserControl
    {
        // We'd love to use WinForm's data binding here
        // But it somehow inconvenient, since we cannot (???) disable a link label if a property is not set.
        // Also, we don't care about two-way-databinding. We're only interested in displaying data of a single item that does not change.
        private readonly IPluginMetadata _model;

        #region Init and Model

        public PluginItem(IPluginMetadata info)
        {
            InitializeComponent();
            Debug.Assert(info != null);

            SuspendLayout();

            _model = DesignMode ? new DummyMetadata() : info;
            InitializeModel();

            ResumeLayout(true);
        }

        private void InitializeModel()
        {
            pluginVersion.Text = _model.Version.ToString();
            pluginName.Text = _model.Name;
            pluginAuthor.Text = _model.Author;

            authorWebsite.Enabled = _model.Website != null;
            reportBug.Enabled = _model.BugsUrl != null;
        }

        #endregion

        #region Painting and UI State

        private const int BorderPadding = 0;
        private const int HotMargin = BorderPadding;
        private static readonly Pen _separatorPen = new Pen(Color.FromArgb(0xff, 0xcc, 0xcc, 0xcc));

        protected override void OnPaint(PaintEventArgs e)
        {
            Debug.Assert(e != null);
            // e.Graphics.DrawLine(_separatorPen, new Point(0 + BorderPadding, Height - 1), new Point(Width - 1 - BorderPadding, Height - 1));
            e.Graphics.DrawLine(_separatorPen, new Point(0, Height - 1), new Point(Width - 1, Height - 1));
        }

        /*

        private bool _isMouseHovering;
        private static readonly VisualStyleRenderer _hotRenderer = new VisualStyleRenderer(VisualStyleElement.CreateElement("LISTVIEW", 6, 6));

        protected override void OnPaint(PaintEventArgs e)
        {
            Debug.Assert(e != null);

            if (_isMouseHovering)
            {
                var highlightRegion = new Rectangle(ClientRectangle.X + HotMargin, ClientRectangle.Y, ClientRectangle.Width - HotMargin * 2, ClientRectangle.Height - 1);
                _hotRenderer.DrawBackground(e.Graphics, highlightRegion);
            }

            e.Graphics.DrawLine(_separatorPen, new Point(0 + BorderPadding, Height - 1), new Point(Width - 1 - BorderPadding, Height - 1));
        }

        protected override void OnMouseEnter(EventArgs e) => UpdateHoverState();
        protected override void OnMouseLeave(EventArgs e) => UpdateHoverState();

        private void UpdateHoverState()
        {
            _isMouseHovering = ClientRectangle.Contains(PointToClient(MousePosition));
            Invalidate(false);
        }
        */

        #endregion

        private void pluginSettings_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // TODO: Fix Plugin Settings (we don't seem to support them now)
            // In VB, it was:

            // If _pluginMetadata.SettingsMode <> SettingsModes.NoSettings Then
            //     Try
            //         Dim dialog = _pluginMetadata.SettingsDialog
            //         If dialog IsNot Nothing Then
            //             dialog.ShowDialog(Me)
            //         End If
            //     Catch ex As Exception
            //         HumanInterop.ErrorWhileOpeningSettingsDialog(ex)
            //     End Try
            // End If
        }

        private void authorWebSite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => OpenUrlIfPresent(_model?.Website);
        private void reportBug_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => OpenUrlIfPresent(_model?.BugsUrl);

        private static void OpenUrlIfPresent(string /* ? */ url)
        {
            if (url != null)
                HolzShotsPaths.OpenLink(url);
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
