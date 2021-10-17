using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using HolzShots.Net;
using HolzShots.Windows.Forms;

namespace HolzShots.Windows.Net
{
    public static class UploadHelper
    {
        public static ITransferProgressReporter GetUploadReporterForCurrentSettingsContext(HSSettings settingsContext, IWin32Window parentWindow)
        {
            var reporters = new List<ITransferProgressReporter>(4);
#if DEBUG
            reporters.Add(new ConsoleProgressReporter());
#endif
            if (settingsContext.ShowUploadProgress)
                reporters.Add(new UploadStatusFlyoutForm());

            if (parentWindow.GetHandle() != IntPtr.Zero)
                reporters.Add(new TaskbarItemProgressReporter(parentWindow.Handle));

            return new AggregateProgressReporter(reporters);
        }

        public static void InvokeUploadFinishedUI(UploadResult result, HSSettings settingsContext)
        {
            Debug.Assert(result != null);
            Debug.Assert(!string.IsNullOrWhiteSpace(result.Url));

            switch (settingsContext.ActionAfterUpload)
            {
                case UploadHandlingAction.Flyout:
                    // HolzShots.Forms.ToastNotifications.ShowUploadResult(result, settingsContext);
                    (new UploadResultForm(result, settingsContext)).Show();
                    break;
                case UploadHandlingAction.CopyToClipboard:
                    {
                        var success = ClipboardEx.SetText(result.Url);
                        if (settingsContext.ShowCopyConfirmation)
                        {
                            if (success)
                                HolzShots.Forms.ToastNotifications.ShowCopyConfirmation(result.Url);
                            else
                                HolzShots.Forms.ToastNotifications.ShowCopyingFailed(result.Url);
                        }
                    }
                    break;
                case UploadHandlingAction.None:
                    // Intentionally left empty
                    break;
                default:
                    // Intentionally left empty
                    break;
            }
        }
    }
}
