Imports HolzShots.Composition
Imports HolzShots.Net
Imports HolzShots.UI.Dialogs
Imports Microsoft.WindowsAPICodePack.Dialogs

Namespace Interop
    ' TODO: Translate
    Friend Class HumanInterop
        Public Shared Sub UnauthorizedAccessExceptionRegistry()
            Show("Fehler :(",
                 "Zugriff auf den Registry-key wurde verweigert",
                 "Der Registrierungsschlüssel ist 'Read-Only'. HolzShots konnte sich nicht in den Autostart eintragen.",
                 TaskDialogStandardButtons.Ok, TaskDialogStandardIcon.Error)
        End Sub

        Friend Shared Sub NoAdmin()
            Show("Fehlende Rechte",
                 "Es fehlen Rechte",
                 "Es werden die Rechte des Administrators benötigt, um Änderungen am Explorer-Kontextmenü vorzunehmen.",
                 TaskDialogStandardButtons.Ok, TaskDialogStandardIcon.Error)
        End Sub

        Public Shared Sub UnauthorizedAccessExceptionDirectory(directory As String)
            Show("Fehler :(",
                 "Zugriff auf das Verzeichnis wurde verweigert",
                 "Der Zugriff auf den Ordner" & Environment.NewLine & directory & Environment.NewLine & "wurde verweigert.",
                 TaskDialogStandardButtons.Ok, TaskDialogStandardIcon.Error)
        End Sub
        Public Shared Sub PathIsTooLong(directory As String, Optional parent As IWin32Window = Nothing)
            Show("Fehler :(",
                "Der Pfad ist zu lang",
                "Der Pfad" & Environment.NewLine & directory & Environment.NewLine & "ist länger als 255 Zeichen. Wählen Sie einen kürzeren Pfad.",
                TaskDialogStandardButtons.Ok, TaskDialogStandardIcon.Error,
                parent)
        End Sub

        Friend Shared Sub PluginLoadingFailed(ex As PluginLoadingFailedException)
            Debug.Assert(ex IsNot Nothing)
            FlyoutNotifier.Notify("Plugins nicht geladen", $"Die Plugins konnten nicht geladen werden:\n{ex.InnerException.Message}")
        End Sub

        Public Shared Sub SecurityExceptionRegistry()
            Show("Fehler :(",
                 "Zugriff auf die Registry wurde verweigert",
                 "Der Zugriff auf die Windows-registry wurde verweigert. HolzShots konnte sich nicht in den Autostart eintragen.",
                 TaskDialogStandardButtons.Ok, TaskDialogStandardIcon.Error)
        End Sub
        Public Shared Sub UploadFailed(result As UploadException)
            Show("Fehler beim Hochladen", String.Empty, result.Message, TaskDialogStandardButtons.Ok, TaskDialogStandardIcon.Error)
        End Sub
        Public Shared Sub CopyingFailed(text As String)
            FlyoutNotifier.Notify("Fehler beim Kopieren", "Der Link konnte nicht in die Zwischenablage kopiert werden.")
        End Sub
        Public Shared Sub ShowCopyConfirmation(text As String)
            FlyoutNotifier.Notify("Link kopiert!", "Der Link wurde in deine Zwischenablage kopiert.")
        End Sub
        Public Shared Sub ShowOperationCanceled()
            FlyoutNotifier.Notify("Vorgang abgebrochen", "Du hast den Vorgang abgebrochen.")
        End Sub

        Public Shared Sub NoPathSpecified()
            Show("Fehler :(",
                 "Es wurde kein Pfad angegeben.",
                 "Es wurde kein Pfad angegeben, der geöffnet werden kann.",
                 TaskDialogStandardButtons.Ok, TaskDialogStandardIcon.Error)
        End Sub
        Shared Sub PathDoesNotExist(path As String)
            Show("Fehler :(",
                 "Der Pfad existiert nicht.",
                 "Der angegebene Pfad" & Environment.NewLine & path & Environment.NewLine & "existiert nicht und konnte deshalb nicht geöffnet werden.",
                 TaskDialogStandardButtons.Ok, TaskDialogStandardIcon.Error)
        End Sub
        Shared Sub InvalidFilePattern(pattern As String)
            'If Not ManagedSettings.SaveImagesToLocalDiskPolicy.IsSet Then
            Dim res = Show("Fehler :(", "Kein gültiges Benennungsmuster angegeben.",
                           "Es wurde kein gültiges Benennungsmuster für das automatische Speichern angegeben. Das Speichern des Bildes wurde abgebrochen. Geben Sie ein gültiges Muster an oder schalten Sie das automatische Abspeichern der Screenshots aus." & Environment.NewLine & Environment.NewLine & "Möchten Sie das automatische Speichern jetzt deaktivieren?",
                           TaskDialogStandardButtons.Yes Or TaskDialogStandardButtons.No, TaskDialogStandardIcon.Error)
            If res = DialogResult.Yes Then
                ManagedSettings.SaveImagesToLocalDisk = False
            End If
            'Else
            '    Show("Fehler :(", "Kein gültiges Benennungsmuster angegeben.",
            '               "Es wurde kein gültiges Benennungsmuster für das automatische Speichern angegeben. Das Speichern des Bildes wurde abgebrochen. Geben Sie ein gültiges Muster an oder schalten Sie das automatische Abspeichern der Screenshots aus." & Environment.NewLine & Environment.NewLine & "Kontaktieren Sie Ihren Systemadministrator.",
            '               MessageBoxButtons.OK, MessageBoxIcon.Error)
            'End If
        End Sub
        Shared Sub ErrorWhileOpeningSettingsDialog(ex As Exception)
            Show("Fehler :(",
                 "Beim Öffnen des Einstellungsdialoges trat ein Fehler auf.",
                 ex.Message,
                 TaskDialogStandardButtons.Ok, TaskDialogStandardIcon.Error)
        End Sub
        Shared Sub ErrorSavingImage(ex As Exception, parent As IWin32Window)
            Show("Fehler :(",
                 "Es trat ein Fehler beim Speichern des Bildes auf.",
                 ex.Message,
                 TaskDialogStandardButtons.Ok, TaskDialogStandardIcon.Error,
                 parent)
        End Sub

        Private Shared Function Show(title As String, instructionText As String, text As String, buttons As TaskDialogStandardButtons, icon As TaskDialogStandardIcon, Optional parent As IWin32Window = Nothing) As TaskDialogResult
            If Not TaskDialog.IsPlatformSupported Then
                MessageBox.Show(parent, instructionText & Environment.NewLine & text & Environment.NewLine & "HolzShots wird jetzt abstürzen, da dein Betriebssystem offenbar zu alt ist.", title)
                Throw New DivideByZeroException()
            End If

            Using diag As New TaskDialog()
                diag.Caption = title
                diag.InstructionText = instructionText
                diag.Text = text
                diag.Icon = icon
                diag.StandardButtons = buttons
                diag.OwnerWindowHandle = If(parent?.Handle, IntPtr.Zero)
                Return diag.Show()
            End Using
        End Function
    End Class
End Namespace
