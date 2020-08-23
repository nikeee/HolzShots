
namespace HolzShots.Windows.Forms
{
    public static class CommandLine
    {
        public const string AutorunParamter = "--autorun";

        public const string AreaSelectorParameter = "--capture-area";
        public const string FullscreenScreenshotParameter = "--capture-full";

        // These parameters support passing a path to an image as well as no path
        // "--open-image <pathToImage>" opens an image
        // "--open-image" opens a file open dialog so the user can choose a file
        // They might be part of a shell integration (which used different params before GH#36)
        public const string OpenParameter = "--open-image";
        public const string UploadParameter = "--upload-image";
    }
}
