
namespace HolzShots.Windows.Forms;

public static class CommandLine
{
    public const string AutorunParamter = "--autorun";

    public const string AreaSelectorCliCommand = "capture-area";
    public const string FullscreenScreenshotCliCommand = "capture-full";

    // These parameters support passing a path to an image as well as no path
    // "open-image <pathToImage>" opens an image
    // "open-image" opens a file open dialog so the user can choose a file
    // They might be part of a shell integration (which used different params before GH#36)
    public const string OpenImageCliCommand = "open-image";
    public const string UploadFileCliCommand = "upload-file";
}
