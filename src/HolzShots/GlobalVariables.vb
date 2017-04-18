Imports System.Drawing.Imaging
Imports HolzShots.UI.Windows.Forms

Friend Module GlobalVariables

    Friend GlobalContextMenuRenderer As ToolStripRenderer = New ToolStripAeroRenderer(ToolBarTheme.MediaToolbar)
    Friend GlobalSubMenuContextMenuRenderer As ToolStripRenderer = New VSRenderer()

    Public Const SupportedFilesFilter As String = "*.bmp;*.jpg;*.jpeg;*.png;*.gif;*.tif;*.tiff"

    Public ReadOnly DefaultImageFormat As ImageFormat = ImageFormat.Png
    Public Const DefaultFileExtension = ".png"
    Friend Const DefaultFileName = "HolzShots"

    Friend Const AreaSelectorParameter As String = "-selector" ' TODO: refacotr command line
    Friend Const TaskbarScreenshotParameter As String = "-taskbar" ' TODO: refacotr command line
    Friend Const FullscreenScreenshotParameter As String = "-fullscreen" ' TODO: refacotr command line

    Friend Const OpenParameter As String = "-open" ' TODO: refacotr command line
    Friend Const UploadParameter As String = "-upload" ' TODO: refacotr command line

    Friend Const OpenFromShellParameter As String = "-open_shell" ' TODO: refacotr command line
    Friend Const UploadFromShellParameter As String = "-upload_shell" ' TODO: refacotr command line
End Module
