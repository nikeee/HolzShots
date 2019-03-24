Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Threading.Tasks
Imports HolzShots.Common.Drawing
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
            If ManagedSettings.EnableIngameMode AndAlso HolzShotsEnvironment.IsFullScreen AndAlso Not HolzShotsEnvironment.IsInMetroApplication() Then Return Nothing

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
            If NativeMethods.IsIconic(windowHandle) Then Return Nothing

            Using prio As New ProcessPriorityRequest()
                Dim isInMetro = HolzShotsEnvironment.IsInMetroApplication()
                Using shotSet = GetShotSet(windowHandle, includeMargin, isInMetro)
                    Return Screenshot.FromWindow(shotSet)
                End Using
            End Using
        End Function

        Private Shared Function GetShotSet(windowHandle As IntPtr, includeMargin As Boolean, isInMetro As Boolean) As WindowScreenshotSet
            ' TODO: Refactor methods to WindowScreenshotSet?
            If HolzShotsEnvironment.IsAeroEnabled AndAlso Not isInMetro Then
                Return DoAeroOn(windowHandle, includeMargin, False)
            ElseIf isInMetro OrElse HolzShotsEnvironment.IsVistaOrHigher Then
                Return DoAeroOff(windowHandle)
            Else
                Debugger.Break() ' wait, you prick!
                Throw New InvalidOperationException("Unsupported operating system.")
            End If
        End Function

        Private Shared Function DoAeroOn(wndHandle As IntPtr, includeMargin As Boolean, smallMargin As Boolean) As WindowScreenshotSet

            Dim rct As NativeTypes.Rect
            Dim plc As NativeTypes.WindowPlacement
            NativeMethods.GetWindowRect(wndHandle, rct)
            NativeMethods.GetWindowPlacement(wndHandle, plc)

            If includeMargin Then
                If plc.showCmd <> 3 Then
                    rct.Left -= If(smallMargin, 4, 17)
                    rct.Right += If(smallMargin, 4, 21)
                    rct.Top -= If(smallMargin, 4, 17)
                    rct.Bottom += If(smallMargin, 4, 21)

                    rct.Bottom = If(rct.Bottom > SystemInformation.VirtualScreen.Bottom, SystemInformation.VirtualScreen.Bottom, rct.Bottom)
                    rct.Top = If(rct.Top < SystemInformation.VirtualScreen.Top, SystemInformation.VirtualScreen.Top, rct.Top)
                    rct.Left = If(rct.Left < SystemInformation.VirtualScreen.Left, SystemInformation.VirtualScreen.Left, rct.Left)
                    rct.Right = If(rct.Right > SystemInformation.VirtualScreen.Right, SystemInformation.VirtualScreen.Right, rct.Right)
                Else
                    Dim tmprect As Rectangle = rct 'rct.ToRectangle()
                    Dim center As New Point(tmprect.X + CInt(tmprect.Width / 2), tmprect.Y + CInt(tmprect.Height / 2))
                    rct = Screen.GetWorkingArea(center) 'NativeTypes.Rect.FromRectangle(Screen.GetWorkingArea(center))
                End If
            End If

            Dim nrct As Rectangle = Rectangle.FromLTRB(rct.Left, rct.Top, rct.Right, rct.Bottom)
            Dim curp As New Point(Cursor.Position.X - nrct.Location.X, Cursor.Position.Y - nrct.Location.Y)

            If nrct.Size.Height < 0 OrElse nrct.Size.Width < 0 Then Return Nothing

            Using bg As New BackgroundForm(nrct.Location, nrct.Size) _
                'New Point(rct.Left, rct.Top), New Size(rct.Right - rct.Left, rct.Bottom - rct.Top))
                'Using bg As New FloatingWindow(nrct.X, nrct.Y, nrct.Width, nrct.Height)
                Using bmpBlack As New Bitmap(nrct.Width, nrct.Height, PixelFormat.Format32bppPArgb)
                    Using bmpWhite As New Bitmap(nrct.Width, nrct.Height, PixelFormat.Format32bppPArgb)

                        bg.Visible = True

                        ScreenshotMethodsHelper.StopRedraw(wndHandle)

                        HolzShotsEnvironment.SetForegroundWindowEx(bg.Handle)
                        HolzShotsEnvironment.SetForegroundWindowEx(wndHandle)

                        Using ga As Graphics = Graphics.FromImage(bmpBlack)
                            ga.CompositingQuality = CompositingQuality.HighQuality
                            ga.CopyFromScreen(nrct.X, nrct.Y, 0, 0, bg.Size)
                        End Using

                        bg.BackColor = Color.White
                        bg.Refresh()

                        Using ga As Graphics = Graphics.FromImage(bmpWhite)
                            ga.CopyFromScreen(nrct.X, nrct.Y, 0, 0, bg.Size)
                        End Using

                        ScreenshotMethodsHelper.StartRedraw(wndHandle)

                        bg.Visible = False

                        Dim result As New Bitmap(bmpWhite.Width, bmpWhite.Height)

                        Common.Drawing.Computation.ComputeAlphaChannel(bmpWhite, bmpBlack, result)

                        ' Old method:
                        ' ScreenshotMethodsHelper.ComputeAlphaChannel(bmpWhite, bmpBlack)

                        Dim wndTitle = String.Empty
                        Dim procName As String = String.Empty
                        ScreenshotMethodsHelper.GetWindowInformation(wndHandle, wndTitle, procName)

                        Return New WindowScreenshotSet(result, curp, wndTitle, procName)
                    End Using
                End Using
            End Using
        End Function

        Private Shared Function DoAeroOff(wndHandle As IntPtr) As WindowScreenshotSet
            Dim rct As NativeTypes.Rect
            NativeMethods.GetWindowRect(wndHandle, rct)
            Dim nrct As Rectangle = Rectangle.FromLTRB(rct.Left, rct.Top, rct.Right, rct.Bottom)
            Dim curp As New Point(Cursor.Position.X - nrct.Location.X, Cursor.Position.Y - nrct.Location.Y)
            Dim bmp As New Bitmap(nrct.Width, nrct.Height)

            Using g As Graphics = Graphics.FromImage(bmp)
                g.Clear(Color.FromArgb(255, 13, 11, 12))
                g.CopyFromScreen(nrct.Location.X, nrct.Location.Y, 0, 0, nrct.Size)
                g.Flush()
            End Using

            Dim wndTitle = String.Empty, procName As String = String.Empty
            ScreenshotMethodsHelper.GetWindowInformation(wndHandle, wndTitle, procName)

            Return New WindowScreenshotSet(bmp, curp, wndTitle, procName)
        End Function

        Protected Shared Function CaptureCursor() As Bitmap
            Dim c As New Bitmap(MainWindow.Instance.Cursor.Size.Width, MainWindow.Instance.Cursor.Size.Height)
            Using ge As Graphics = Graphics.FromImage(c)
                ge.SmoothingMode = SmoothingMode.AntiAlias
                MainWindow.Instance.Cursor.Draw(ge, New Rectangle(0, 0, c.Width, c.Height))
            End Using
            Return c
        End Function

        Public Shared Function CaptureTaskbar() As Screenshot
            Dim taskbarHandle = NativeMethods.FindWindow("Shell_TrayWnd", String.Empty)
            Dim shot = CaptureWindow(taskbarHandle, False)
            ' shot.WindowName = "Taskleiste"
            Return shot
        End Function

    End Class
End Namespace
