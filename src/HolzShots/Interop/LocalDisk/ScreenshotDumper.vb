Imports System.Drawing.Imaging
Imports System.IO
Imports HolzShots.Drawing
Imports HolzShots.IO
Imports HolzShots.IO.Naming

Namespace Interop.LocalDisk
    Friend Class ScreenshotDumper

        Private Shared _lastFileName As String = String.Empty

        Public Shared Sub OpenPictureDumpFolder()
            Dim path As String = UserSettings.Current.ExpandedSavePath

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
            Dim extensionAndMimeType = ImageFormatInformation.GetExtensionAndMimeType(format)

            Debug.Assert(TypeOf shot.Image Is Bitmap)
            Dim screenshotImage = If(TypeOf shot.Image Is Bitmap, DirectCast(shot.Image, Bitmap), New Bitmap(shot.Image))

            If UserSettings.Current.EnableSmartFormatForSaving AndAlso ImageFormatAnalyser.IsOptimizable(screenshotImage) Then
                format = ImageFormatAnalyser.GetBestFittingFormat(screenshotImage)

                extensionAndMimeType = format.GetExtensionAndMimeType()
                Debug.Assert(Not String.IsNullOrWhiteSpace(extensionAndMimeType.FileExtension))
            End If


            Dim pattern As String = UserSettings.Current.SaveFileNamePattern
            Dim name As String
            Try
                name = FileNamePatternFormatter.GetFileNameFromPattern(shot, format, pattern)
            Catch ex As PatternSyntaxException
                HumanInterop.InvalidFilePattern(pattern)
                Return
            End Try

            Dim fileName = System.IO.Path.ChangeExtension(name, extensionAndMimeType.FileExtension)
            Dim path As String = GetAbsolutePath(fileName)
            screenshotImage.Save(path, format)

            _lastFileName = path
        End Sub

        Private Shared Function CheckSavePath() As Boolean
            Dim datPath = UserSettings.Current.ExpandedSavePath
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

        Private Shared Function GetAbsolutePath(fileName As String) As String
            If String.IsNullOrWhiteSpace(fileName) Then Throw New ArgumentNullException(NameOf(fileName))
            Return Path.Combine(UserSettings.Current.ExpandedSavePath, fileName)
        End Function
    End Class
End Namespace
