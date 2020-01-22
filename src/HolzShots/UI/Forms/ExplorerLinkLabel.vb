
Namespace UI.Forms
    Public Class ExplorerLinkLabel
        Inherits LinkLabel
        Sub New()
            MyBase.New()
            LinkBehavior = LinkBehavior.HoverUnderline
            Cursor = Cursors.Hand
            LinkColor = SystemColors.HotTrack
            ActiveLinkColor = SystemColors.Highlight
        End Sub
        Protected Overrides Sub OnMouseEnter(e As EventArgs)
            LinkColor = SystemColors.Highlight
        End Sub
        Protected Overrides Sub OnMouseLeave(e As EventArgs)
            MyBase.OnMouseLeave(e)
            LinkColor = SystemColors.HotTrack
        End Sub
    End Class
End Namespace
