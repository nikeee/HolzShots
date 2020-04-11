Imports System.Linq
Imports System.Security
Imports System.Text
Imports System.Threading
Imports Microsoft.Win32
Imports StartupHelper

Namespace Interop
    Friend Class HolzShotsEnvironment

        Public Shared ReadOnly Property CurrentStartupManager As New StartupManager(Reflection.Assembly.GetEntryAssembly().Location,
                                                                      LibraryInformation.Name,
                                                                      RegistrationScope.Local,
                                                                      False,
                                                                      StartupProviders.Registry,
                                                                      "-autorun")

        Private Sub New()
        End Sub

        Private Const MetroStartMenu As String = "ImmersiveLauncher"
        Private Const GtkWindow As String = "gdkWindowToplevel"
        Private Shared ReadOnly MetroWindowClasses As String() = {"Windows.UI.Core.CoreWindow", "ImmersiveSplashScreenWindowClass", MetroStartMenu}

        Private Shared ReadOnly CanBeInMetroApp As Boolean = IsEightOrHigher

        Public Shared ReadOnly ApplicationDataRoaming As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)

        Public Shared Function IsInMetroApplication() As Boolean
            If Not CanBeInMetroApp Then Return False

            Dim hwnd As IntPtr = NativeMethods.GetForegroundWindow()
            Dim sb = New StringBuilder(255)
            NativeMethods.GetClassName(hwnd, sb, sb.Capacity)
            Return MetroWindowClasses.Contains(sb.ToString())
        End Function

        Public Shared Function SwitchToDesktopApplication() As Boolean
            Dim hwnd As IntPtr = NativeMethods.GetForegroundWindow()
            Dim sb = New StringBuilder(255)
            NativeMethods.GetClassName(hwnd, sb, sb.Capacity)
            Return MetroWindowClasses.Contains(sb.ToString())
        End Function

        Public Shared Function IsFullScreen() As Boolean
            Dim hndl As IntPtr = NativeMethods.GetForegroundWindow()
            Dim sb As New StringBuilder()
            NativeMethods.GetClassName(hndl, sb, sb.Capacity)
            If sb.ToString = "WorkerW" Then Return False
            Dim rct As NativeTypes.Rect
            NativeMethods.GetWindowRect(hndl, rct)
            Return Screen.PrimaryScreen.Bounds.Height = rct.Bottom AndAlso Screen.PrimaryScreen.Bounds.Width = rct.Right
        End Function

        Public Shared WriteOnly Property AutoStart As Boolean
            Set(ByVal value As Boolean)
                If value Then
                    CurrentStartupManager.Register()
                Else
                    CurrentStartupManager.Unregister()
                End If
            End Set
        End Property

        Public Shared ReadOnly Property IsEightOrHigher As Boolean = (Environment.OSVersion.Version.Major = 6 AndAlso Environment.OSVersion.Version.Minor >= 2) OrElse Environment.OSVersion.Version.Major > 6
        Public Shared ReadOnly Property IsVistaOrHigher As Boolean = Environment.OSVersion.Version.Major >= 6
        Public Shared ReadOnly Property IsAeroEnabled As Boolean
            Get
                Return IsVistaOrHigher AndAlso NativeMethods.DwmIsCompositionEnabled()
            End Get
        End Property

        Friend Shared Function SetForegroundWindowEx(ByVal hWndWindow As IntPtr) As Boolean
            ' Dient dem Setzen des Vordergrundfensters mit der Funktion
            ' SetForegroundWindow, die sich unter neueren Windows-Versionen
            ' anders verhält als unter Windows 95 und Windows NT 4.0.
            ' Der Rückgabewert ist True, wenn das Fenster erfolgreich in den
            ' Vordergrund gebracht werden konnte.
            Dim lThreadForeWin As Integer
            ' Thread-ID für das aktuelle Vordergrundfenster
            Dim lThreadWindow As Integer
            ' Thread-ID für das in hWndWindow spezifizierte
            ' Fenster, das in den Vordergrund des Desktops
            ' gebracht werden soll.
            ' Falls das Fenster dem gleichen Thread wie das aktuelle
            ' Vordergrundfenster angehört, ist kein Workaround erforderlich:
            lThreadWindow = NativeMethods.GetWindowThreadProcessId(hWndWindow, 0)
            lThreadForeWin = NativeMethods.GetWindowThreadProcessId(NativeMethods.GetForegroundWindow(), 0)
            If lThreadWindow = lThreadForeWin Then
                ' Vordergrundfenster und zu aktivierendes Fenster gehören zum
                ' gleichen Thread. SetForegroundWindow allein reicht aus:
                Return NativeMethods.SetForegroundWindow(hWndWindow)
            Else
                ' Das Vordergrundfenster gehört zu einem anderen Thread als das
                ' Fenster, das neues Vordergrundfenster werden soll. Mittels
                ' AttachThreadInput erhaten wir kurzzeitig Zugriff auf die
                ' Eingabeverarbeitung des Threads des Vordergrundfensters,
                ' so dass SetForegroundWindow wie erwartet arbeitet:
                Dim result As Boolean
                NativeMethods.AttachThreadInput(lThreadForeWin, lThreadWindow, True)
                result = NativeMethods.SetForegroundWindow(hWndWindow)
                NativeMethods.AttachThreadInput(lThreadForeWin, lThreadWindow, False)
                Return result
            End If
        End Function
    End Class
End Namespace
