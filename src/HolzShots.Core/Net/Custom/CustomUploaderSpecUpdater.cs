using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HolzShots.Net.Custom
{
    public class CustomUploaderSpecUpdater
    {
        public static async Task<UploaderSpecUpdateResult> FetchUpdates(IReadOnlyList<(string filePath, CustomUploaderSpec spec)> uploaders, CancellationToken cancellationToken)
        {
            int noUpdateUrl = 0;
            int emptyResponse = 0;
            int invalidResponse = 0;
            var availableUpdates = new List<SpecUpdate>();

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

                using var client = new HttpClient();
                var newSpecCandidate = await client.GetStringAsync(updateUrl, cancellationToken);
                if (newSpecCandidate == null)
                {
                    ++emptyResponse;
                    continue;
                }

                if (CustomUploader.TryParse(newSpecCandidate, out var parsedSpec))
                {
                    availableUpdates.Add(new SpecUpdate(filePath, spec, parsedSpec.UploaderInfo, newSpecCandidate));
                }
                else
                {
                    ++invalidResponse;
                }
            }

            return new UploaderSpecUpdateResult(noUpdateUrl, emptyResponse, invalidResponse, availableUpdates);
        }

        public async Task ApplyUpdate(SpecUpdate update)
        {
            // TODO
        }
    }

    public record UploaderSpecUpdateResult(
        int NoUpdateUrl,
        int EmptyResponse,
        int InvalidResponse,
        IReadOnlyList<SpecUpdate> AvailableUpdates
    );
    public record SpecUpdate(
        string JsonFilePath,
        CustomUploaderSpec OldSpec,
        CustomUploaderSpec NewSpec,
        string NewContents
    );
}
