Namespace Interop
    Friend Class TaskBar
        'TODO: What is this for?

        Private Shared _rectangle As Rectangle
        Public Shared ReadOnly Property Rectangle As Rectangle
            Get
                If Not _initialized Then Initialize()
                Return _rectangle
            End Get
        End Property


        Private Shared _position As Native.Shell32.TaskbarPosition
        Public Shared ReadOnly Property Position As Native.Shell32.TaskbarPosition
            Get
                If Not _initialized Then Initialize()
                Return _position
            End Get
        End Property

        Private Shared _initialized As Boolean
        Private Shared Sub Initialize()

            Dim data = New Native.Shell32.AppBarData()
            data.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(data)
            Dim retval = Native.Shell32.SHAppBarMessage(Native.Shell32.Abm.GetTaskBarPos, data)

            If retval = IntPtr.Zero Then
                _rectangle = New Rectangle(0, 0, -1, -1)
                _position = Native.Shell32.TaskbarPosition.Unknown
            Else
                _rectangle = data.rc
                _position = data.uEdge
            End If


            _initialized = True
        End Sub
    End Class
End Namespace
