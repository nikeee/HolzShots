Imports HolzShots.Input

Namespace UI
    ''' <summary> Not used. See SimpleHotkeySelector. </summary>
    Friend Class HotkeySelector
        Inherits BorderPanelWindow
        Implements IHotkeySelector

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

            If parent Is Nothing Then
                Throw New ArgumentNullException(NameOf(parent))
            End If

            InitializeComponent()
            Owner = parent

            If Not String.IsNullOrEmpty(title) Then Text = title

            _selectedKeyStroke = initialKeyStroke

            UpdateTextBox()
        End Sub

        Private Sub UpdateKeystroke(e As KeyEventArgs)
            _selectedKeyStroke = Hotkey.FromKeyboardEvent(e)
            e.SuppressKeyPress = False
            e.Handled = True
            UpdateTextBox()
        End Sub

        Private Sub UpdateTextBox()
            If _selectedKeyStroke Is Nothing Then Return

            hotkeyBox.Text = _selectedKeyStroke.ToString()
        End Sub

        Private Sub HotkeySelectorKeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
            UpdateKeystroke(e)
        End Sub
    End Class
End Namespace
