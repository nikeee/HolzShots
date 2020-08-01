Imports System.Drawing.Imaging
Imports System.IO
Imports HolzShots
Imports HolzShots.IO
Imports HolzShots.IO.Naming

Namespace Interop.LocalDisk
    Friend Class ScreenshotDumper

        Private Shared _lastFileName As String = String.Empty

        Public Shared Sub OpenPictureDumpFolderIfEnabled()
            If UserSettings.Current.SaveImagesToLocalDisk Then
                OpenPictureDumpFolder()
            End If
        End Sub

        Public Shared Sub OpenPictureDumpFolder()
            Dim path As String = ManagedSettings.ScreenshotPath

            If String.IsNullOrWhiteSpace(path) Then
                HumanInterop.NoPathSpecified()
                Return
            End If

            If Not Directory.Exists(path) Then
                HumanInterop.PathDoesNotExist(path)
                Return
            End If

            ' Always select last saved file if available
            If Not String.IsNullOrWhiteSpace(_lastFileName) AndAlso File.Exists(_lastFileName) Then
                _lastFileName.OpenAndSelectFileInExplorer()
            Else
                path.OpenFolderInExplorer()
            End If

        End Sub

        Friend Shared Sub HandleScreenshot(shot As Screenshot)
            If Not UserSettings.Current.SaveImagesToLocalDisk OrElse Not CheckSavePath() Then Return
            SaveScreenshot(shot)
        End Sub

        Friend Shared Sub SaveScreenshot(shot As Screenshot)

            Dim format As ImageFormat = GlobalVariables.DefaultImageFormat
            Dim fileExtension As String = GlobalVariables.DefaultFileExtension

            Debug.Assert(TypeOf shot.Image Is Bitmap)
            Dim screenshotImage = If(TypeOf shot.Image Is Bitmap, DirectCast(shot.Image, Bitmap), New Bitmap(shot.Image))

            If UserSettings.Current.EnableSmartFormatForSaving AndAlso Drawing.ImageFormatAnalyser.IsOptimizable(screenshotImage) Then
                format = Drawing.ImageFormatAnalyser.GetBestFittingFormat(screenshotImage)
                fileExtension = (format.GetFormatMetadata()?.Extension)
                Debug.Assert(Not String.IsNullOrWhiteSpace(fileExtension))
            End If


            Dim pattern As String = ManagedSettings.SaveImagesPattern
            Dim name As String
            Try
                name = FileNamePatternFormatter.GetFileNameFromPattern(shot, format, pattern)
            Catch ex As PatternSyntaxException
                HumanInterop.InvalidFilePattern(pattern)
                Return
            End Try

            Dim fileName = System.IO.Path.ChangeExtension(name, fileExtension)
            Dim path As String = GetAbsolutePath(fileName)
            screenshotImage.Save(path, format)

            _lastFileName = path
        End Sub

        Public Shared Function GetDefaultSavePath() As String
            If CheckSavePath() Then
                Return ManagedSettings.ScreenshotPath
            End If
            Return String.Empty
        End Function

        Private Shared Function CheckSavePath() As Boolean
            Dim datPath = ManagedSettings.ScreenshotPath
            If Not Directory.Exists(datPath) Then
                If String.IsNullOrWhiteSpace(datPath) Then

                    Dim fallbackDirectory = HolzShotsPaths.DefaultScreenshotSavePath
                    Try
                        HolzShotsPaths.EnsureDirectory(fallbackDirectory)
                    Catch uae As UnauthorizedAccessException
                        HumanInterop.UnauthorizedAccessExceptionDirectory(fallbackDirectory)
                        Return False
                    Catch ptle As PathTooLongException
                        HumanInterop.PathIsTooLong(fallbackDirectory)
                        Return False
                    End Try
                    ManagedSettings.ScreenshotPath = fallbackDirectory
                    Return True
                End If

                Dim nPath = ManagedSettings.ScreenshotPath
                Try
                    Directory.CreateDirectory(nPath)
                Catch uae As UnauthorizedAccessException
                    HumanInterop.UnauthorizedAccessExceptionDirectory(nPath)
                    Return False
                Catch ptle As PathTooLongException
                    HumanInterop.PathIsTooLong(nPath)
                    Return False
                End Try
            End If
            Return True
        End Function

        Private Shared Function GetAbsolutePath(fileName As String) As String
            If String.IsNullOrWhiteSpace(fileName) Then Throw New ArgumentNullException(NameOf(fileName))
            Return Path.Combine(ManagedSettings.ScreenshotPath, fileName)
        End Function
    End Class
End Namespace
