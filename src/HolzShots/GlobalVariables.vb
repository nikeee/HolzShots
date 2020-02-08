Imports System.Drawing.Imaging
Imports HolzShots.UI.Forms
Imports HolzShots.Windows.Forms

Friend Module GlobalVariables

    Friend GlobalContextMenuRenderer As ToolStripRenderer = New ToolStripAeroRenderer(ToolBarTheme.MediaToolbar)
    Friend GlobalSubMenuContextMenuRenderer As ToolStripRenderer = New VSRenderer()

    Public Const SupportedFilesFilter As String = "*.bmp;*.jpg;*.jpeg;*.png;*.gif;*.tif;*.tiff"

    Public ReadOnly DefaultImageFormat As ImageFormat = ImageFormat.Png
    Public Const DefaultFileExtension = ".png"
    Friend Const DefaultFileName = "HolzShots"

    Friend Const AreaSelectorParameter As String = "-selector" ' TODO: Refactor command line
    Friend Const FullscreenScreenshotParameter As String = "-fullscreen" ' TODO: Refactor command line

    Friend Const OpenParameter As String = "-open" ' TODO: Refactor command line
    Friend Const UploadParameter As String = "-upload" ' TODO: Refactor command line

    Friend Const OpenFromShellParameter As String = "-open_shell" ' TODO: Refactor command line
    Friend Const UploadFromShellParameter As String = "-upload_shell" ' TODO: Refactor command line
End Module
