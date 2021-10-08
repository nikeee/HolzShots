using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace HolzShots.Windows.Forms
{
    public enum ToolBarTheme
    {
        Toolbar,
        MediaToolbar,
        CommunicationsToolbar,
        BrowserTabBar,
        HelpBar
    }

    /// <summary>Renders a toolstrip using the UxTheme API via VisualStyleRenderer and a specific style.</summary>
    /// <remarks>Perhaps surprisingly, this does not need to be disposable.</remarks>
    public class VisualStyleStripRenderer : ToolStripSystemRenderer
    {
        private VisualStyleRenderer _renderer = null!;

        public VisualStyleStripRenderer(ToolBarTheme theme) => Theme = theme;

        // See http://msdn2.microsoft.com/en-us/library/bb773210.aspx - "Parts and States"
        // Only menu-related parts/states are needed here, VisualStyleRenderer handles most of the rest.
        private enum MenuPart : int
        {
            ItemTMSchema = 1,
            DropDownTMSchema = 2,
            BarItemTMSchema = 3,
            BarDropDownTMSchema = 4,
            ChevronTMSchema = 5,
            SeparatorTMSchema = 6,
            BarBackground = 7,
            BarItem = 8,
            PopupBackground = 9,
            PopupBorders = 10,
            PopupCheck = 11,
            PopupCheckBackground = 12,
            PopupGutter = 13,
            PopupItem = 14,
            PopupSeparator = 15,
            PopupSubmenu = 16,
            SystemClose = 17,
            SystemMaximize = 18,
            SystemMinimize = 19,
            SystemRestore = 20
        }

        private enum MenuBarState : int
        {
            Active = 1,
            Inactive = 2
        }

        private enum MenuBarItemState : int
        {
            Normal = 1,
            Hover = 2,
            Pushed = 3,
            Disabled = 4,
            DisabledHover = 5,
            DisabledPushed = 6
        }

        private enum MenuPopupItemState : int
        {
            Normal = 1,
            Hover = 2,
            Disabled = 3,
            DisabledHover = 4
        }

        private enum MenuPopupCheckState : int
        {
            CheckmarkNormal = 1,
            CheckmarkDisabled = 2,
            BulletNormal = 3,
            BulletDisabled = 4
        }

        private enum MenuPopupCheckBackgroundState : int
        {
            Disabled = 1,
            Normal = 2,
            Bitmap = 3
        }

        private enum MenuPopupSubMenuState : int
        {
            Normal = 1,
            Disabled = 2
        }

        private enum MarginType : int
        {
            Sizing = 3601,
            Content = 3602,
            Caption = 3603
        }

        private const int RebarBackground = 6;

        private Padding GetThemeMargins(IDeviceContext dc, MarginType marginType)
        {
            var margins = new Native.Margin();
            try
            {
                var hDc = dc.GetHdc();
                if (0 == Native.UxTheme.GetThemeMargins(_renderer.Handle, hDc, _renderer.Part, _renderer.State, (int)marginType, IntPtr.Zero, ref margins))
                    return new Padding(margins.cxLeftWidth, margins.cyTopHeight, margins.cxRightWidth, margins.cyBottomheight);
                return Padding.Empty;
            }
            finally
            {
                dc.ReleaseHdc();
            }
        }

        private static int GetItemState(ToolStripItem item)
        {
            bool hot = item.Selected;

            if (item.IsOnDropDown)
            {
                if (item.Enabled)
                    return hot ? (int)MenuPopupItemState.Hover : (int)MenuPopupItemState.Normal;
                return hot ? (int)MenuPopupItemState.DisabledHover : (int)MenuPopupItemState.Disabled;
            }
            else
            {
                if (item.Pressed)
                    return item.Enabled ? (int)MenuBarItemState.Pushed : (int)MenuBarItemState.DisabledPushed;
                if (item.Enabled)
                    return hot ? (int)MenuBarItemState.Hover : (int)MenuBarItemState.Normal;
                return hot ? (int)MenuBarItemState.DisabledHover : (int)MenuBarItemState.Disabled;
            }
        }

        public ToolBarTheme Theme { get; set; }

        private string RebarClass => SubclassPrefix + "Rebar";
        private string ToolbarClass => SubclassPrefix + "ToolBar";
        private string MenuClass => SubclassPrefix + "Menu";
        private string SubclassPrefix => Theme switch {
            ToolBarTheme.MediaToolbar => "Media::",
            ToolBarTheme.CommunicationsToolbar => "Communications::",
            ToolBarTheme.BrowserTabBar => "BrowserTabBar::",
            ToolBarTheme.HelpBar => "Help::",
            _ => string.Empty,
        };

        private VisualStyleElement Subclass(VisualStyleElement element)
        {
            return VisualStyleElement.CreateElement(SubclassPrefix + element.ClassName, element.Part, element.State);
        }

        private bool EnsureRenderer()
        {
            if (!IsSupported)
                return false;

            if (_renderer == null)
                _renderer = new VisualStyleRenderer(VisualStyleElement.Button.PushButton.Normal);

            return true;
        }

        // Gives parented ToolStrips a transparent background.
        protected override void Initialize(ToolStrip toolStrip)
        {
            if (toolStrip == null)
                throw new ArgumentNullException(nameof(toolStrip));

            if (toolStrip.Parent is ToolStripPanel)
                toolStrip.BackColor = Color.Transparent;

            base.Initialize(toolStrip);
        }

        // Using just ToolStripManager.Renderer without setting the Renderer individually per ToolStrip means
        // that the ToolStrip is not passed to the Initialize method. ToolStripPanels, however, are. So we can
        // simply initialize it here too, and this should guarantee that the ToolStrip is initialized at least
        // once. Hopefully it isn't any more complicated than this.
        protected override void InitializePanel(ToolStripPanel toolStripPanel)
        {
            if (toolStripPanel == null)
                throw new ArgumentNullException(nameof(toolStripPanel));

            foreach (var control in toolStripPanel.Controls)
            {
                if (control is ToolStrip)
                    Initialize((ToolStrip)control);
            }

            base.InitializePanel(toolStripPanel);
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            if (e == null)
                throw new ArgumentNullException(nameof(e));

            if (EnsureRenderer())
            {
                _renderer.SetParameters(MenuClass, (int)MenuPart.PopupBorders, 0);
                if (e.ToolStrip.IsDropDown)
                {
                    Region oldClip = e.Graphics.Clip;

                    // Tool strip borders are rendered *after* the content, for some reason.
                    // So we have to exclude the inside of the popup otherwise we'll draw over it.
                    Rectangle insideRect = e.ToolStrip.ClientRectangle;
                    insideRect.Inflate(-1, -1);
                    e.Graphics.ExcludeClip(insideRect);

                    _renderer.DrawBackground(e.Graphics, e.ToolStrip.ClientRectangle, e.AffectedBounds);

                    // Restore the old clip in case the Graphics is used again (does that ever happen?)
                    e.Graphics.Clip = oldClip;
                }
            }
            else
                base.OnRenderToolStripBorder(e);
        }

        private Rectangle GetBackgroundRectangle(ToolStripItem item)
        {
            if (!item.IsOnDropDown)
                return new Rectangle(new Point(), item.Bounds.Size);

            // For a drop-down menu item, the background rectangles of the items should be touching vertically.
            // This ensures that's the case.
            Rectangle rect = item.Bounds;

            // The background rectangle should be inset two pixels horizontally (on both sides), but we have
            // to take into account the border.
            rect.X = item.ContentRectangle.X + 1;
            rect.Width = item.ContentRectangle.Width - 1;

            // Make sure we're using all of the vertical space, so that the edges touch.
            rect.Y = 0;
            return rect;
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (e == null)
                throw new ArgumentNullException(nameof(e));

            if (EnsureRenderer())
            {
                int partID = e.Item.IsOnDropDown ? (int)MenuPart.PopupItem : (int)MenuPart.BarItem;
                _renderer.SetParameters(MenuClass, partID, GetItemState(e.Item));

                Rectangle bgRect = GetBackgroundRectangle(e.Item);
                _renderer.DrawBackground(e.Graphics, bgRect, bgRect);
            }
            else
                base.OnRenderMenuItemBackground(e);
        }

        protected override void OnRenderToolStripPanelBackground(ToolStripPanelRenderEventArgs e)
        {
            if (e == null)
                throw new ArgumentNullException(nameof(e));

            if (EnsureRenderer())
            {
                // Draw the background using Rebar & RP_BACKGROUND (or, if that is not available, fall back to Rebar.Band.Normal)
                if (VisualStyleRenderer.IsElementDefined(VisualStyleElement.CreateElement(RebarClass, RebarBackground, 0)))
                    _renderer.SetParameters(RebarClass, RebarBackground, 0);
                else
                    _renderer.SetParameters(RebarClass, 0, 0);

                if (_renderer.IsBackgroundPartiallyTransparent())
                    _renderer.DrawParentBackground(e.Graphics, e.ToolStripPanel.ClientRectangle, e.ToolStripPanel);

                _renderer.DrawBackground(e.Graphics, e.ToolStripPanel.ClientRectangle);

                e.Handled = true;
            }
            else
                base.OnRenderToolStripPanelBackground(e);
        }

        // Render the background of an actual menu bar, dropdown menu or toolbar.
        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            if (e == null)
                throw new ArgumentNullException(nameof(e));

            if (EnsureRenderer())
            {
                if (e.ToolStrip.IsDropDown)
                    _renderer.SetParameters(MenuClass, (int)MenuPart.PopupBackground, 0);
                else
                {
                    // It's a MenuStrip or a ToolStrip. If it's contained inside a larger panel, it should have a
                    // transparent background, showing the panel's background.
                    if (e.ToolStrip.Parent is ToolStripPanel)
                    {

                        // The background should be transparent, because the ToolStripPanel's background will be visible.
                        // (Of course, we assume the ToolStripPanel is drawn using the same theme, but it's not my fault
                        // if someone does that.)
                        return;
                    }
                    else
                    {
                        // A lone toolbar/menubar should act like it's inside a toolbox, I guess.
                        // Maybe I should use the MenuClass in the case of a MenuStrip, although that would break
                        // the other themes...
                        if (VisualStyleRenderer.IsElementDefined(VisualStyleElement.CreateElement(RebarClass, RebarBackground, 0)))
                            _renderer.SetParameters(RebarClass, RebarBackground, 0);
                        else
                            _renderer.SetParameters(RebarClass, 0, 0);
                    }
                }

                if (_renderer.IsBackgroundPartiallyTransparent())
                    _renderer.DrawParentBackground(e.Graphics, e.ToolStrip.ClientRectangle, e.ToolStrip);

                _renderer.DrawBackground(e.Graphics, e.ToolStrip.ClientRectangle, e.AffectedBounds);
            }
            else
                base.OnRenderToolStripBackground(e);
        }

        // The only purpose of this override is to change the arrow colour.
        // It's OK to just draw over the default arrow since we also pass down arrow drawing to the system renderer.
        protected override void OnRenderSplitButtonBackground(ToolStripItemRenderEventArgs e)
        {
            if (e == null)
                throw new ArgumentNullException(nameof(e));

            if (EnsureRenderer())
            {
                ToolStripSplitButton sb = (ToolStripSplitButton)e.Item;
                base.OnRenderSplitButtonBackground(e);

                // It doesn't matter what colour of arrow we tell it to draw. OnRenderArrow will compute it from the item anyway.
                OnRenderArrow(new ToolStripArrowRenderEventArgs(e.Graphics, sb, sb.DropDownButtonBounds, Color.Red, ArrowDirection.Down));
            }
            else
                base.OnRenderSplitButtonBackground(e);
        }

        private Color GetItemTextColor(ToolStripItem item)
        {
            int partId = item.IsOnDropDown ? (int)MenuPart.PopupItem : (int)MenuPart.BarItem;
            _renderer.SetParameters(MenuClass, partId, GetItemState(item));
            return _renderer.GetColor(ColorProperty.TextColor);
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            if (e == null)
                throw new ArgumentNullException(nameof(e));

            if (EnsureRenderer())
                e.TextColor = GetItemTextColor(e.Item);

            base.OnRenderItemText(e);
        }

        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            if (e == null)
                throw new ArgumentNullException(nameof(e));

            if (EnsureRenderer())
            {
                if (e.ToolStrip.IsDropDown)
                {
                    _renderer.SetParameters(MenuClass, (int)MenuPart.PopupGutter, 0);
                    // The AffectedBounds is usually too small, way too small to look right. Instead of using that,
                    // use the AffectedBounds but with the right width. Then narrow the rectangle to the correct edge
                    // based on whether or not it's RTL. (It doesn't need to be narrowed to an edge in LTR mode, but let's
                    // do that anyway.)
                    // Using the DisplayRectangle gets roughly the right size so that the separator is closer to the text.
                    Padding margins = GetThemeMargins(e.Graphics, MarginType.Sizing);
                    int extraWidth = (e.ToolStrip.Width - e.ToolStrip.DisplayRectangle.Width - margins.Left - margins.Right - 1) - e.AffectedBounds.Width;
                    Rectangle rect = e.AffectedBounds;
                    rect.Y += 2;
                    rect.Height -= 4;
                    int sepWidth = _renderer.GetPartSize(e.Graphics, ThemeSizeType.True).Width;
                    if (e.ToolStrip.RightToLeft == RightToLeft.Yes)
                    {
                        rect = new Rectangle(rect.X - extraWidth, rect.Y, sepWidth, rect.Height);
                        rect.X += sepWidth;
                    }
                    else
                        rect = new Rectangle(rect.Width + extraWidth - sepWidth, rect.Y, sepWidth, rect.Height);
                    _renderer.DrawBackground(e.Graphics, rect);
                }
            }
            else
                base.OnRenderImageMargin(e);
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            if (e == null)
                throw new ArgumentNullException(nameof(e));

            if (e.ToolStrip.IsDropDown && EnsureRenderer())
            {
                _renderer.SetParameters(MenuClass, (int)MenuPart.PopupSeparator, 0);
                Rectangle rect = new Rectangle(e.ToolStrip.DisplayRectangle.Left, 0, e.ToolStrip.DisplayRectangle.Width, e.Item.Height);
                _renderer.DrawBackground(e.Graphics, rect, rect);
            }
            else
                base.OnRenderSeparator(e);
        }

        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            if (e == null)
                throw new ArgumentNullException(nameof(e));

            if (EnsureRenderer())
            {
                Rectangle bgRect = GetBackgroundRectangle(e.Item);
                bgRect.Width = bgRect.Height;

                // Now, mirror its position if the menu item is RTL.
                if (e.Item.RightToLeft == RightToLeft.Yes)
                    bgRect = new Rectangle(e.ToolStrip.ClientSize.Width - bgRect.X - bgRect.Width, bgRect.Y, bgRect.Width, bgRect.Height);

                _renderer.SetParameters(MenuClass, (int)MenuPart.PopupCheckBackground, e.Item.Enabled ? (int)MenuPopupCheckBackgroundState.Normal : (int)MenuPopupCheckBackgroundState.Disabled);
                _renderer.DrawBackground(e.Graphics, bgRect);

                Rectangle checkRect = e.ImageRectangle;
                checkRect.X = bgRect.X + bgRect.Width / 2 - checkRect.Width / 2;
                checkRect.Y = bgRect.Y + bgRect.Height / 2 - checkRect.Height / 2;

                // I don't think ToolStrip even supports radio box items, so no need to render them.
                _renderer.SetParameters(MenuClass, (int)MenuPart.PopupCheck, e.Item.Enabled ? (int)MenuPopupCheckState.CheckmarkNormal : (int)MenuPopupCheckState.CheckmarkDisabled);

                _renderer.DrawBackground(e.Graphics, checkRect);
            }
            else
                base.OnRenderItemCheck(e);
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            if (e == null)
                throw new ArgumentNullException(nameof(e));

            // The default renderer will draw an arrow for us (the UXTheme API seems not to have one for all directions),
            // but it will get the colour wrong in many cases. The text colour is probably the best colour to use.
            if (EnsureRenderer())
                e.ArrowColor = GetItemTextColor(e.Item);
            base.OnRenderArrow(e);
        }

        protected override void OnRenderOverflowButtonBackground(ToolStripItemRenderEventArgs e)
        {
            if (e == null)
                throw new ArgumentNullException(nameof(e));

            if (EnsureRenderer())
            {
                // BrowserTabBar::Rebar draws the chevron using the default background. Odd.
                string rebarClass1 = RebarClass;
                if (Theme == ToolBarTheme.BrowserTabBar)
                    rebarClass1 = "Rebar";

                int state = VisualStyleElement.Rebar.Chevron.Normal.State;
                if (e.Item.Pressed)
                    state = VisualStyleElement.Rebar.Chevron.Pressed.State;
                else if (e.Item.Selected)
                    state = VisualStyleElement.Rebar.Chevron.Hot.State;

                _renderer.SetParameters(rebarClass1, VisualStyleElement.Rebar.Chevron.Normal.Part, state);
                _renderer.DrawBackground(e.Graphics, new Rectangle(Point.Empty, e.Item.Size));
            }
            else
                base.OnRenderOverflowButtonBackground(e);
        }

        public bool IsSupported
        {
            get
            {
                if (!VisualStyleRenderer.IsSupported)
                    return false;

                // Needs a more robust check. It seems mono supports very different style sets.
                return VisualStyleRenderer.IsElementDefined(VisualStyleElement.CreateElement("Menu", (int)MenuPart.BarBackground, (int)MenuBarState.Active));
            }
        }
    }
}
