Imports HolzShots.Drawing.Tools

Namespace UI.Controls
    Friend Class PanelActivator

        Private ReadOnly _panel As Control
        Sub New(panel As Control)
            ArgumentNullException.ThrowIfNull(panel)
            _panel = panel
        End Sub

        Public Sub CreateSettingsPanel(tool As ITool(Of ToolSettingsBase))
            Debug.Assert(tool.SettingsControl IsNot Nothing)
            Debug.Assert(_panel IsNot Nothing)

            Dim controlRaw = tool.SettingsControl
            Debug.Assert(controlRaw IsNot Nothing)
            Debug.Assert(TypeOf controlRaw Is UserControl)

            Dim control = DirectCast(controlRaw, Control)
            control.Dock = DockStyle.Fill

            _panel.Controls.Add(control)

            _panel.Visible = True
            _panel.BringToFront()
        End Sub

        Public Sub ClearSettingsPanel()

            Dim toRemove = New List(Of Control)
            For Each c In _panel.Controls
                Dim control = DirectCast(c, Control)
                If control IsNot Nothing Then
                    toRemove.Add(control)
                End If
            Next

            _panel.Controls.Clear()

            For Each c In toRemove
                c.Dispose()
            Next
        End Sub
    End Class
End Namespace
