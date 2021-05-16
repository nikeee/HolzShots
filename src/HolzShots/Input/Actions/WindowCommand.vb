Imports HolzShots.Composition.Command
Imports HolzShots.Threading
Imports HolzShots.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports HolzShots.Windows.Forms

Namespace Input.Actions

    <Command("captureWindow")>
    Public Class WindowCommand
        Inherits CapturingCommand

        Public Overrides Async Function Invoke(parameters As IReadOnlyDictionary(Of String, String), settingsContext As HSSettings) As Task
            ' TODO: Add proper assertion
            ' Debug.Assert(ManagedSettings.EnableWindowScreenshot)

            ' TODO: Re-add proper if condition
            ' If ManagedSettings.EnableWindowScreenshot Then
            Dim h As IntPtr = Native.User32.GetForegroundWindow()

            Dim info As Native.User32.WindowPlacement
            Native.User32.GetWindowPlacement(h, info)

            Dim shot = CaptureWindow(h)
            Await ProcessCapturing(shot, settingsContext).ConfigureAwait(True)
            ' End If
        End Function

        Private Shared Function CaptureWindow(windowHandle As IntPtr, Optional includeMargin As Boolean = True) As Screenshot
            If Native.User32.IsIconic(windowHandle) Then Return Nothing

            Using prio As New ProcessPriorityRequest()
                Using shotSet = GetShotSet(windowHandle, includeMargin)
                    Return Screenshot.FromWindow(shotSet)
                End Using
            End Using
        End Function

        Private Shared Function GetShotSet(windowHandle As IntPtr, includeMargin As Boolean) As WindowScreenshotSet
            ' TODO: Refactor methods to WindowScreenshotSet?
            If HolzShots.Windows.Forms.EnvironmentEx.IsAeroEnabled() Then
                Return DoAeroOn(windowHandle, includeMargin, False)
            ElseIf HolzShots.Windows.Forms.EnvironmentEx.IsVistaOrHigher Then
                Return DoAeroOff(windowHandle)
            Else
                Debugger.Break() ' wait, you prick!
                Throw New InvalidOperationException("Unsupported operating system.")
            End If
        End Function

        ' TODO: Rewrite this whole mess

        Private Shared Function DoAeroOn(wndHandle As IntPtr, includeMargin As Boolean, smallMargin As Boolean) As WindowScreenshotSet

            Dim nativeRectangle As Native.Rect
            Native.User32.GetWindowRect(wndHandle, nativeRectangle)

            Dim placement As Native.User32.WindowPlacement
            Native.User32.GetWindowPlacement(wndHandle, placement)

            If includeMargin Then
                If placement.showCmd <> 3 Then
                    Dim left = nativeRectangle.Left - If(smallMargin, 4, 17)
                    Dim top = nativeRectangle.Top - If(smallMargin, 4, 17)
                    Dim right = nativeRectangle.Right + If(smallMargin, 4, 21)
                    Dim bottom = nativeRectangle.Bottom + If(smallMargin, 4, 21)

                    nativeRectangle = New Native.Rect(
                        Math.Max(left, SystemInformation.VirtualScreen.Left),
                        Math.Max(top, SystemInformation.VirtualScreen.Top),
                        Math.Min(right, SystemInformation.VirtualScreen.Right),
                        Math.Min(bottom, SystemInformation.VirtualScreen.Bottom)
                    )
                Else
                    Dim tmprect As Rectangle = nativeRectangle
                    Dim center As New Point(tmprect.X + CInt(tmprect.Width / 2), tmprect.Y + CInt(tmprect.Height / 2))
                    nativeRectangle = Screen.GetWorkingArea(center) 'NativeTypes.Rect.FromRectangle(Screen.GetWorkingArea(center))
                End If
            End If

            Dim drawingRectangle As Rectangle = nativeRectangle

            If drawingRectangle.Size.Height < 0 OrElse drawingRectangle.Size.Width < 0 Then Return Nothing

            Dim cursorPositonOnScreenshot As New Point(Cursor.Position.X - drawingRectangle.Location.X, Cursor.Position.Y - drawingRectangle.Location.Y)


            Using bg As New BackgroundForm(drawingRectangle.Location, drawingRectangle.Size) _
                'New Point(rct.Left, rct.Top), New Size(rct.Right - rct.Left, rct.Bottom - rct.Top))
                'Using bg As New FloatingWindow(nrct.X, nrct.Y, nrct.Width, nrct.Height)
                Using bmpBlack As New Bitmap(drawingRectangle.Width, drawingRectangle.Height, PixelFormat.Format32bppPArgb)
                    Using bmpWhite As New Bitmap(drawingRectangle.Width, drawingRectangle.Height, PixelFormat.Format32bppPArgb)

                        bg.Visible = True

                        WindowRedraw.StopRedraw(wndHandle)

                        Native.User32.SetForegroundWindowEx(bg.Handle)
                        Native.User32.SetForegroundWindowEx(wndHandle)

                        Using ga As Graphics = Graphics.FromImage(bmpBlack)
                            ga.CompositingQuality = CompositingQuality.HighQuality
                            ga.CopyFromScreen(drawingRectangle.X, drawingRectangle.Y, 0, 0, bg.Size)
                        End Using

                        bg.BackColor = Color.White
                        bg.Refresh()

                        Using ga As Graphics = Graphics.FromImage(bmpWhite)
                            ga.CopyFromScreen(drawingRectangle.X, drawingRectangle.Y, 0, 0, bg.Size)
                        End Using

                        WindowRedraw.StartRedraw(wndHandle)

                        bg.Visible = False

                        Dim result As New Bitmap(bmpWhite.Width, bmpWhite.Height)

                        Computation.ComputeAlphaChannel(bmpWhite, bmpBlack, result)

                        ' Old method:
                        ' ScreenshotMethodsHelper.ComputeAlphaChannel(bmpWhite, bmpBlack)

                        Dim windowTitle = WindowInformation.GetWindowTitle(wndHandle)
                        Dim processName = WindowInformation.GetProcessNameOfWindow(wndHandle)

                        Return New WindowScreenshotSet(result, cursorPositonOnScreenshot, windowTitle, processName)
                    End Using
                End Using
            End Using
        End Function

        Private Shared Function DoAeroOff(wndHandle As IntPtr) As WindowScreenshotSet
            Dim nativeRectangle As Native.Rect
            Native.User32.GetWindowRect(wndHandle, nativeRectangle)
            Dim drawingRectangle As Rectangle = nativeRectangle

            Dim cursorPositonOnScreenshot As New Point(Cursor.Position.X - drawingRectangle.Location.X, Cursor.Position.Y - drawingRectangle.Location.Y)

            Dim bmp = ScreenshotCreator.CaptureScreenshot(drawingRectangle)

            Dim windowTitle = WindowInformation.GetWindowTitle(wndHandle)
            Dim processName = WindowInformation.GetProcessNameOfWindow(wndHandle)

            Return New WindowScreenshotSet(bmp, cursorPositonOnScreenshot, windowTitle, processName)
        End Function

    End Class
End Namespace
