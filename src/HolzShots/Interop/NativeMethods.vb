Imports System.Runtime.InteropServices
Imports System.Text

Namespace Interop
    Friend NotInheritable Class NativeMethods
        Private Sub New()
        End Sub

        Private Const User32 As String = "user32.dll"
        Private Const Shell32 As String = "shell32.dll"
        Private Const DwmApi As String = "dwmapi.dll"

#Region "shell32"

        <DllImport(Shell32, BestFitMapping:=False, ThrowOnUnmappableChar:=True)>
        Public Shared Sub SHAddToRecentDocs(flag As NativeTypes.ShellAddToRecentDocsFlags, <MarshalAs(UnmanagedType.LPStr)> path As String)
        End Sub

        <DllImport(Shell32)>
        Public Shared Function SHAppBarMessage(msg As NativeTypes.Abm, ByRef data As NativeTypes.AppBarData) As IntPtr
        End Function

#End Region
#Region "dwmapi"

        <DllImport(DwmApi, PreserveSig:=False)>
        Public Shared Function DwmIsCompositionEnabled() As <MarshalAs(UnmanagedType.U1)> Boolean
        End Function

#End Region
#Region "user32"

#Region "FindWindow"

        <DllImport(User32, SetLastError:=True, CharSet:=CharSet.Unicode)>
        Public Shared Function FindWindow(ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr
        End Function

        <DllImport(User32, EntryPoint:="FindWindow", SetLastError:=True, CharSet:=CharSet.Unicode)>
        Public Shared Function FindWindowByClass(ByVal lpClassName As String, ByVal zero As IntPtr) As IntPtr
        End Function

        <DllImport(User32, EntryPoint:="FindWindow", SetLastError:=True, CharSet:=CharSet.Unicode)>
        Public Shared Function FindWindowByCaption(ByVal zero As IntPtr, ByVal lpWindowName As String) As IntPtr
        End Function

        <DllImport(User32, SetLastError:=True, CharSet:=CharSet.Unicode)>
        Public Shared Function FindWindowEx(ByVal parentHandle As IntPtr, ByVal childAfter As IntPtr, ByVal lclassName As String, ByVal windowTitle As String) As IntPtr
        End Function

#End Region
#Region "SendMessage"

        <DllImport(User32)>
        Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As UInt32, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
        End Function

        <DllImport(User32, CharSet:=CharSet.Unicode)>
        Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As UInt32, ByVal wParam As IntPtr, ByVal lParam As StringBuilder) As IntPtr
        End Function

        <DllImport(User32)>
        Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As UInt32, ByVal wParam As IntPtr, <MarshalAs(UnmanagedType.LPWStr)> ByVal lParam As String) As IntPtr
        End Function

#End Region
#Region "Get/SetParent"

        <DllImport(User32, SetLastError:=True, CharSet:=CharSet.Auto)>
        Public Shared Function SetParent(ByVal hWndChild As IntPtr, ByVal hWndNewParent As IntPtr) As IntPtr
        End Function

        <DllImport(User32, ExactSpelling:=True, CharSet:=CharSet.Auto)>
        Public Shared Function GetParent(ByVal hWnd As IntPtr) As IntPtr
        End Function

#End Region
#Region "Get/SetForegroundwindow"

        <DllImport(User32)>
        Friend Shared Function SetForegroundWindow(ByVal hWnd As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport(User32)>
        Public Shared Function GetForegroundWindow() As IntPtr
        End Function

#End Region
#Region "GetWindowText"

        <DllImport(User32, SetLastError:=True, CharSet:=CharSet.Unicode)>
        Public Shared Function GetWindowText(ByVal hwnd As IntPtr, ByVal lpString As StringBuilder, ByVal cch As Integer) As Integer
        End Function

        <DllImport(User32, SetLastError:=True, CharSet:=CharSet.Auto)>
        Public Shared Function GetWindowTextLength(ByVal hwnd As IntPtr) As Integer
        End Function

#End Region
#Region "Window Position"

        <DllImport(User32)>
        Public Shared Function GetWindowRect(ByVal hWnd As IntPtr, ByRef lpRect As NativeTypes.Rect) As Boolean
        End Function

        <DllImport(User32)>
        Public Shared Function GetWindowPlacement(ByVal hWnd As IntPtr, ByRef lpwndpl As NativeTypes.WindowPlacement) As <MarshalAs(UnmanagedType.U1)> Boolean
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
        Public Shared Function MoveWindow(ByVal hWnd As IntPtr, ByVal x As Integer, ByVal y As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer, ByVal bRepaint As Boolean) As Boolean
        End Function

#End Region
#Region "Window State/Information"

        <DllImport(User32)>
        Public Shared Function IsIconic(ByVal hWnd As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport(User32, CharSet:=CharSet.Unicode)>
        Friend Shared Function GetClassName(ByVal hWnd As IntPtr, ByVal lpClassName As StringBuilder, ByVal nMaxCount As Integer) As Integer
        End Function

#End Region
#Region "Drawing-Related"

        <DllImport(User32)>
        Public Shared Function DestroyIcon(ByVal hIcon As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport(User32)>
        Public Shared Function LockWindowUpdate(ByVal hWndLock As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport(User32, CharSet:=CharSet.Auto)>
        Friend Shared Function ReleaseDC(hWnd As IntPtr, hDc As IntPtr) As Integer
        End Function

#End Region
#Region "Window-Threading"

        <DllImport(User32)>
        Friend Shared Function AttachThreadInput(ByVal idAttach As Integer, ByVal idAttachTo As Integer, ByVal fAttach As Boolean) As Boolean
        End Function

        <DllImport(User32, SetLastError:=True)>
        Friend Shared Function GetWindowThreadProcessId(ByVal hWnd As IntPtr, ByRef lpdwProcessId As Integer) As Integer
        End Function

#End Region

#End Region

    End Class
End Namespace
