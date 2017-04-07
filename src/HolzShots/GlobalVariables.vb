Imports System.Drawing.Imaging
Imports HolzShots.UI.Windows.Forms

Friend Module GlobalVariables

    Friend GlobalContextMenuRenderer As ToolStripRenderer = New ToolStripAeroRenderer(ToolBarTheme.MediaToolbar)
    Friend GlobalSubMenuContextMenuRenderer As ToolStripRenderer = New VSRenderer()

    Public Const SupportedFilesFilter As String = "*.bmp;*.jpg;*.jpeg;*.png;*.gif;*.tif;*.tiff"

    Public ReadOnly DefaultImageFormat As ImageFormat = ImageFormat.Png
    Public Const DefaultFileExtension = ".png"
    Friend Const DefaultFileName = "HolzShots"

    Friend Const AreaSelectorParameter As String = "-selector"
    Friend Const TaskbarScreenshotParameter As String = "-taskbar"
    Friend Const FullscreenScreenshotParameter As String = "-fullscreen"

    Friend Const OpenParameter As String = "-open"
    Friend Const UploadParameter As String = "-upload"

    Friend Const OpenFromShellParameter As String = "-open_shell"
    Friend Const UploadFromShellParameter As String = "-upload_shell"

    Friend Const HelpLink As String = "http://forum.vb-paradise.de/HS/39990-2/" '"http://holzshots.net"
    Friend Const AboutLink As String = HelpLink
End Module
