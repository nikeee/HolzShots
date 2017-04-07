Imports System.Windows.Forms

Namespace Windows.Forms
    Public Class ExplorerLinkLabel
        Inherits LinkLabel
        Sub New()
            MyBase.New()
            LinkBehavior = LinkBehavior.HoverUnderline
            Cursor = Cursors.Hand
            LinkColor = Drawing.SystemColors.HotTrack
            ActiveLinkColor = Drawing.SystemColors.Highlight
        End Sub
        Protected Overrides Sub OnMouseEnter(e As EventArgs)
            LinkColor = Drawing.SystemColors.Highlight
        End Sub
        Protected Overrides Sub OnMouseLeave(e As EventArgs)
            MyBase.OnMouseLeave(e)
            LinkColor = Drawing.SystemColors.HotTrack
        End Sub
    End Class
End Namespace
