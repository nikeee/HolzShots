
Namespace UI.Forms
    Public Class EnvironmentEx

        Friend Shared ReadOnly Property IsVistaOrHigher As Boolean = System.Environment.OSVersion.Version.Major >= 6
        Friend Shared ReadOnly Property IsSevenOrHigher As Boolean = (System.Environment.OSVersion.Version.Major = 6 AndAlso System.Environment.OSVersion.Version.Minor >= 1) OrElse System.Environment.OSVersion.Version.Major > 6
        Friend Shared ReadOnly Property IsEightOrHigher As Boolean = (System.Environment.OSVersion.Version.Major = 6 AndAlso System.Environment.OSVersion.Version.Minor >= 2) OrElse System.Environment.OSVersion.Version.Major > 6
        Friend Shared ReadOnly Property IsAeroEnabled As Boolean
            Get
                Return IsVistaOrHigher AndAlso Native.DwmApi.DwmIsCompositionEnabled()
            End Get
        End Property
    End Class
End Namespace
