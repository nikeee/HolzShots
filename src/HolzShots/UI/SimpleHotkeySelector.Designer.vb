Namespace UI
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class SimpleHotkeySelector
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SimpleHotkeySelector))
            Me.acceptHotkeyButton = New System.Windows.Forms.Button()
            Me.cancelHotkeyButton = New System.Windows.Forms.Button()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.primaryKey = New System.Windows.Forms.ComboBox()
            Me.modAlt = New System.Windows.Forms.CheckBox()
            Me.modCtrl = New System.Windows.Forms.CheckBox()
            Me.modShift = New System.Windows.Forms.CheckBox()
            Me.modWin = New System.Windows.Forms.CheckBox()
            Me.SuspendLayout()
            '
            'acceptHotkeyButton
            '
            Me.acceptHotkeyButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.acceptHotkeyButton.Location = New System.Drawing.Point(177, 83)
            Me.acceptHotkeyButton.Name = "acceptHotkeyButton"
            Me.acceptHotkeyButton.Size = New System.Drawing.Size(90, 23)
            Me.acceptHotkeyButton.TabIndex = 0
            Me.acceptHotkeyButton.Text = "Apply"
            Me.acceptHotkeyButton.UseVisualStyleBackColor = True
            '
            'cancelHotkeyButton
            '
            Me.cancelHotkeyButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.cancelHotkeyButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.cancelHotkeyButton.Location = New System.Drawing.Point(81, 83)
            Me.cancelHotkeyButton.Name = "cancelHotkeyButton"
            Me.cancelHotkeyButton.Size = New System.Drawing.Size(90, 23)
            Me.cancelHotkeyButton.TabIndex = 2
            Me.cancelHotkeyButton.Text = "Cancel"
            Me.cancelHotkeyButton.UseVisualStyleBackColor = True
            '
            'Label2
            '
            Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(130, 27)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(19, 20)
            Me.Label2.TabIndex = 7
            Me.Label2.Text = "+"
            '
            'primaryKey
            '
            Me.primaryKey.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.primaryKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.primaryKey.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.primaryKey.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.primaryKey.FormattingEnabled = True
            Me.primaryKey.Items.AddRange(New Object() {"Cancel", "Back", "Tab", "LineFeed", "Clear", "Enter", "Return", "Pause", "CapsLock", "Capital", "Escape", "Space", "PageUp", "PageDown", "Next", "End", "Home", "Left", "Up", "Right", "Down", "Select", "Print", "Execute", "PrintScreen", "Insert", "Delete", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "DockPanelMouseWatcher", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "LWin", "RWin", "NumPad0", "NumPad1", "NumPad2", "NumPad3", "NumPad4", "NumPad5", "NumPad6", "NumPad7", "NumPad8", "NumPad9", "Multiply", "Add", "Separator", "Subtract", "Decimal", "Divide", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12", "F13", "F14", "F15", "F16", "F17", "F18", "F19", "NumLock", "Scroll", "BrowserBack", "BrowserForward", "BrowserRefresh", "BrowserStop", "BrowserSearch", "BrowserFavorites", "BrowserHome"})
            Me.primaryKey.Location = New System.Drawing.Point(155, 26)
            Me.primaryKey.Name = "primaryKey"
            Me.primaryKey.Size = New System.Drawing.Size(112, 23)
            Me.primaryKey.TabIndex = 6
            '
            'modAlt
            '
            Me.modAlt.AutoSize = True
            Me.modAlt.Location = New System.Drawing.Point(12, 15)
            Me.modAlt.Name = "modAlt"
            Me.modAlt.Size = New System.Drawing.Size(41, 19)
            Me.modAlt.TabIndex = 8
            Me.modAlt.Text = "Alt"
            Me.modAlt.UseVisualStyleBackColor = True
            '
            'modCtrl
            '
            Me.modCtrl.AutoSize = True
            Me.modCtrl.Location = New System.Drawing.Point(12, 40)
            Me.modCtrl.Name = "modCtrl"
            Me.modCtrl.Size = New System.Drawing.Size(47, 19)
            Me.modCtrl.TabIndex = 9
            Me.modCtrl.Text = "Ctrl"
            Me.modCtrl.UseVisualStyleBackColor = True
            '
            'modShift
            '
            Me.modShift.AutoSize = True
            Me.modShift.Location = New System.Drawing.Point(59, 15)
            Me.modShift.Name = "modShift"
            Me.modShift.Size = New System.Drawing.Size(76, 19)
            Me.modShift.TabIndex = 10
            Me.modShift.Text = "Shift"
            Me.modShift.UseVisualStyleBackColor = True
            '
            'modWin
            '
            Me.modWin.AutoSize = True
            Me.modWin.Location = New System.Drawing.Point(59, 40)
            Me.modWin.Name = "modWin"
            Me.modWin.Size = New System.Drawing.Size(75, 19)
            Me.modWin.TabIndex = 11
            Me.modWin.Text = "Windows"
            Me.modWin.UseVisualStyleBackColor = True
            '
            'SimpleHotkeySelector
            '
            Me.AcceptButton = Me.acceptHotkeyButton
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Window
            Me.CancelButton = Me.cancelHotkeyButton
            Me.ClientSize = New System.Drawing.Size(279, 114)
            Me.Controls.Add(Me.modWin)
            Me.Controls.Add(Me.modShift)
            Me.Controls.Add(Me.modCtrl)
            Me.Controls.Add(Me.modAlt)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.primaryKey)
            Me.Controls.Add(Me.cancelHotkeyButton)
            Me.Controls.Add(Me.acceptHotkeyButton)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.KeyPreview = True
            Me.Name = "SimpleHotkeySelector"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Set Hotkey"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents acceptHotkeyButton As System.Windows.Forms.Button
        Friend WithEvents cancelHotkeyButton As System.Windows.Forms.Button
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents primaryKey As System.Windows.Forms.ComboBox
        Friend WithEvents modAlt As System.Windows.Forms.CheckBox
        Friend WithEvents modCtrl As System.Windows.Forms.CheckBox
        Friend WithEvents modShift As System.Windows.Forms.CheckBox
        Friend WithEvents modWin As System.Windows.Forms.CheckBox
    End Class
End Namespace