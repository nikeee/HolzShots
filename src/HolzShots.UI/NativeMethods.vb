Imports System.Runtime.InteropServices
Imports System.Text

Friend NotInheritable Class NativeMethods

    Private Const User32 As String = "user32.dll"
    Private Const Gdi32 As String = "gdi32.dll"
    Private Const DwmApi As String = "dwmapi.dll"
    Private Const UxTheme As String = "Uxtheme.dll"

    <DllImport(User32)>
    Public Shared Function FlashWindowEx(ByRef pwfi As NativeTypes.FlashWindowInfo) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

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

    <DllImport(UxTheme)>
    Public Shared Function SetWindowThemeAttribute(ByVal hWnd As IntPtr, ByVal wtype As Integer, ByRef attributes As NativeTypes.WtaOptions, ByVal size As UInteger) As Integer
    End Function
    <DllImport(UxTheme)>
    Public Shared Function GetThemeMargins(hTheme As IntPtr, hdc As IntPtr, iPartId As Integer, iStateId As Integer, iPropId As Integer, rect As IntPtr, ByRef pMargins As NativeTypes.Margin) As Integer
    End Function
    <DllImport(UxTheme, ExactSpelling:=True, CharSet:=CharSet.Unicode)>
    Public Shared Function SetWindowTheme(ByVal hWnd As IntPtr, ByVal pszSubAppName As String, ByVal pszSubIdList As Integer) As Integer
    End Function
#Region "DWM"

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

    <DllImport(DwmApi, PreserveSig:=False)>
    Public Shared Function DwmIsCompositionEnabled() As <MarshalAs(UnmanagedType.U1)> Boolean
    End Function

    <DllImport(DwmApi)>
    Public Shared Function DwmDefWindowProc(ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr, ByRef plResult As IntPtr) As Integer
    End Function

#End Region

    <DllImport(User32, SetLastError:=True)>
    Public Shared Function DestroyIcon(ByVal iconHandle As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    <DllImport(User32)>
    Public Shared Function GetForegroundWindow() As IntPtr
    End Function
End Class
