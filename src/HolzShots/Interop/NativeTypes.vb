Imports System.Runtime.InteropServices

Namespace Interop
    Friend Class NativeTypes
        Private Sub New()
        End Sub

        <StructLayout(LayoutKind.Sequential, Pack:=1)>
        Public Structure Blendfunction
            Public BlendOp As Byte
            Public BlendFlags As Byte
            Public SourceConstantAlpha As Byte
            Public AlphaFormat As Byte
        End Structure


        <StructLayout(LayoutKind.Sequential)>
        Public Structure WindowPlacement
            Public length As Integer
            Public flags As Integer
            Public showCmd As Integer
            Public minPosition As Point
            Public maxPosition As Point
            Public normalPosition As Rectangle
        End Structure

        Public Enum Tv As Integer
            First = &H1100
            SetExtendedStyle = First + 44
            GetExtendedStyle = First + 45
            SetAutoScrollInfo = First + 59
            NoHScroll = &H8000
            ExAutoSHcroll = &H20
            ExFaceInOutExpandOs = &H40
        End Enum

        <Flags()>
        Public Enum ButtonStyle
            CommandLink = &HE
        End Enum

        Enum Fos
            PickFolders = &H20
            ForceFileSystem = &H40
            NoValidate = &H100
            NoTestFileCreate = &H10000
            DontAddToRecent = &H2000000
        End Enum

        ''' <summary>
        '''     Specifies a raster-operation code. These codes define how the color data for the
        '''     source rectangle is to be combined with the color data for the destination
        '''     rectangle to achieve the final color.
        ''' </summary>
        Enum TernaryRasterOperations As UInteger
            ''' <summary>dest = source</summary>
            SrcCopy = &HCC0020
            ''' <summary>dest = source OR dest</summary>
            SrcPaint = &HEE0086
            ''' <summary>dest = source AND dest</summary>
            SrcAnd = &H8800C6
            ''' <summary>dest = source XOR dest</summary>
            SrcInvert = &H660046
            ''' <summary>dest = source AND (NOT dest)</summary>
            SrcErase = &H440328
            ''' <summary>dest = (NOT source)</summary>
            NotSrcCopy = &H330008
            ''' <summary>dest = (NOT Src) AND (NOT dest)</summary>
            NotSrcErase = &H1100A6
            ''' <summary>dest = (source AND pattern)</summary>
            MergeCopy = &HC000CA
            ''' <summary>dest = (NOT source) OR dest</summary>
            MergePaint = &HBB0226
            ''' <summary>dest = pattern</summary>
            PatCopy = &HF00021
            ''' <summary>dest = DPSnoo</summary>
            PatPaint = &HFB0A09
            ''' <summary>dest = pattern XOR dest</summary>
            PatInvert = &H5A0049
            ''' <summary>dest = (NOT dest)</summary>
            DstInvert = &H550009
            ''' <summary>dest = BLACK</summary>
            Blackness = &H42
            ''' <summary>dest = WHITE</summary>
            Whiteness = &HFF0062
            ''' <summary>
            ''' Capture window as seen on screen.  This includes layered windows
            ''' such as WPF windows with AllowsTransparency="true"
            ''' </summary>
            CaptureBlt = &H40000000
        End Enum

        Public Enum WindowMessage
            NcCalcSize = &H83
            NcHitTest = &H84
            DwmCompositionChanged = &H31E
            Paint = &HF
            User = &H400
        End Enum
        Public Enum ProgressBarMessage
            SetState = 16
        End Enum

        <Flags()>
        Public Enum DrawThemeFlags
            Composited = 8192
            Glowsize = 2048
            TextColor = 1
        End Enum

        <StructLayout(LayoutKind.Sequential)>
        Public Structure DwmColorizationParams
            Public Color1 As Integer
            Public Color2 As Integer
            Public Brightness As Integer
            Public Saturation As Integer
            Public Intensity As Integer
            Private ReadOnly Dunno1 As Integer
            Private ReadOnly Dunno2 As Integer
        End Structure

        <StructLayout(LayoutKind.Sequential)>
        Friend Class BitmapInfo
            Public biSize As Integer
            Public biWidth As Integer
            Public biHeight As Integer
            Public biPlanes As Short
            Public biBitCount As Short
            Public biCompression As Integer
            Public biSizeImage As Integer
            Public biXPelsPerMeter As Integer
            Public biYPelsPerMeter As Integer
            Public biClrUsed As Integer
            Public biClrImportant As Integer
            Public bmiColors_rgbBlue As Byte
            Public bmiColors_rgbGreen As Byte
            Public bmiColors_rgbRed As Byte
            Public bmiColors_rgbReserved As Byte
        End Class

    End Class
End Namespace
