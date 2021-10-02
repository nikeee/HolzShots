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
    /// <summary> Only supported in gdigrab mode (needs different option for other capture modes) </summary>
    record DrawMouseArgument(bool Value) : IArgument
    {
        public string Text => $"-draw_mouse {(Value ? 1 : 0)}";
    }
}
