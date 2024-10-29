using HolzShots.Drawing.Tools.UI;

namespace HolzShots.Drawing.Tools;
public class Scale : IDialogTool
{
    public void ShowToolDialog(ref Image rawImage, Screenshot screenshot, IWin32Window? parent)
    {
        using var s = new ScaleWindow(rawImage);

        if (s.ShowDialog(parent) != DialogResult.OK)
            return;

        var width = s.WidthBoxV;
        var height = s.HeightBoxV;

        ScaleUnit unit = s.CurrentScaleUnit;

        int newWidth = (int)width;
        int newHeight = (int)height;

        // This is wrong and may fail if there was no cursor position
        // We ignore this for now, since we're not caring about legacy stuff
        var newCursorCoordinates = screenshot.CursorPosition.OnImage;


        if (unit == ScaleUnit.Percent)
        {
            newWidth = (int)(rawImage.Width * (width / 100));
            newHeight = (int)(rawImage.Height * (height / 100));
            newCursorCoordinates.X = (int)(newCursorCoordinates.X * (height / 100));
            newCursorCoordinates.Y = (int)(newCursorCoordinates.Y * (width / 100));
        }

        var newRawImage = new Bitmap(newWidth, newHeight);

        using var g = Graphics.FromImage(newRawImage);
        g.DrawImage(rawImage, 0, 0, newWidth, newHeight);

        rawImage = newRawImage;
    }

    public void LoadInitialSettings() { }
    public void PersistSettings() { }
    public void Dispose() { }
}
