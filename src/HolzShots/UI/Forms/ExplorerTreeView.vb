Imports System.Security.Permissions
Imports System.Windows.Forms
Imports HolzShots.UI.Forms

Namespace Windows.Forms
    Public Class ExplorerTreeView
        Inherits TreeView

        Protected Overrides ReadOnly Property CreateParams() As CreateParams
            <SecurityPermission(SecurityAction.LinkDemand, Flags:=SecurityPermissionFlag.UnmanagedCode)>
            Get
                Dim cp As CreateParams = MyBase.CreateParams
                If EnvironmentEx.IsVistaOrHigher AndAlso Not Me.Scrollable Then
                    cp.Style = CInt(cp.Style Or Interop.NativeTypes.Tv.NoHScroll)
                End If
                Return cp
            End Get
        End Property

        Protected Overrides Sub OnHandleCreated(ByVal e As EventArgs)
            MyBase.OnHandleCreated(e)
            If EnvironmentEx.IsVistaOrHigher Then
                Dim hndl As IntPtr = Interop.NativeMethods.SendMessage(Handle, DirectCast(Interop.NativeTypes.Tv.GetExtendedStyle, Integer), IntPtr.Zero, IntPtr.Zero)
                hndl = New IntPtr(hndl.ToInt32 Or Interop.NativeTypes.Tv.ExAutoSHcroll Or Interop.NativeTypes.Tv.ExFaceInOutExpandOs)
                Interop.NativeMethods.SendMessage(Handle, DirectCast(Interop.NativeTypes.Tv.SetExtendedStyle, Integer), IntPtr.Zero, hndl)
                Interop.NativeMethods.SetWindowTheme(Handle, "explorer", 0)
            End If
        End Sub

    End Class
End Namespace
