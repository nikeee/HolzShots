using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using HolzShots.Composition;

namespace HolzShots.Net.Custom;

public class CustomUploaderSpecUpdater
{
    public static async Task<UploaderSpecUpdateResult> FetchUpdates(IReadOnlyList<(string filePath, CustomUploaderSpec spec)> uploaders, CancellationToken cancellationToken = default)
    {
        int noUpdateUrl = 0;
        int emptyResponse = 0;
        int invalidResponse = 0;
        int noUpdateAvailable = 0;
        var availableUpdates = new List<SpecUpdate>();
        var errors = new List<Exception>();

        using var client = new HttpClient();

        foreach (var uploader in uploaders)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var (filePath, spec) = uploader;
            var updateUrl = spec.Meta.UpdateUrl;
            if (updateUrl == null)
            {
                ++noUpdateUrl;
                continue;
            }

            // TODO: Maybe add a user-agent, so the server knows it's HS requesting
            string newSpecCandidate;
            try
            {
                newSpecCandidate = await client.GetStringAsync(updateUrl, cancellationToken);
                if (string.IsNullOrWhiteSpace(newSpecCandidate))
                {
                    ++emptyResponse;
                    continue;
                }

            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                errors.Add(ex);
                continue;
            }

            if (CustomUploader.TryParse(newSpecCandidate, null, out var parsedUploader))
            {
                var newSpec = parsedUploader.UploaderInfo;
                if (newSpec is not null)
                {
                    if (newSpec.Meta.Version.ComparePrecedenceTo(spec.Meta.Version) > 0)
                    {
                        availableUpdates.Add(new SpecUpdate(filePath, spec, newSpec, newSpecCandidate));
                    }
                    else
                    {
                        noUpdateAvailable++;
                    }
                }
            }
            else
            {
                ++invalidResponse;
            }
        }

        return new UploaderSpecUpdateResult(noUpdateUrl, emptyResponse, invalidResponse, noUpdateAvailable, availableUpdates, errors);
    }

    public static Task ApplyUpdate(SpecUpdate update, CancellationToken cancellationToken)
    {
        return File.WriteAllTextAsync(update.JsonFilePath, update.NewContents, cancellationToken);
    }

    public static async Task ApplyUpdates(CustomUploaderSource uploaderSpecSource, IReadOnlyList<SpecUpdate> updates, CancellationToken cancellationToken)
    {
        foreach (var update in updates)
            await ApplyUpdate(update, cancellationToken);

        await uploaderSpecSource.Load();
    }
}

public record UploaderSpecUpdateResult(
    int NoUpdateUrl,
    int EmptyResponse,
    int InvalidResponse,
    int NoUpdateAvailable,
    IReadOnlyList<SpecUpdate> AvailableUpdates,
    IReadOnlyList<Exception> Errors
);
public record SpecUpdate(
    string JsonFilePath,
    CustomUploaderSpec OldSpec,
    CustomUploaderSpec NewSpec,
    string NewContents
);
