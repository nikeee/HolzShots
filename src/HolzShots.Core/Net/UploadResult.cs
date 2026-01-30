
namespace HolzShots.Net;

public record UploadResult(Uploader Source, string Url, DateTime Timestamp)
{
    public override string ToString() => $"{Source}: {Url}";
}
