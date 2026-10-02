using System.IO;

namespace HolzShots.Capture.Video.FFmpeg;

/// <summary> Ref: https://stackoverflow.com/a/46497896 </summary>
public static class StreamExtensions
{
    /// <summary>Copies a stream asynchronously, reporting progress.</summary>
    /// <exception cref="System.ArgumentNullException"><paramref name="source" /> or <paramref name="destination" /> is <see langword="null" />.</exception>
    /// <exception cref="System.ArgumentOutOfRangeException"><paramref name="bufferSize" /> is negative.</exception>
    /// <exception cref="System.ArgumentException"><paramref name="source" /> is not readable or <paramref name="destination" /> is not writable.</exception>
    public static async Task CopyToAsync(this Stream source, Stream destination, int bufferSize, IProgress<long>? progress = default, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(destination);
        ArgumentOutOfRangeException.ThrowIfNegative(bufferSize);
        if (!source.CanRead)
            throw new ArgumentException("Has to be readable", nameof(source));
        if (!destination.CanWrite)
            throw new ArgumentException("Has to be writable", nameof(destination));


        var buffer = new byte[bufferSize];
        long totalBytesRead = 0;
        int bytesRead;

        while ((bytesRead = await source.ReadAsync(buffer, cancellationToken).ConfigureAwait(false)) != 0)
        {
            await destination.WriteAsync(buffer.AsMemory(0, bytesRead), cancellationToken).ConfigureAwait(false);
            totalBytesRead += bytesRead;
            progress?.Report(totalBytesRead);
        }
    }
}
