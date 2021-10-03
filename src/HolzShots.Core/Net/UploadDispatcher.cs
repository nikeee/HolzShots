using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using HolzShots.Composition;

namespace HolzShots.Net
{
    public static class UploadDispatcher
    {
        // TODO: maybe refactor this, so the payload gets created somewhere else
        /// <summary> Catch the UploadException! </summary>
        public static async Task<UploadResult> InitiateUploadToDefaultUploader(Image image, HSSettings settingsContext, UploaderManager uploaderManager, ImageFormat? format, ITransferProgressReporter? progressReporter)
        {
            Debug.Assert(image != null);
            Debug.Assert(settingsContext != null);
            Debug.Assert(uploaderManager != null);

            format ??= GetImageFormat(image, settingsContext);
            using var payload = new ImageUploadPayload(image, format);
            return await InitiateUploadToDefaultUploader(payload, settingsContext, uploaderManager, progressReporter);
        }
        public static Task<UploadResult> InitiateUploadToDefaultUploader(IUploadPayload payload, HSSettings settingsContext, UploaderManager uploaderManager, ITransferProgressReporter? progressReporter)
        {
            Debug.Assert(payload != null);
            Debug.Assert(settingsContext != null);
            Debug.Assert(uploaderManager != null);

            var service = GetUploadServiceForSettingsContext(settingsContext, uploaderManager);
            Debug.Assert(!(service is null));
            Debug.Assert(service.Metadata != null);
            Debug.Assert(service.Uploader != null);

            if (service?.Metadata == null || service?.Uploader == null)
                throw new UploadException("Unable to find an uploader for the current settings context");

            return InitiateUpload(payload, settingsContext, service.Uploader, progressReporter);
        }


        // TODO: maybe refactor this, so the payload gets created somewhere else
        /// <summary> Catch the UploadException! </summary>
        public static async Task<UploadResult> InitiateUpload(Image image, HSSettings settingsContext, Uploader uploader, ImageFormat? format, ITransferProgressReporter? progressReporter)
        {
            format ??= GetImageFormat(image, settingsContext);
            using var payload = new ImageUploadPayload(image, format);
            return await InitiateUpload(payload, settingsContext, uploader, progressReporter);
        }

        public static async Task<UploadResult> InitiateUpload(IUploadPayload payload, HSSettings settingsContext, Uploader uploader, ITransferProgressReporter? progressReporter)
        {
            if (payload == null)
                throw new ArgumentNullException(nameof(payload));
            if (settingsContext == null)
                throw new ArgumentNullException(nameof(settingsContext));
            if (uploader == null)
                throw new ArgumentNullException(nameof(uploader));

            try
            {

                var sc = uploader.GetSupportedSettingsInvocations();
                if ((sc & SettingsInvocationContexts.OnUse) == SettingsInvocationContexts.OnUse)
                {
                    // A third-party settings dialog may throw an exception
                    // If it does, we abort the upload
                    await uploader.InvokeSettingsAsync(SettingsInvocationContexts.OnUse).ConfigureAwait(true);
                }

                using var ui = new UploadUI(payload, uploader, progressReporter);
                try
                {
                    ui.ShowUI();

                    return await ui.InvokeUploadAsync().ConfigureAwait(true);
                }
                finally
                {
                    ui.HideUI();
                }
            }
            catch (OperationCanceledException c)
            {
                throw new UploadCanceledException(c);
            }
            catch (UploadException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UploadException(ex.Message, ex);
            }
        }

        public static UploaderEntry? GetUploadServiceForSettingsContext(HSSettings context, UploaderManager uploaderManager)
        {
            Debug.Assert(context != null);
            Debug.Assert(uploaderManager != null);
            Debug.Assert(uploaderManager.Loaded);

            return uploaderManager.GetUploaderByName(context.TargetImageHoster);

        }

        private static ImageFormat GetImageFormat(Image image, HSSettings settingsContext)
        {
            Debug.Assert(image != null);
            Debug.Assert(settingsContext != null);

            if (!settingsContext.EnableSmartFormatForUpload)
                return ImageFormat.Png;

            if (!Drawing.ImageFormatAnalyser.IsOptimizable(image))
                return ImageFormat.Png;

            try
            {
                var bmp = image is Bitmap b ? b : new Bitmap(image);

                return Drawing.ImageFormatAnalyser.GetBestFittingFormat(bmp); // Experimental?
            }
            catch (Exception e)
            {
                Debug.Fail(e.Message);
                return ImageFormat.Png;
            }
        }
    }
}
