Imports System.ComponentModel
Imports HolzShots.UI.Windows.Forms

Namespace UI.Controls
    Friend Class CopyLinkLabel
        Inherits ExplorerLinkLabel

        Private Shared ReadOnly TheFont As Font = New Font("Consolas", 9.75, FontStyle.Regular, GraphicsUnit.Point) 'Consolas; 9,75pt

        <[ReadOnly](True)>
        Public Overrides Property Font As Font
            Get
                Return TheFont
            End Get
            Set(value As Font)
            End Set
        End Property

        Sub New()
            MyBase.New()
            LinkBehavior = LinkBehavior.HoverUnderline
        End Sub
    End Class
End Namespace
