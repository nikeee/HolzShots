Imports System.Runtime.InteropServices
Imports System.Text

Namespace Interop
    Friend NotInheritable Class NativeMethods
        Private Sub New()
        End Sub

        Private Const User32 As String = "user32.dll"

#Region "user32"

#Region "Get/SetForegroundwindow"

        <DllImport(User32)>
        Friend Shared Function SetForegroundWindow(ByVal hWnd As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport(User32)>
        Friend Shared Function GetForegroundWindow() As IntPtr
        End Function

#End Region
#Region "Window Position"

        <DllImport(User32)>
        Friend Shared Function GetWindowRect(ByVal hWnd As IntPtr, ByRef lpRect As Native.Rect) As Boolean
        End Function

        <DllImport(User32)>
        Friend Shared Function GetWindowPlacement(ByVal hWnd As IntPtr, ByRef lpwndpl As NativeTypes.WindowPlacement) As <MarshalAs(UnmanagedType.U1)> Boolean
        End Function

        <DllImport(User32, CharSet:=CharSet.Auto)>
        Friend Shared Function SetWindowPos(hWnd As IntPtr, hWndAfter As IntPtr, x As Integer, y As Integer, width As Integer, height As Integer, flags As Integer) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        ''' <summary>
        ''' The MoveWindow function changes the position and dimensions of the specified window. For a top-level window, the position and dimensions are relative to the upper-left corner of the screen. For a child window, they are relative to the upper-left corner of the parent window's client area.
        ''' </summary>
        ''' <param name="hWnd">Handle to the window.</param>
        ''' <param name="x">Specifies the new position of the left side of the window.</param>
        ''' <param name="y">Specifies the new position of the top of the window.</param>
        ''' <param name="nWidth">Specifies the new width of the window.</param>
        ''' <param name="nHeight">Specifies the new height of the window.</param>
        ''' <param name="bRepaint">Specifies whether the window is to be repainted. If this parameter is TRUE, the window receives a message. If the parameter is FALSE, no repainting of any kind occurs. This applies to the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent window uncovered as a result of moving a child window.</param>
        ''' <returns>If the function succeeds, the return value is nonzero.
        ''' <para>If the function fails, the return value is zero. To get extended error information, call GetLastError.</para></returns>
        <DllImport(User32)>
        Friend Shared Function MoveWindow(ByVal hWnd As IntPtr, ByVal x As Integer, ByVal y As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer, ByVal bRepaint As Boolean) As Boolean
        End Function

#End Region
#Region "Window State/Information"

        <DllImport(User32)>
        Friend Shared Function IsIconic(ByVal hWnd As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport(User32, CharSet:=CharSet.Unicode)>
        Friend Shared Function GetClassName(ByVal hWnd As IntPtr, ByVal lpClassName As StringBuilder, ByVal nMaxCount As Integer) As Integer
        End Function

        <DllImport(User32)>
        Public Shared Function FlashWindowEx(ByRef pwfi As NativeTypes.FlashWindowInfo) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

#End Region
#Region "Drawing-Related"

        <DllImport(User32)>
        Friend Shared Function DestroyIcon(ByVal hIcon As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport(User32)>
        Friend Shared Function LockWindowUpdate(ByVal hWndLock As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport(User32, CharSet:=CharSet.Auto)>
        Friend Shared Function ReleaseDC(hWnd As IntPtr, hDc As IntPtr) As Integer
        End Function

#End Region

#End Region

    End Class
End Namespace
