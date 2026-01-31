using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HolzShots.Composition.Command;
using HolzShots.Net.Custom;

namespace HolzShots.Input.Actions;

[Command("updateUploaderSpecs")]
public class UpdateUploaderSpecsCommand : ICommand<HSSettings>
{
    public async Task Invoke(IReadOnlyDictionary<string, string> parameters, HSSettings settingsContext)
    {
        ArgumentNullException.ThrowIfNull(parameters);
        ArgumentNullException.ThrowIfNull(settingsContext);

        var cts = new CancellationTokenSource();

        var customUploaderManager = HolzShotsApplication.Instance.Uploaders.Customs;
        var customUploaderSpecs = customUploaderManager.GetCustomUploaderSpecs();
        try
        {
            var updateResult = await CustomUploaderSpecUpdater.FetchUpdates(customUploaderSpecs, cts.Token);

            // TODO: Maybe display errors somewhere

            if (updateResult.AvailableUpdates.Count > 0)
            {
                var updateOffer = GetUpdateOfferPage(updateResult);
                var decision = TaskDialog.ShowDialog(updateOffer, TaskDialogStartupLocation.CenterScreen);
                if (decision == TaskDialogButton.Yes)
                {
                    await CustomUploaderSpecUpdater.ApplyUpdates(customUploaderManager, updateResult.AvailableUpdates, cts.Token);
                }
            }
            else
            {
                TaskDialog.ShowDialog(GetNoUpdatesAvailablePage(updateResult), TaskDialogStartupLocation.CenterScreen);
            }
        }
        catch (OperationCanceledException)
        {
            // Nothing to do in this case
            return;
        }
        catch (Exception ex)
        {
            // TODO: This can be made prettier
            MessageBox.Show("There was an error while checking for updates. Try again later.\n\nDetails on the error:\n\n" + ex.Message, "Sorry. :(");
        }
    }

    private static TaskDialogPage GetUpdateOfferPage(UploaderSpecUpdateResult updateResult) => new()
    {
        Caption = "Updates Available",
        Icon = TaskDialogIcon.Information,
        Heading = updateResult.AvailableUpdates.Count == 1
            ? "There is an update for one of your custom uploaders."
            : $"There are {updateResult.AvailableUpdates.Count} updates for your custom uploaders.",
        Text = "Do you want to apply these updates?",
        Expander = new TaskDialogExpander()
        {
            Text = "These uploader specs will be updated:\n" + string.Join("\n", updateResult.AvailableUpdates.Select(update =>
            {
                var nameChanged = update.NewSpec.Meta.Name != update.OldSpec.Meta.Name;
                var newName = nameChanged ? update.NewSpec.Meta.Name + " " : string.Empty;
                return $"- {update.OldSpec.Meta.Name}: {update.OldSpec.Meta.Version} -> {newName}{update.NewSpec.Meta.Version}";
            })),
        },
        Buttons = [
            TaskDialogButton.No,
            TaskDialogButton.Yes,
        ],
        DefaultButton = TaskDialogButton.No,
    };

    private static TaskDialogPage GetNoUpdatesAvailablePage(UploaderSpecUpdateResult res) => new()
    {
        Caption = "No Updates Available",
        Icon = TaskDialogIcon.Information,
        Heading = "You're all set!",
        Text = "There are not an updates for your custom uploaders.",
        Expander = new TaskDialogExpander()
        {
            Text = $"{res.NoUpdateUrl} had no update URL, {res.EmptyResponse + res.InvalidResponse} replied with an invalid response, {res.Errors.Count} had an error and {res.NoUpdateAvailable} are up-to-date.",
        },
        Buttons = [
            TaskDialogButton.OK,
        ],
        DefaultButton = TaskDialogButton.OK,
    };
}
