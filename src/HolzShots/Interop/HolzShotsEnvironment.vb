Imports System.Text
Imports StartupHelper

Namespace Interop
    Friend Class HolzShotsEnvironment

        Public Shared ReadOnly Property CurrentStartupManager As New StartupManager(Reflection.Assembly.GetEntryAssembly().Location,
                                                                      LibraryInformation.Name,
                                                                      RegistrationScope.Local,
                                                                      False,
                                                                      StartupProviders.Registry,
                                                                      AutorunParamter)

        Private Sub New()
        End Sub

        Public Shared Function IsFullScreen() As Boolean
            Dim hndl As IntPtr = Native.User32.GetForegroundWindow()
            Dim sb As New StringBuilder()

            Dim unused = Native.User32.GetClassName(hndl, sb, sb.Capacity)

            If sb.ToString = "WorkerW" Then Return False
            Dim rct As Native.Rect
            Native.User32.GetWindowRect(hndl, rct)

            Return Screen.PrimaryScreen.Bounds.Height = rct.Bottom AndAlso Screen.PrimaryScreen.Bounds.Width = rct.Right
        End Function

        Public Shared Property AutoStart As Boolean
            Get
                Return CurrentStartupManager.IsRegistered
            End Get
            Set(ByVal value As Boolean)
                If value Then
                    CurrentStartupManager.Register()
                Else
                    CurrentStartupManager.Unregister()
                End If
            End Set
        End Property

        Public Shared ReadOnly Property IsVistaOrHigher As Boolean = Environment.OSVersion.Version.Major >= 6
        Public Shared ReadOnly Property IsAeroEnabled As Boolean
            Get
                Return IsVistaOrHigher AndAlso Native.DwmApi.DwmIsCompositionEnabled()
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
            lThreadWindow = Native.User32.GetWindowThreadProcessId(hWndWindow, 0)
            lThreadForeWin = Native.User32.GetWindowThreadProcessId(Native.User32.GetForegroundWindow(), 0)
            If lThreadWindow = lThreadForeWin Then
                ' Vordergrundfenster und zu aktivierendes Fenster gehören zum
                ' gleichen Thread. SetForegroundWindow allein reicht aus:
                Return Native.User32.SetForegroundWindow(hWndWindow)
            Else
                ' Das Vordergrundfenster gehört zu einem anderen Thread als das
                ' Fenster, das neues Vordergrundfenster werden soll. Mittels
                ' AttachThreadInput erhaten wir kurzzeitig Zugriff auf die
                ' Eingabeverarbeitung des Threads des Vordergrundfensters,
                ' so dass SetForegroundWindow wie erwartet arbeitet:
                Dim result As Boolean
                Native.User32.AttachThreadInput(lThreadForeWin, lThreadWindow, True)
                result = Native.User32.SetForegroundWindow(hWndWindow)
                Native.User32.AttachThreadInput(lThreadForeWin, lThreadWindow, False)
                Return result
            End If
        End Function
    End Class
End Namespace
