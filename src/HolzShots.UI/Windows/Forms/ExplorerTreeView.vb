Imports System.Security.Permissions
Imports System.Windows.Forms

Namespace Windows.Forms
    Public Class ExplorerTreeView
        Inherits TreeView

        Protected Overrides ReadOnly Property CreateParams() As CreateParams
            <SecurityPermission(SecurityAction.LinkDemand, Flags:=SecurityPermissionFlag.UnmanagedCode)>
            Get
                Dim cp As CreateParams = MyBase.CreateParams
                If Environment.IsVistaOrHigher AndAlso Not Me.Scrollable Then
                    cp.Style = CInt(cp.Style Or NativeTypes.Tv.NoHScroll)
                End If
                Return cp
            End Get
        End Property

        Protected Overrides Sub OnHandleCreated(ByVal e As EventArgs)
            MyBase.OnHandleCreated(e)
            If Environment.IsVistaOrHigher Then
                Dim hndl As IntPtr = NativeMethods.SendMessage(Handle, DirectCast(NativeTypes.Tv.GetExtendedStyle, Integer), IntPtr.Zero, IntPtr.Zero)
                hndl = New IntPtr(hndl.ToInt32 Or NativeTypes.Tv.ExAutoSHcroll Or NativeTypes.Tv.ExFaceInOutExpandOs)
                NativeMethods.SendMessage(Handle, DirectCast(NativeTypes.Tv.SetExtendedStyle, Integer), IntPtr.Zero, hndl)
                NativeMethods.SetWindowTheme(Handle, "explorer", 0)
            End If
        End Sub

    End Class
End Namespace
