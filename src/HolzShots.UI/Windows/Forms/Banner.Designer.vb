Namespace Windows.Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class Banner
        Inherits System.Windows.Forms.UserControl

        'UserControl überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
        <System.Diagnostics.DebuggerNonUserCode()> _
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Wird vom Windows Form-Designer benötigt.
        Private components As System.ComponentModel.IContainer

        'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
        'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
        'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.textLabel = New System.Windows.Forms.Label()
            Me.SuspendLayout()
            '
            'textLabel
            '
            Me.textLabel.AutoSize = True
            Me.textLabel.BackColor = System.Drawing.Color.Transparent
            Me.textLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.textLabel.Location = New System.Drawing.Point(26, 5)
            Me.textLabel.Name = "textLabel"
            Me.textLabel.Size = New System.Drawing.Size(44, 15)
            Me.textLabel.TabIndex = 0
            Me.textLabel.Text = "Banner"
            '
            'Banner
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Info
            Me.Controls.Add(Me.textLabel)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.Name = "Banner"
            Me.Size = New System.Drawing.Size(292, 24)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents textLabel As System.Windows.Forms.Label

    End Class
End Namespace