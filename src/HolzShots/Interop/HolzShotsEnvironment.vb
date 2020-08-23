Imports System.Text
Imports StartupHelper

Namespace Interop
    Friend Class HolzShotsEnvironment

        Public Shared ReadOnly Property CurrentStartupManager As New StartupManager(Reflection.Assembly.GetEntryAssembly().Location,
                                                                      LibraryInformation.Name,
                                                                      RegistrationScope.Local,
                                                                      False,
                                                                      StartupProviders.Registry,
                                                                      AutorunParamter)

        Private Sub New()
        End Sub

    End Class
End Namespace
