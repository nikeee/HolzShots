Namespace UI
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class HotkeySelector
        Inherits HolzShots.UI.BorderPanelWindow

        'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HotkeySelector))
            Me.acceptHotkeyButton = New System.Windows.Forms.Button()
            Me.hotkeyBox = New System.Windows.Forms.TextBox()
            Me.cancelHotkeyButton = New System.Windows.Forms.Button()
            Me.SuspendLayout()
            '
            'acceptHotkeyButton
            '
            Me.acceptHotkeyButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.acceptHotkeyButton.Location = New System.Drawing.Point(291, 84)
            Me.acceptHotkeyButton.Name = "acceptHotkeyButton"
            Me.acceptHotkeyButton.Size = New System.Drawing.Size(90, 21)
            Me.acceptHotkeyButton.TabIndex = 0
            Me.acceptHotkeyButton.Text = "Übernehmen"
            Me.acceptHotkeyButton.UseVisualStyleBackColor = True
            '
            'hotkeyBox
            '
            Me.hotkeyBox.BackColor = System.Drawing.Color.White
            Me.hotkeyBox.Font = New System.Drawing.Font("Courier New", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.hotkeyBox.Location = New System.Drawing.Point(12, 21)
            Me.hotkeyBox.Name = "hotkeyBox"
            Me.hotkeyBox.ReadOnly = True
            Me.hotkeyBox.ShortcutsEnabled = False
            Me.hotkeyBox.Size = New System.Drawing.Size(369, 29)
            Me.hotkeyBox.TabIndex = 1
            Me.hotkeyBox.Text = "Strg + Alt + Entf"
            Me.hotkeyBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            '
            'cancelHotkeyButton
            '
            Me.cancelHotkeyButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.cancelHotkeyButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.cancelHotkeyButton.Location = New System.Drawing.Point(195, 84)
            Me.cancelHotkeyButton.Name = "cancelHotkeyButton"
            Me.cancelHotkeyButton.Size = New System.Drawing.Size(90, 21)
            Me.cancelHotkeyButton.TabIndex = 2
            Me.cancelHotkeyButton.Text = "Abbrechen"
            Me.cancelHotkeyButton.UseVisualStyleBackColor = True
            '
            'HotkeySelector
            '
            Me.AcceptButton = Me.acceptHotkeyButton
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Window
            Me.CancelButton = Me.cancelHotkeyButton
            Me.ClientSize = New System.Drawing.Size(393, 114)
            Me.Controls.Add(Me.cancelHotkeyButton)
            Me.Controls.Add(Me.hotkeyBox)
            Me.Controls.Add(Me.acceptHotkeyButton)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.KeyPreview = True
            Me.Name = "HotkeySelector"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Hotkey festlegen"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents acceptHotkeyButton As System.Windows.Forms.Button
        Friend WithEvents hotkeyBox As System.Windows.Forms.TextBox
        Friend WithEvents cancelHotkeyButton As System.Windows.Forms.Button
    End Class
End Namespace