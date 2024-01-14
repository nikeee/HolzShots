using System.Windows.Forms;

namespace HolzShots.Input;

public static class VideoCaptureFormatSelection
{
    public static VideoCaptureFormat? PromptFormat()
    {
        var pressedButton = TaskDialog.ShowDialog(new()
        {
            Caption = "Choose a video format",
            Heading = "Which format would you like to record in?",
            Buttons = [
               new TaskDialogCommandLinkButton()
               {
                   Text = "MP4",
                   DescriptionText = "What most pages and messengers actually use when serving Gifs, because it's way smaller.",
                   Tag = VideoCaptureFormat.Mp4,
               },
               new TaskDialogCommandLinkButton()
               {
                   Text = "WebM",
                   DescriptionText = "Open video format. May produce even smaller files than MP4. Try this when MP4 didn't work for you last time.",
                   Tag = VideoCaptureFormat.Webm,
               },
               new TaskDialogCommandLinkButton()
               {
                   Text = "GIF",
                   DescriptionText = "Legacy format that produces large files. Most of the time, embeddable as an image (like PNG or JPG).",
                   Tag = VideoCaptureFormat.Gif,
               },
               TaskDialogButton.Cancel,
            ]
        });

        return pressedButton.Tag == null
            ? null // "Cancel"
            : (VideoCaptureFormat)pressedButton.Tag;
    }
}
