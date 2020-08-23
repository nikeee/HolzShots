Imports System.Drawing.Imaging

Friend Module GlobalVariables
    Public Const SupportedFilesFilter As String = "*.bmp;*.jpg;*.jpeg;*.png;*.gif;*.tif;*.tiff"

    Public ReadOnly DefaultImageFormat As ImageFormat = ImageFormat.Png
    Public Const DefaultFileExtension = ".png"
    Friend Const DefaultFileName = "HolzShots"
End Module
