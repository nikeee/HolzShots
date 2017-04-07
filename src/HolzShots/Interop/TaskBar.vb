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


        Private Shared _position As NativeTypes.TaskbarPosition
        Public Shared ReadOnly Property Position As NativeTypes.TaskbarPosition
            Get
                If Not _initialized Then Initialize()
                Return _position
            End Get
        End Property

        Private Shared _initialized As Boolean
        Private Shared Sub Initialize()

            Dim data = New NativeTypes.AppBarData()
            data.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(data)
            Dim retval = NativeMethods.SHAppBarMessage(NativeTypes.Abm.GetTaskBarPos, data)

            If retval = IntPtr.Zero Then
                _rectangle = New Rectangle(0, 0, -1, -1)
                _position = NativeTypes.TaskbarPosition.Unknown
            Else
                _rectangle = data.rc
                _position = data.uEdge
            End If


            _initialized = True
        End Sub
    End Class
End Namespace
