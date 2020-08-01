Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Threading.Tasks
Imports HolzShots.Drawing
Imports HolzShots.Interop
Imports HolzShots.ScreenshotRelated.Selection
Imports HolzShots.Threading
Imports HolzShots.UI.Specialized

Namespace ScreenshotRelated
    Friend Class ScreenshotMethods
        Private Sub New()
        End Sub

        Public Shared Function CaptureFullscreen() As Screenshot
            Using prio As New ProcessPriorityRequest()
                Dim c As New Bitmap(MainWindow.Instance.Cursor.Size.Width, MainWindow.Instance.Cursor.Size.Height)
                Using g As Graphics = Graphics.FromImage(c)
                    g.SmoothingMode = SmoothingMode.AntiAlias
                    g.CompositingQuality = CompositingQuality.HighQuality
                    Using ico As Icon = Icon.FromHandle(MainWindow.Instance.Cursor.Handle)
                        g.DrawIcon(ico, 0, 0)
                    End Using
                End Using

                Dim screen = ScreenshotCreator.CaptureScreenshot(SystemInformation.VirtualScreen)
                Return Screenshot.FromFullscreen(screen, Cursor.Position)
            End Using
        End Function

        Public Shared Async Function CaptureSelection() As Task(Of Screenshot)
            Debug.Assert(Not AreaSelector.IsInAreaSelector)
            If AreaSelector.IsInAreaSelector Then Return Nothing
            If UserSettings.Current.EnableIngameMode AndAlso HolzShotsEnvironment.IsFullScreen Then Return Nothing

            Using prio As New ProcessPriorityRequest()
                Using screen = ScreenshotCreator.CaptureScreenshot(SystemInformation.VirtualScreen)
                    Using selector As New AreaSelector()
                        Dim selectedArea = Await selector.PromptSelectionAsync(screen).ConfigureAwait(True)

                        Debug.Assert(selectedArea.Width > 0)
                        Debug.Assert(selectedArea.Height > 0)

                        Dim selectedImage As New Bitmap(selectedArea.Width, selectedArea.Height)

                        Using g As Graphics = Graphics.FromImage(selectedImage)
                            g.DrawImage(screen, New Rectangle(0, 0, selectedArea.Width, selectedArea.Height), selectedArea, GraphicsUnit.Pixel)
                        End Using

                        Return Screenshot.FromSelection(selectedImage, Cursor.Position)
                    End Using
                End Using
            End Using
        End Function


        Public Shared Function CaptureWindow(windowHandle As IntPtr, Optional includeMargin As Boolean = True) As Screenshot
            If Native.User32.IsIconic(windowHandle) Then Return Nothing

            Using prio As New ProcessPriorityRequest()
                Using shotSet = GetShotSet(windowHandle, includeMargin)
                    Return Screenshot.FromWindow(shotSet)
                End Using
            End Using
        End Function

        Private Shared Function GetShotSet(windowHandle As IntPtr, includeMargin As Boolean) As WindowScreenshotSet
            ' TODO: Refactor methods to WindowScreenshotSet?
            If HolzShotsEnvironment.IsAeroEnabled Then
                Return DoAeroOn(windowHandle, includeMargin, False)
            ElseIf HolzShotsEnvironment.IsVistaOrHigher Then
                Return DoAeroOff(windowHandle)
            Else
                Debugger.Break() ' wait, you prick!
                Throw New InvalidOperationException("Unsupported operating system.")
            End If
        End Function

        Private Shared Function DoAeroOn(wndHandle As IntPtr, includeMargin As Boolean, smallMargin As Boolean) As WindowScreenshotSet

            Dim nativeRectangle As HolzShots.Native.Rect
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

                        ScreenshotMethodsHelper.StopRedraw(wndHandle)

                        HolzShotsEnvironment.SetForegroundWindowEx(bg.Handle)
                        HolzShotsEnvironment.SetForegroundWindowEx(wndHandle)

                        Using ga As Graphics = Graphics.FromImage(bmpBlack)
                            ga.CompositingQuality = CompositingQuality.HighQuality
                            ga.CopyFromScreen(drawingRectangle.X, drawingRectangle.Y, 0, 0, bg.Size)
                        End Using

                        bg.BackColor = Color.White
                        bg.Refresh()

                        Using ga As Graphics = Graphics.FromImage(bmpWhite)
                            ga.CopyFromScreen(drawingRectangle.X, drawingRectangle.Y, 0, 0, bg.Size)
                        End Using

                        ScreenshotMethodsHelper.StartRedraw(wndHandle)

                        bg.Visible = False

                        Dim result As New Bitmap(bmpWhite.Width, bmpWhite.Height)

                        Drawing.Computation.ComputeAlphaChannel(bmpWhite, bmpBlack, result)

                        ' Old method:
                        ' ScreenshotMethodsHelper.ComputeAlphaChannel(bmpWhite, bmpBlack)

                        Dim windowTitle = ScreenshotMethodsHelper.GetWindowTitle(wndHandle)
                        Dim processName = ScreenshotMethodsHelper.GetProcessNameOfWindow(wndHandle)

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

            Dim windowTitle = ScreenshotMethodsHelper.GetWindowTitle(wndHandle)
            Dim processName = ScreenshotMethodsHelper.GetProcessNameOfWindow(wndHandle)

            Return New WindowScreenshotSet(bmp, cursorPositonOnScreenshot, windowTitle, processName)
        End Function

        Protected Shared Function CaptureCursor() As Bitmap
            Dim c As New Bitmap(MainWindow.Instance.Cursor.Size.Width, MainWindow.Instance.Cursor.Size.Height)
            Using ge As Graphics = Graphics.FromImage(c)
                ge.SmoothingMode = SmoothingMode.AntiAlias
                MainWindow.Instance.Cursor.Draw(ge, New Rectangle(0, 0, c.Width, c.Height))
            End Using
            Return c
        End Function

    End Class
End Namespace
