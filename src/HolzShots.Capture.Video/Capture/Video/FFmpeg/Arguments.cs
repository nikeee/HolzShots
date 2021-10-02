using FFMpegCore.Arguments;

namespace HolzShots.Capture.Video.FFmpeg
{
    record OffsetArgument(int Offset, char Dimension) : IArgument
    {
        public string Text => $"-offset_{char.ToLower(Dimension)} {Offset}";
    }

    record VideoSizeArgument(int X, int Y) : IArgument
    {
        public string Text => $"-video_size {X}x{Y}";
    }
    record ShowRegionArgument(bool Value) : IArgument
    {
        public string Text => $"-show_region {(Value ? 1 : 0)}";
    }
    /// <summary> Not supported by gdigrab mode and therefore unsued for now. </summary>
    record CaptureCursorArgument(bool Value) : IArgument
    {
        public string Text => $"-capture_cursor {(Value ? 1 : 0)}";
    }
}
