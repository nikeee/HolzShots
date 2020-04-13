Namespace Interop
    Friend Class NativeTypes
        Private Sub New()
        End Sub

        Public Enum Tv As Integer
            First = &H1100
            SetExtendedStyle = First + 44
            GetExtendedStyle = First + 45
            SetAutoScrollInfo = First + 59
            NoHScroll = &H8000
            ExAutoSHcroll = &H20
            ExFaceInOutExpandOs = &H40
        End Enum

        Public Enum WindowMessage
            NcCalcSize = &H83
            NcHitTest = &H84
            DwmCompositionChanged = &H31E
            Paint = &HF
            User = &H400
        End Enum

    End Class
End Namespace
