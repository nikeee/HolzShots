Namespace UI.Controls
    Friend Class PanelActivator

        Private Shared ReadOnly OpenPoint As New Point(475, 0) '(525, 0)

        Private ReadOnly _toolPanelDict As New Dictionary(Of PaintPanel.ShotEditorTool, Panel)

        Public Sub AddPanel(tool As PaintPanel.ShotEditorTool, targetPanel As Panel)
            If targetPanel Is Nothing Then Throw New ArgumentNullException(NameOf(targetPanel))
            If _toolPanelDict.ContainsKey(tool) Then Throw New ArgumentException("Settings panel already in dictionary")

            _toolPanelDict.Add(tool, targetPanel)
        End Sub

        Public Sub ActivateSettingsPanel(tool As PaintPanel.ShotEditorTool)
            If Not _toolPanelDict.ContainsKey(tool) Then Throw New ArgumentException("No matching settings panel")

            Dim targetPanel = _toolPanelDict(tool)
            targetPanel.Location = OpenPoint
            targetPanel.Visible = True
            targetPanel.BringToFront()

            _toolPanelDict.Where(Function(ent) ent.Key <> tool).ToList().ForEach(Function(ent) ent.Value.Visible = False)
        End Sub

        Public Sub HideAll()
            _toolPanelDict.ToList().ForEach(Function(ent) ent.Value.Visible = False)
        End Sub

    End Class
End Namespace
