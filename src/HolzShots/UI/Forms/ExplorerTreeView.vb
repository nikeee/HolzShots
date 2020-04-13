Imports System.Security.Permissions
Imports HolzShots.UI.Forms

Namespace Windows.Forms
    Public Class ExplorerTreeView
        Inherits TreeView

        Protected Overrides ReadOnly Property CreateParams() As CreateParams
            <SecurityPermission(SecurityAction.LinkDemand, Flags:=SecurityPermissionFlag.UnmanagedCode)>
            Get
                Dim cp As CreateParams = MyBase.CreateParams
                If EnvironmentEx.IsVistaOrHigher AndAlso Not Me.Scrollable Then
                    cp.Style = cp.Style Or Native.WindowStyleFlags.TVS_NoHScroll
                End If

                Return cp
            End Get
        End Property

        Protected Overrides Sub OnHandleCreated(ByVal e As EventArgs)
            MyBase.OnHandleCreated(e)
            If EnvironmentEx.IsVistaOrHigher Then
                Dim extendedStyleIntPtr = Native.User32.SendMessage(Handle, Native.WindowMessage.TVM_GetExtendedStyle, IntPtr.Zero, IntPtr.Zero)

                Dim extendedStyle = extendedStyleIntPtr.ToInt32() Or Native.ExtendedWindowStyleFlags.TVS_EX_AutoHScroll Or Native.ExtendedWindowStyleFlags.TVS_EX_FadeInOutExpandOs

                Native.User32.SendMessage(Handle, Native.WindowMessage.TVM_SetExtendedStyle, IntPtr.Zero, New IntPtr(extendedStyle))

                Dim unused = Native.UxTheme.SetWindowTheme(Handle, "explorer", 0)
            End If
        End Sub

    End Class
End Namespace
