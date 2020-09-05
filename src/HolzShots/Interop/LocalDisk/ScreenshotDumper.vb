Imports System.Drawing.Imaging
Imports System.IO
Imports HolzShots.Drawing
Imports HolzShots.IO
Imports HolzShots.IO.Naming

Namespace Interop.LocalDisk
    Friend Class ScreenshotDumper

        Private Shared _lastFileName As String = String.Empty

        Public Shared Sub OpenPictureDumpFolder(settingsContext As HSSettings)
            Dim path As String = settingsContext.ExpandedSavePath

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

        Friend Shared Sub HandleScreenshot(shot As Screenshot, settingsContext As HSSettings)
            If Not settingsContext.SaveImagesToLocalDisk OrElse Not CheckSavePath(settingsContext) Then Return
            SaveScreenshot(shot, settingsContext)
        End Sub

        Friend Shared Sub SaveScreenshot(shot As Screenshot, settingsContext As HSSettings)

            Dim format As ImageFormat = GlobalVariables.DefaultImageFormat
            Dim extensionAndMimeType = ImageFormatInformation.GetExtensionAndMimeType(format)

            Debug.Assert(TypeOf shot.Image Is Bitmap)
            Dim screenshotImage = If(TypeOf shot.Image Is Bitmap, DirectCast(shot.Image, Bitmap), New Bitmap(shot.Image))

            If settingsContext.EnableSmartFormatForSaving AndAlso ImageFormatAnalyser.IsOptimizable(screenshotImage) Then
                format = ImageFormatAnalyser.GetBestFittingFormat(screenshotImage)

                extensionAndMimeType = format.GetExtensionAndMimeType()
                Debug.Assert(Not String.IsNullOrWhiteSpace(extensionAndMimeType.FileExtension))
            End If


            Dim pattern As String = settingsContext.SaveFileNamePattern
            Dim name As String
            Try
                name = FileNamePatternFormatter.GetFileNameFromPattern(shot, format, pattern)
            Catch ex As PatternSyntaxException
                HumanInterop.InvalidFilePattern(pattern)
                Return
            End Try

            Dim fileName = System.IO.Path.ChangeExtension(name, extensionAndMimeType.FileExtension)
            Dim path As String = GetAbsolutePath(fileName, settingsContext)
            screenshotImage.Save(path, format)

            _lastFileName = path
        End Sub

        Private Shared Function CheckSavePath(settingsContext As HSSettings) As Boolean
            Dim datPath = settingsContext.ExpandedSavePath
            Debug.Assert(Not String.IsNullOrEmpty(datPath))

            Try
                HolzShotsPaths.EnsureDirectory(datPath)
            Catch uae As UnauthorizedAccessException
                HumanInterop.UnauthorizedAccessExceptionDirectory(datPath)
                Return False
            Catch ptle As PathTooLongException
                HumanInterop.PathIsTooLong(datPath)
                Return False
            End Try

            Return True
        End Function

        Private Shared Function GetAbsolutePath(fileName As String, settingsContext As HSSettings) As String
            If String.IsNullOrWhiteSpace(fileName) Then Throw New ArgumentNullException(NameOf(fileName))
            Return Path.Combine(settingsContext.ExpandedSavePath, fileName)
        End Function
    End Class
End Namespace
