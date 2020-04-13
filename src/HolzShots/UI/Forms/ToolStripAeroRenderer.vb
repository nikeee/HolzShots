Imports System.Drawing
Imports System.Windows.Forms
Imports System.Windows.Forms.VisualStyles
Imports HolzShots.Interop

Namespace UI.Forms

    Public Enum ToolBarTheme
        Toolbar
        MediaToolbar
        CommunicationsToolbar
        BrowserTabBar
        HelpBar
    End Enum

    ''' <summary>Renders a toolstrip using the UxTheme API via VisualStyleRenderer and a specific style.</summary>
    ''' <remarks>Perhaps surprisingly, this does not need to be disposable.</remarks>
    Public Class ToolStripAeroRenderer
        Inherits ToolStripSystemRenderer

        Private _renderer As VisualStyleRenderer

        Public Sub New(thm As ToolBarTheme)
            Theme = thm
        End Sub

        ' See http://msdn2.microsoft.com/en-us/library/bb773210.aspx - "Parts and States"
        ' Only menu-related parts/states are needed here, VisualStyleRenderer handles most of the rest.
        Private Enum MenuParts As Integer
            ItemTMSchema = 1
            DropDownTMSchema = 2
            BarItemTMSchema = 3
            BarDropDownTMSchema = 4
            ChevronTMSchema = 5
            SeparatorTMSchema = 6
            BarBackground = 7
            BarItem = 8
            PopupBackground = 9
            PopupBorders = 10
            PopupCheck = 11
            PopupCheckBackground = 12
            PopupGutter = 13
            PopupItem = 14
            PopupSeparator = 15
            PopupSubmenu = 16
            SystemClose = 17
            SystemMaximize = 18
            SystemMinimize = 19
            SystemRestore = 20
        End Enum

        Private Enum MenuBarStates As Integer
            Active = 1
            Inactive = 2
        End Enum

        Private Enum MenuBarItemStates As Integer
            Normal = 1
            Hover = 2
            Pushed = 3
            Disabled = 4
            DisabledHover = 5
            DisabledPushed = 6
        End Enum

        Private Enum MenuPopupItemStates As Integer
            Normal = 1
            Hover = 2
            Disabled = 3
            DisabledHover = 4
        End Enum

        Private Enum MenuPopupCheckStates As Integer
            CheckmarkNormal = 1
            CheckmarkDisabled = 2
            BulletNormal = 3
            BulletDisabled = 4
        End Enum

        Private Enum MenuPopupCheckBackgroundStates As Integer
            Disabled = 1
            Normal = 2
            Bitmap = 3
        End Enum

        Private Enum MenuPopupSubMenuStates As Integer
            Normal = 1
            Disabled = 2
        End Enum

        Private Enum MarginTypes As Integer
            Sizing = 3601
            Content = 3602
            Caption = 3603
        End Enum

        Private Const RebarBackground As Integer = 6

        Private Function GetThemeMargins(dc As IDeviceContext, marginType As MarginType) As Padding
            Dim margins As Native.Margin
            Try
                Dim hDc As IntPtr = dc.GetHdc()
                If 0 = NativeMethods.GetThemeMargins(_renderer.Handle, hDc, _renderer.Part, _renderer.State, marginType, IntPtr.Zero, margins) Then
                    Return New Padding(margins.cxLeftWidth, margins.cyTopHeight, margins.cxRightWidth, margins.cyBottomheight)
                End If
                Return Padding.Empty
            Finally
                dc.ReleaseHdc()
            End Try
        End Function

        Private Shared Function GetItemState(item As ToolStripItem) As Integer
            Dim hot As Boolean = item.Selected

            If item.IsOnDropDown Then
                If item.Enabled Then
                    Return If(hot, CInt(MenuPopupItemStates.Hover), CInt(MenuPopupItemStates.Normal))
                End If
                Return If(hot, CInt(MenuPopupItemStates.DisabledHover), CInt(MenuPopupItemStates.Disabled))
            Else
                If item.Pressed Then
                    Return If(item.Enabled, CInt(MenuBarItemStates.Pushed), CInt(MenuBarItemStates.DisabledPushed))
                End If
                If item.Enabled Then
                    Return If(hot, CInt(MenuBarItemStates.Hover), CInt(MenuBarItemStates.Normal))
                End If
                Return If(hot, CInt(MenuBarItemStates.DisabledHover), CInt(MenuBarItemStates.Disabled))
            End If
        End Function

        Public Property Theme As ToolBarTheme

        Private ReadOnly Property RebarClass() As String
            Get
                Return SubclassPrefix & "Rebar"
            End Get
        End Property

        Private ReadOnly Property ToolbarClass() As String
            Get
                Return SubclassPrefix & "ToolBar"
            End Get
        End Property

        Private ReadOnly Property MenuClass() As String
            Get
                Return SubclassPrefix & "Menu"
            End Get
        End Property

        Private ReadOnly Property SubclassPrefix() As String
            Get
                Select Case Theme
                    Case ToolBarTheme.MediaToolbar
                        Return "Media::"
                    Case ToolBarTheme.CommunicationsToolbar
                        Return "Communications::"
                    Case ToolBarTheme.BrowserTabBar
                        Return "BrowserTabBar::"
                    Case ToolBarTheme.HelpBar
                        Return "Help::"
                    Case Else
                        Return String.Empty
                End Select
            End Get
        End Property

        Private Function Subclass(element As VisualStyleElement) As VisualStyleElement
            Return VisualStyleElement.CreateElement(SubclassPrefix & element.ClassName, element.Part, element.State)
        End Function

        Private Function EnsureRenderer() As Boolean
            If Not IsSupported Then
                Return False
            End If

            If _renderer Is Nothing Then
                _renderer = New VisualStyleRenderer(VisualStyleElement.Button.PushButton.Normal)
            End If

            Return True
        End Function

        ' Gives parented ToolStrips a transparent background.
        Protected Overrides Sub Initialize(toolStrip As ToolStrip)
            If TypeOf toolStrip.Parent Is ToolStripPanel Then
                toolStrip.BackColor = Color.Transparent
            End If

            MyBase.Initialize(toolStrip)
        End Sub

        ' Using just ToolStripManager.Renderer without setting the Renderer individually per ToolStrip means
        ' that the ToolStrip is not passed to the Initialize method. ToolStripPanels, however, are. So we can
        ' simply initialize it here too, and this should guarantee that the ToolStrip is initialized at least
        ' once. Hopefully it isn't any more complicated than this.
        Protected Overrides Sub InitializePanel(toolStripPanel As ToolStripPanel)
            For Each control As Control In toolStripPanel.Controls
                If TypeOf control Is ToolStrip Then
                    Initialize(DirectCast(control, ToolStrip))
                End If
            Next

            MyBase.InitializePanel(toolStripPanel)
        End Sub

        Protected Overrides Sub OnRenderToolStripBorder(e As ToolStripRenderEventArgs)
            If EnsureRenderer() Then
                _renderer.SetParameters(MenuClass, CInt(MenuParts.PopupBorders), 0)
                If e.ToolStrip.IsDropDown Then
                    Dim oldClip As Region = e.Graphics.Clip

                    ' Tool strip borders are rendered *after* the content, for some reason.
                    ' So we have to exclude the inside of the popup otherwise we'll draw over it.
                    Dim insideRect As Rectangle = e.ToolStrip.ClientRectangle
                    insideRect.Inflate(-1, -1)
                    e.Graphics.ExcludeClip(insideRect)

                    _renderer.DrawBackground(e.Graphics, e.ToolStrip.ClientRectangle, e.AffectedBounds)

                    ' Restore the old clip in case the Graphics is used again (does that ever happen?)
                    e.Graphics.Clip = oldClip
                End If
            Else
                MyBase.OnRenderToolStripBorder(e)
            End If
        End Sub

        Private Function GetBackgroundRectangle(item As ToolStripItem) As Rectangle
            If Not item.IsOnDropDown Then
                Return New Rectangle(New Point(), item.Bounds.Size)
            End If

            ' For a drop-down menu item, the background rectangles of the items should be touching vertically.
            ' This ensures that's the case.
            Dim rect As Rectangle = item.Bounds

            ' The background rectangle should be inset two pixels horizontally (on both sides), but we have
            ' to take into account the border.
            rect.X = item.ContentRectangle.X + 1
            rect.Width = item.ContentRectangle.Width - 1

            ' Make sure we're using all of the vertical space, so that the edges touch.
            rect.Y = 0
            Return rect
        End Function

        Protected Overrides Sub OnRenderMenuItemBackground(e As ToolStripItemRenderEventArgs)
            If EnsureRenderer() Then
                Dim partID As Integer = If(e.Item.IsOnDropDown, CInt(MenuParts.PopupItem), CInt(MenuParts.BarItem))
                _renderer.SetParameters(MenuClass, partID, GetItemState(e.Item))

                Dim bgRect As Rectangle = GetBackgroundRectangle(e.Item)
                _renderer.DrawBackground(e.Graphics, bgRect, bgRect)
            Else
                MyBase.OnRenderMenuItemBackground(e)
            End If
        End Sub

        Protected Overrides Sub OnRenderToolStripPanelBackground(e As ToolStripPanelRenderEventArgs)
            If EnsureRenderer() Then
                ' Draw the background using Rebar & RP_BACKGROUND (or, if that is not available, fall back to
                ' Rebar.Band.Normal)
                If VisualStyleRenderer.IsElementDefined(VisualStyleElement.CreateElement(RebarClass, RebarBackground, 0)) Then
                    _renderer.SetParameters(RebarClass, RebarBackground, 0)
                Else
                    _renderer.SetParameters(RebarClass, 0, 0)
                End If

                If _renderer.IsBackgroundPartiallyTransparent() Then
                    _renderer.DrawParentBackground(e.Graphics, e.ToolStripPanel.ClientRectangle, e.ToolStripPanel)
                End If

                _renderer.DrawBackground(e.Graphics, e.ToolStripPanel.ClientRectangle)

                e.Handled = True
            Else
                MyBase.OnRenderToolStripPanelBackground(e)
            End If
        End Sub

        ' Render the background of an actual menu bar, dropdown menu or toolbar.
        Protected Overrides Sub OnRenderToolStripBackground(e As System.Windows.Forms.ToolStripRenderEventArgs)
            If EnsureRenderer() Then
                If e.ToolStrip.IsDropDown Then
                    _renderer.SetParameters(MenuClass, CInt(MenuParts.PopupBackground), 0)
                Else
                    ' It's a MenuStrip or a ToolStrip. If it's contained inside a larger panel, it should have a
                    ' transparent background, showing the panel's background.

                    If TypeOf e.ToolStrip.Parent Is ToolStripPanel Then
                        ' The background should be transparent, because the ToolStripPanel's background will be visible.
                        ' (Of course, we assume the ToolStripPanel is drawn using the same theme, but it's not my fault
                        ' if someone does that.)
                        Return
                    Else
                        ' A lone toolbar/menubar should act like it's inside a toolbox, I guess.
                        ' Maybe I should use the MenuClass in the case of a MenuStrip, although that would break
                        ' the other themes...
                        If VisualStyleRenderer.IsElementDefined(VisualStyleElement.CreateElement(RebarClass, RebarBackground, 0)) Then
                            _renderer.SetParameters(RebarClass, RebarBackground, 0)
                        Else
                            _renderer.SetParameters(RebarClass, 0, 0)
                        End If
                    End If
                End If

                If _renderer.IsBackgroundPartiallyTransparent() Then
                    _renderer.DrawParentBackground(e.Graphics, e.ToolStrip.ClientRectangle, e.ToolStrip)
                End If

                _renderer.DrawBackground(e.Graphics, e.ToolStrip.ClientRectangle, e.AffectedBounds)
            Else
                MyBase.OnRenderToolStripBackground(e)
            End If
        End Sub

        ' The only purpose of this override is to change the arrow colour.
        ' It's OK to just draw over the default arrow since we also pass down arrow drawing to the system renderer.
        Protected Overrides Sub OnRenderSplitButtonBackground(e As ToolStripItemRenderEventArgs)
            If EnsureRenderer() Then
                Dim sb As ToolStripSplitButton = DirectCast(e.Item, ToolStripSplitButton)
                MyBase.OnRenderSplitButtonBackground(e)

                ' It doesn't matter what colour of arrow we tell it to draw. OnRenderArrow will compute it from the item anyway.
                OnRenderArrow(New ToolStripArrowRenderEventArgs(e.Graphics, sb, sb.DropDownButtonBounds, Color.Red, ArrowDirection.Down))
            Else
                MyBase.OnRenderSplitButtonBackground(e)
            End If
        End Sub

        Private Function GetItemTextColor(item As ToolStripItem) As Color
            Dim partId As Integer = If(item.IsOnDropDown, CInt(MenuParts.PopupItem), CInt(MenuParts.BarItem))
            _renderer.SetParameters(MenuClass, partId, GetItemState(item))
            Return _renderer.GetColor(ColorProperty.TextColor)
        End Function

        Protected Overrides Sub OnRenderItemText(e As ToolStripItemTextRenderEventArgs)
            If EnsureRenderer() Then
                e.TextColor = GetItemTextColor(e.Item)
            End If

            MyBase.OnRenderItemText(e)
        End Sub

        Protected Overrides Sub OnRenderImageMargin(e As ToolStripRenderEventArgs)
            If EnsureRenderer() Then
                If e.ToolStrip.IsDropDown Then
                    _renderer.SetParameters(MenuClass, CInt(MenuParts.PopupGutter), 0)
                    ' The AffectedBounds is usually too small, way too small to look right. Instead of using that,
                    ' use the AffectedBounds but with the right width. Then narrow the rectangle to the correct edge
                    ' based on whether or not it's RTL. (It doesn't need to be narrowed to an edge in LTR mode, but let's
                    ' do that anyway.)
                    ' Using the DisplayRectangle gets roughly the right size so that the separator is closer to the text.
                    Dim margins As Padding = GetThemeMargins(e.Graphics, MarginTypes.Sizing)
                    Dim extraWidth As Integer = (e.ToolStrip.Width - e.ToolStrip.DisplayRectangle.Width - margins.Left - margins.Right - 1) - e.AffectedBounds.Width
                    Dim rect As Rectangle = e.AffectedBounds
                    rect.Y += 2
                    rect.Height -= 4
                    Dim sepWidth As Integer = _renderer.GetPartSize(e.Graphics, ThemeSizeType.[True]).Width
                    If e.ToolStrip.RightToLeft = RightToLeft.Yes Then
                        rect = New Rectangle(rect.X - extraWidth, rect.Y, sepWidth, rect.Height)
                        rect.X += sepWidth
                    Else
                        rect = New Rectangle(rect.Width + extraWidth - sepWidth, rect.Y, sepWidth, rect.Height)
                    End If
                    _renderer.DrawBackground(e.Graphics, rect)
                End If
            Else
                MyBase.OnRenderImageMargin(e)
            End If
        End Sub

        Protected Overrides Sub OnRenderSeparator(e As ToolStripSeparatorRenderEventArgs)
            If e.ToolStrip.IsDropDown AndAlso EnsureRenderer() Then
                _renderer.SetParameters(MenuClass, CInt(MenuParts.PopupSeparator), 0)
                Dim rect As New Rectangle(e.ToolStrip.DisplayRectangle.Left, 0, e.ToolStrip.DisplayRectangle.Width, e.Item.Height)
                _renderer.DrawBackground(e.Graphics, rect, rect)
            Else
                MyBase.OnRenderSeparator(e)
            End If
        End Sub

        Protected Overrides Sub OnRenderItemCheck(e As ToolStripItemImageRenderEventArgs)
            If EnsureRenderer() Then
                Dim bgRect As Rectangle = GetBackgroundRectangle(e.Item)
                bgRect.Width = bgRect.Height

                ' Now, mirror its position if the menu item is RTL.
                If e.Item.RightToLeft = RightToLeft.Yes Then
                    bgRect = New Rectangle(e.ToolStrip.ClientSize.Width - bgRect.X - bgRect.Width, bgRect.Y, bgRect.Width, bgRect.Height)
                End If

                _renderer.SetParameters(MenuClass, CInt(MenuParts.PopupCheckBackground), If(e.Item.Enabled, CInt(MenuPopupCheckBackgroundStates.Normal), CInt(MenuPopupCheckBackgroundStates.Disabled)))
                _renderer.DrawBackground(e.Graphics, bgRect)

                Dim checkRect As Rectangle = e.ImageRectangle
                checkRect.X = bgRect.X + bgRect.Width \ 2 - checkRect.Width \ 2
                checkRect.Y = bgRect.Y + bgRect.Height \ 2 - checkRect.Height \ 2

                ' I don't think ToolStrip even supports radio box items, so no need to render them.
                _renderer.SetParameters(MenuClass, CInt(MenuParts.PopupCheck), If(e.Item.Enabled, CInt(MenuPopupCheckStates.CheckmarkNormal), CInt(MenuPopupCheckStates.CheckmarkDisabled)))

                _renderer.DrawBackground(e.Graphics, checkRect)
            Else
                MyBase.OnRenderItemCheck(e)
            End If
        End Sub

        Protected Overrides Sub OnRenderArrow(e As ToolStripArrowRenderEventArgs)
            ' The default renderer will draw an arrow for us (the UXTheme API seems not to have one for all directions),
            ' but it will get the colour wrong in many cases. The text colour is probably the best colour to use.
            If EnsureRenderer() Then
                e.ArrowColor = GetItemTextColor(e.Item)
            End If
            MyBase.OnRenderArrow(e)
        End Sub

        Protected Overrides Sub OnRenderOverflowButtonBackground(e As ToolStripItemRenderEventArgs)
            If EnsureRenderer() Then
                ' BrowserTabBar::Rebar draws the chevron using the default background. Odd.
                Dim rebarClass1 As String = RebarClass
                If Theme = ToolBarTheme.BrowserTabBar Then
                    rebarClass1 = "Rebar"
                End If

                Dim state As Integer = VisualStyleElement.Rebar.Chevron.Normal.State
                If e.Item.Pressed Then
                    state = VisualStyleElement.Rebar.Chevron.Pressed.State
                ElseIf e.Item.Selected Then
                    state = VisualStyleElement.Rebar.Chevron.Hot.State
                End If

                _renderer.SetParameters(rebarClass1, VisualStyleElement.Rebar.Chevron.Normal.Part, state)
                _renderer.DrawBackground(e.Graphics, New Rectangle(Point.Empty, e.Item.Size))
            Else
                MyBase.OnRenderOverflowButtonBackground(e)
            End If
        End Sub

        Public ReadOnly Property IsSupported() As Boolean
            Get
                If Not VisualStyleRenderer.IsSupported Then
                    Return False
                End If

                ' Needs a more robust check. It seems mono supports very different style sets.
                Return VisualStyleRenderer.IsElementDefined(VisualStyleElement.CreateElement("Menu", CInt(MenuParts.BarBackground), CInt(MenuBarStates.Active)))
            End Get
        End Property
    End Class
End Namespace
