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

    Friend Const AutorunParamter As String = "--autorun"

    Friend Const AreaSelectorParameter As String = "--capture-area"
    Friend Const FullscreenScreenshotParameter As String = "--capture-full"

    ' These parameters support passing a path to an image as well as no path
    ' "--open-image <pathToImage>" opens an image
    ' "--open-image" opens a file open dialog so the user can choose a file
    ' They might be part of a shell integration (which used different params before GH#36)
    Friend Const OpenParameter As String = "--open-image"
    Friend Const UploadParameter As String = "--upload-image"

End Module
