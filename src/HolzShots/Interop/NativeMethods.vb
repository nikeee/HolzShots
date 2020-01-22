Imports System.Runtime.InteropServices
Imports System.Text

Namespace Interop
    Friend NotInheritable Class NativeMethods
        Private Sub New()
        End Sub

        Private Const User32 As String = "user32.dll"
        Private Const Shell32 As String = "shell32.dll"
        Private Const DwmApi As String = "dwmapi.dll"
        Private Const UxTheme As String = "Uxtheme.dll"

#Region "shell32"

        <DllImport(Shell32, BestFitMapping:=False, ThrowOnUnmappableChar:=True)>
        Friend Shared Sub SHAddToRecentDocs(flag As NativeTypes.ShellAddToRecentDocsFlags, <MarshalAs(UnmanagedType.LPStr)> path As String)
        End Sub

        <DllImport(Shell32)>
        Friend Shared Function SHAppBarMessage(msg As NativeTypes.Abm, ByRef data As NativeTypes.AppBarData) As IntPtr
        End Function

#End Region
#Region "dwmapi"

        <DllImport(DwmApi, PreserveSig:=False)>
        Friend Shared Function DwmIsCompositionEnabled() As <MarshalAs(UnmanagedType.U1)> Boolean
        End Function

        <DllImport(DwmApi)>
        Public Shared Function DwmDefWindowProc(ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr, ByRef plResult As IntPtr) As Integer
        End Function

        <DllImport(DwmApi, PreserveSig:=True)>
        Public Shared Function DwmExtendFrameIntoClientArea(ByVal hwnd As IntPtr, ByRef margins As NativeTypes.Margin) As Integer
        End Function

        <DllImport(DwmApi, EntryPoint:="#104")>
        Public Shared Function DwmSetColorizationColor(ByVal colorizationColor As Integer, ByVal colorizationOpaqueBlend As Boolean, ByVal opacity As Integer) As Integer
        End Function

        <DllImport(DwmApi, PreserveSig:=False)>
        Public Shared Sub DwmGetColorizationColor(ByRef colorizationColor As Integer, <MarshalAs(UnmanagedType.Bool)> ByRef colorizationOpaqueBlend As Boolean)
        End Sub

        <DllImport(DwmApi, EntryPoint:="#127", CharSet:=Runtime.InteropServices.CharSet.Ansi, SetLastError:=True, ExactSpelling:=True)>
        Public Shared Sub DwmGetColorizationParameters(ByRef parameters As NativeTypes.DwmColorizationParams)
        End Sub

        <DllImport(DwmApi, EntryPoint:="#131", CharSet:=Runtime.InteropServices.CharSet.Ansi, SetLastError:=True, ExactSpelling:=True)>
        Public Shared Sub DwmSetColorizationParameters(ByRef parameters As NativeTypes.DwmColorizationParams)
        End Sub

#End Region
#Region "user32"

#Region "FindWindow"

        <DllImport(User32, SetLastError:=True, CharSet:=CharSet.Unicode)>
        Friend Shared Function FindWindow(ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr
        End Function

        <DllImport(User32, EntryPoint:="FindWindow", SetLastError:=True, CharSet:=CharSet.Unicode)>
        Friend Shared Function FindWindowByClass(ByVal lpClassName As String, ByVal zero As IntPtr) As IntPtr
        End Function

        <DllImport(User32, EntryPoint:="FindWindow", SetLastError:=True, CharSet:=CharSet.Unicode)>
        Friend Shared Function FindWindowByCaption(ByVal zero As IntPtr, ByVal lpWindowName As String) As IntPtr
        End Function

        <DllImport(User32, SetLastError:=True, CharSet:=CharSet.Unicode)>
        Friend Shared Function FindWindowEx(ByVal parentHandle As IntPtr, ByVal childAfter As IntPtr, ByVal lclassName As String, ByVal windowTitle As String) As IntPtr
        End Function

#End Region
#Region "SendMessage"

        <DllImport(User32)>
        Friend Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As UInt32, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
        End Function

        <DllImport(User32, CharSet:=CharSet.Unicode)>
        Friend Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As UInt32, ByVal wParam As IntPtr, ByVal lParam As StringBuilder) As IntPtr
        End Function

        <DllImport(User32)>
        Friend Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As UInt32, ByVal wParam As IntPtr, <MarshalAs(UnmanagedType.LPWStr)> ByVal lParam As String) As IntPtr
        End Function

#End Region
#Region "Get/SetParent"

        <DllImport(User32, SetLastError:=True, CharSet:=CharSet.Auto)>
        Friend Shared Function SetParent(ByVal hWndChild As IntPtr, ByVal hWndNewParent As IntPtr) As IntPtr
        End Function

        <DllImport(User32, ExactSpelling:=True, CharSet:=CharSet.Auto)>
        Friend Shared Function GetParent(ByVal hWnd As IntPtr) As IntPtr
        End Function

#End Region
#Region "Get/SetForegroundwindow"

        <DllImport(User32)>
        Friend Shared Function SetForegroundWindow(ByVal hWnd As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport(User32)>
        Friend Shared Function GetForegroundWindow() As IntPtr
        End Function

#End Region
#Region "GetWindowText"

        <DllImport(User32, SetLastError:=True, CharSet:=CharSet.Unicode)>
        Friend Shared Function GetWindowText(ByVal hwnd As IntPtr, ByVal lpString As StringBuilder, ByVal cch As Integer) As Integer
        End Function

        <DllImport(User32, SetLastError:=True, CharSet:=CharSet.Auto)>
        Friend Shared Function GetWindowTextLength(ByVal hwnd As IntPtr) As Integer
        End Function

#End Region
#Region "Window Position"

        <DllImport(User32)>
        Friend Shared Function GetWindowRect(ByVal hWnd As IntPtr, ByRef lpRect As NativeTypes.Rect) As Boolean
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
#Region "Window-Threading"

        <DllImport(User32)>
        Friend Shared Function AttachThreadInput(ByVal idAttach As Integer, ByVal idAttachTo As Integer, ByVal fAttach As Boolean) As Boolean
        End Function

        <DllImport(User32, SetLastError:=True)>
        Friend Shared Function GetWindowThreadProcessId(ByVal hWnd As IntPtr, ByRef lpdwProcessId As Integer) As Integer
        End Function

#End Region

#End Region
#Region "ustheme"

        <DllImport(UxTheme)>
        Public Shared Function SetWindowThemeAttribute(ByVal hWnd As IntPtr, ByVal wtype As Integer, ByRef attributes As NativeTypes.WtaOptions, ByVal size As UInteger) As Integer
        End Function
        <DllImport(UxTheme)>
        Public Shared Function GetThemeMargins(hTheme As IntPtr, hdc As IntPtr, iPartId As Integer, iStateId As Integer, iPropId As Integer, rect As IntPtr, ByRef pMargins As NativeTypes.Margin) As Integer
        End Function
        <DllImport(UxTheme, ExactSpelling:=True, CharSet:=CharSet.Unicode)>
        Public Shared Function SetWindowTheme(ByVal hWnd As IntPtr, ByVal pszSubAppName As String, ByVal pszSubIdList As Integer) As Integer
        End Function

#End Region


    End Class
End Namespace
