Imports HolzShots.Drawing.Tools
Imports HolzShots.Drawing.Tools.UI

Namespace UI.Controls
    Friend Class PanelActivator

        Private Shared ReadOnly OpenPoint As New Point(475, 0) '(525, 0)

        Private ReadOnly _toolPanelDict As New Dictionary(Of PaintPanel.ShotEditorTool, Panel)

        Public Sub AddPanel(tool As PaintPanel.ShotEditorTool, targetPanel As Panel)
            ArgumentNullException.ThrowIfNull(targetPanel)
            If _toolPanelDict.ContainsKey(tool) Then Throw New ArgumentException("Settings panel already in dictionary")

            _toolPanelDict.Add(tool, targetPanel)
        End Sub

        Public Sub ActivateSettingsPanel(tool As PaintPanel.ShotEditorTool)
            Dim value As Panel = Nothing
            If Not _toolPanelDict.TryGetValue(tool, value) Then
                Return
            End If

            Dim targetPanel = value
            targetPanel.Location = OpenPoint
            targetPanel.Visible = True
            targetPanel.BringToFront()

            _toolPanelDict.Where(Function(ent) ent.Key <> tool).ToList().ForEach(Function(ent) ent.Value.Visible = False)
        End Sub

        Public Sub CreateSettingsPanel(tool As ITool(Of ToolSettingsBase))
            Debug.Assert(tool.SettingsControl IsNot Nothing)

            Dim panel = _toolPanelDict(PaintPanel.ShotEditorTool.LegacyNew)
            Debug.Assert(panel IsNot Nothing)

            ActivateSettingsPanel(PaintPanel.ShotEditorTool.LegacyNew)

            Dim control = tool.SettingsControl
            Debug.Assert(control IsNot Nothing)
        End Sub

        Public Sub HideAll()
            For Each v In _toolPanelDict.Values
                v.Visible = False
            Next
        End Sub

    End Class
End Namespace
