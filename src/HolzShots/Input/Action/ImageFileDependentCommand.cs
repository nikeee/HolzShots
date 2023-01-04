using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace HolzShots.Input.Actions;

public abstract class ImageFileDependentCommand
{
    private static readonly string[] AllowedExtensions = new[] { ".bmp", ".jpg", ".jpeg", ".png", ".tif", ".tiff" };

    public const string FileNameParameter = "fileName";

    protected static bool CanProcessFile(string fileName)
    {
        var ext = Path.GetExtension(fileName);
        return AllowedExtensions.Contains(ext);
    }

    protected static string? ShowFileSelector(string title)
    {
        using var ofd = new OpenFileDialog();
        ofd.Title = title;
        ofd.Filter = $"{UI.Localization.DialogFilterImages}|*.bmp;*.jpg;*.jpeg;*.png;*.gif;*.tif;*.tiff";
        ofd.Multiselect = false;
        var res = ofd.ShowDialog();
        return res == DialogResult.OK && File.Exists(ofd.FileName) ? ofd.FileName : null;
    }
}
