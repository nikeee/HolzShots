Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms
Imports Microsoft.WindowsAPICodePack.Taskbar

Namespace UI.Windows.Forms
    Public Module WindowExtensions

        <Extension()>
        Public Sub Flash(window As IWin32Window)
            If window Is Nothing Then Throw New ArgumentNullException(NameOf(window))
            Dim info As New Native.User32.FlashWindowInfo(window.Handle)
            info.Flash()
        End Sub

        <Extension()>
        Public Sub SetProgressValue(window As IWin32Window, currentValue As Integer, maximumValue As Integer)
            If window Is Nothing Then Throw New NullReferenceException()
            Debug.Assert(window.Handle <> IntPtr.Zero)

            If Not TaskbarManager.IsPlatformSupported Then Return

            Try
                TaskbarManager.Instance.SetProgressValue(currentValue, maximumValue, window.Handle)
            Catch
                Debug.Assert(False)
            End Try
        End Sub

        <Extension()>
        Public Sub SetProgressState(window As IWin32Window, state As TaskbarProgressBarState)
            If window Is Nothing Then Throw New NullReferenceException()
            Debug.Assert(window.Handle <> IntPtr.Zero)

            If Not TaskbarManager.IsPlatformSupported Then Return

            Try
                TaskbarManager.Instance.SetProgressState(state, window.Handle)
            Catch
                Debug.Assert(False)
            End Try
        End Sub

        <Extension()>
        Public Sub SetOverlayIcon(window As IWin32Window, ByVal bmp As Bitmap, ByVal accessibilityText As String)
            If window Is Nothing Then Throw New NullReferenceException()
            If bmp Is Nothing Then Throw New ArgumentNullException(NameOf(bmp))

            Debug.Assert(window.Handle <> IntPtr.Zero)
            Debug.Assert(bmp IsNot Nothing)

            If Not TaskbarManager.IsPlatformSupported Then Return

            Dim iconHandle = bmp.GetHicon()
            Try
                Using ico = Icon.FromHandle(iconHandle)
                    TaskbarManager.Instance.SetOverlayIcon(window.Handle, ico, accessibilityText)
                End Using
            Catch
                Debug.Assert(False)
            Finally
                Native.User32.DestroyIcon(iconHandle)
            End Try
        End Sub

        <Extension()>
        Public Sub RemoveOverlayIcon(window As IWin32Window)
            If window Is Nothing Then Throw New NullReferenceException()
            Debug.Assert(window.Handle <> IntPtr.Zero)

            If Not TaskbarManager.IsPlatformSupported Then Return

            Try
                TaskbarManager.Instance.SetOverlayIcon(window.Handle, Nothing, Nothing)
            Catch
                Debug.Assert(False)
            End Try
        End Sub

    End Module
End Namespace
