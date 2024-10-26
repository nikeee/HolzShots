Imports System.Drawing.Drawing2D
Imports HolzShots.Drawing.Tools.UI
Imports HolzShots.UI.Controls

Namespace Drawing.Tools
    Friend Class Eraser
        Inherits Tool

        Private ReadOnly _parent As PaintPanel

        Private ReadOnly _settingsControl As SettingsControl(Of EraserSettings)

        Public Overrides Property BeginCoordinates As Point
            Get
                Return InternalBeginCoordinates
            End Get
            Set(value As Point)
                InternalBeginCoordinates = value
                RenderPreview(_parent.RawBox.Image, Nothing, _parent)
            End Set
        End Property

        Public Overrides ReadOnly Property Cursor As Cursor
            Get
                Dim bmp As New Bitmap(_parent.EraserDiameter + 8, _parent.EraserDiameter + 8)
                bmp.MakeTransparent()
                Using g As Graphics = Graphics.FromImage(bmp)
                    g.SmoothingMode = SmoothingMode.AntiAlias
                    g.FillEllipse(Brushes.LightGray, 4, 4, _parent.EraserDiameter, _parent.EraserDiameter)
                    g.DrawEllipse(Pens.Black, 4, 4, _parent.EraserDiameter, _parent.EraserDiameter)
                End Using
                Return New Cursor(bmp.GetHicon)
            End Get
        End Property

        Public Overrides ReadOnly Property ToolType As PaintPanel.ShotEditorTool = PaintPanel.ShotEditorTool.Eraser

        Private Shared ReadOnly ClearBrush As Brush = New SolidBrush(Color.FromArgb(0, Color.White))
        Private _isFirstClick As Boolean = True

        Public Overrides Sub RenderPreview(rawImage As Image, ga As Graphics, sender As PaintPanel)
            Using g As Graphics = Graphics.FromImage(rawImage)
                g.CompositingMode = CompositingMode.SourceCopy
                g.SmoothingMode = SmoothingMode.AntiAlias
                If _isFirstClick Then
                    g.FillEllipse(ClearBrush, InternalBeginCoordinates.X - CInt(_parent.EraserDiameter / 2), InternalBeginCoordinates.Y - CInt(_parent.EraserDiameter / 2), _parent.EraserDiameter, _parent.EraserDiameter)
                    _isFirstClick = False
                Else
                    g.FillEllipse(ClearBrush, EndCoordinates.X - CInt(_parent.EraserDiameter / 2), EndCoordinates.Y - CInt(_parent.EraserDiameter / 2), _parent.EraserDiameter, _parent.EraserDiameter)
                End If
            End Using
        End Sub

        Public Sub New(parent As PaintPanel)
            ArgumentNullException.ThrowIfNull(parent)
            _parent = parent
            _settingsControl = New EraserSettingsControl(New EraserSettings(10))
            AddHandler _settingsControl.OnSettingsUpdated, AddressOf OnSettingsUpdated
        End Sub

        Private Sub OnSettingsUpdated(sender As Object, newSettings As EraserSettings)
            MessageBox.Show("Settings updated: " & newSettings.ToString())
        End Sub
    End Class
End Namespace
