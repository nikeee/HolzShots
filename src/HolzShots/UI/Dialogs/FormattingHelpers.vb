Namespace UI.Dialogs
    Friend Class FormattingHelpers
        Public Shared Function ShortenUrlForDisplay(derLink As String, Optional maxLength As Integer = 26) As String
            Return If(derLink.Length > maxLength + 1, derLink.Remove(maxLength) & "...", derLink)
        End Function

    End Class
End Namespace
