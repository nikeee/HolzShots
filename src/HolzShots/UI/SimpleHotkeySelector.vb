Imports HolzShots.Input

Namespace UI
    Friend Class SimpleHotkeySelector
        Inherits BorderPanelWindow
        Implements IHotkeySelector

        Private Shared ReadOnly KeyArray As String() = {"Cancel", "Back", "Tab", "LineFeed", "Clear", "Enter", "Return", "Pause", "CapsLock", "Capital", "Escape", "Space", "PageUp", "PageDown", "Next", "End", "Home", "Left", "Up", "Right", "Down", "Select", "Print", "Execute", "PrintScreen", "Insert", "Delete", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "DockPanelMouseWatcher", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "LWin", "RWin", "NumPad0", "NumPad1", "NumPad2", "NumPad3", "NumPad4", "NumPad5", "NumPad6", "NumPad7", "NumPad8", "NumPad9", "Multiply", "Add", "Separator", "Subtract", "Decimal", "Divide", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12", "F13", "F14", "F15", "F16", "F17", "F18", "F19", "NumLock", "Scroll", "BrowserBack", "BrowserForward", "BrowserRefresh", "BrowserStop", "BrowserSearch", "BrowserFavorites", "BrowserHome"}

        Private _selectedKeyStroke As Hotkey
        Public ReadOnly Property SelectedKeyStroke As Hotkey Implements IHotkeySelector.SelectedKeyStroke
            Get
                Return _selectedKeyStroke
            End Get
        End Property

        Public Shadows Function ShowDialog() As DialogResult Implements IHotkeySelector.ShowDialog
            Return MyBase.ShowDialog()
        End Function

        Sub New(parent As Form, initialKeyStroke As Hotkey, title As String)
            MyBase.New()

            If parent Is Nothing Then Throw New ArgumentNullException(NameOf(parent))

            InitializeComponent()
            primaryKey.Items.AddRange(KeyArray)

            Owner = parent

            If Not String.IsNullOrEmpty(title) Then
                Text = title
            End If
            _selectedKeyStroke = initialKeyStroke

            UpdateDisplay()
        End Sub

        Private Sub UpdateDisplay()
            Dim stroke = _selectedKeyStroke
            Debug.Assert(stroke IsNot Nothing)
            Debug.Assert(Not stroke.IsNone())

            Dim keyStr = stroke.Key.ToString()
            Dim index = primaryKey.Items.IndexOf(keyStr)
            Debug.Assert(index > -1)

            primaryKey.SelectedItem = primaryKey.Items(index)

            Dim m = stroke.Modifiers
            modWin.Checked = (m And Input.ModifierKeys.Win) = Input.ModifierKeys.Win
            modAlt.Checked = (m And Input.ModifierKeys.Alt) = Input.ModifierKeys.Alt
            modCtrl.Checked = (m And Input.ModifierKeys.Control) = Input.ModifierKeys.Control
            modShift.Checked = (m And Input.ModifierKeys.Shift) = Input.ModifierKeys.Shift

            ' If any key is checked, m should not be none
            ' -> if m is none, no modifier should be checked
            Debug.Assert((modWin.Checked OrElse modAlt.Checked OrElse modCtrl.Checked OrElse modShift.Checked) Xor (m = Input.ModifierKeys.None))
        End Sub

        Private Sub UpdateKeystroke()
            Dim key As Keys = CType(Keys.Parse(GetType(Keys), primaryKey.Text), Keys)

            Dim m As ModifierKeys
            If modWin.Checked Then m = m Or Input.ModifierKeys.Win
            If modAlt.Checked Then m = m Or Input.ModifierKeys.Alt
            If modCtrl.Checked Then m = m Or Input.ModifierKeys.Control
            If modShift.Checked Then m = m Or Input.ModifierKeys.Shift

            _selectedKeyStroke = New Hotkey(m, key)
        End Sub

        Private Sub PrimaryKeySelectedIndexChanged(sender As Object, e As EventArgs) Handles primaryKey.SelectedIndexChanged
            UpdateKeystroke()
        End Sub

        Private Sub ModKeyCheckedChanged(sender As Object, e As EventArgs) Handles modShift.CheckedChanged, modCtrl.CheckedChanged, modAlt.CheckedChanged, modWin.CheckedChanged
            UpdateKeystroke()
        End Sub

        Private Sub AcceptHotkeyButtonClick(sender As Object, e As EventArgs) Handles acceptHotkeyButton.Click
            DialogResult = DialogResult.OK
        End Sub

        Private Sub CancelHotkeyButtonClick(sender As Object, e As EventArgs) Handles cancelHotkeyButton.Click
            DialogResult = DialogResult.Cancel
        End Sub
    End Class
End Namespace
