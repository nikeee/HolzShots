Imports System.Runtime.InteropServices

Namespace Interop
    Friend Class NativeTypes
        Private Sub New()
        End Sub

        <StructLayout(LayoutKind.Sequential)>
        Public Structure Size
            Public Width As Integer
            Public Height As Integer

            Public Sub New(width As Integer, height As Integer)
                Me.Width = width
                Me.Height = height
            End Sub

            ' Implicit operators ftw
            Public Shared Widening Operator CType(ByVal sz As Size) As System.Drawing.Size
                Return New System.Drawing.Size(sz.Width, sz.Height)
            End Operator
            Public Shared Widening Operator CType(ByVal size As System.Drawing.Size) As Size
                Return New Size(size.Width, size.Height)
            End Operator
        End Structure

        <StructLayout(LayoutKind.Sequential)>
        Public Structure Margin
            Public Shared ReadOnly DefaultMargin As Margin = New Margin(-1)

            Public cxLeftWidth As Integer
            Public cxRightWidth As Integer
            Public cyTopHeight As Integer
            Public cyBottomheight As Integer
            Sub New(all As Integer)
                cxLeftWidth = all
                cxRightWidth = all
                cyTopHeight = all
                cyBottomheight = all
            End Sub
            Sub New(leftWidth As Integer, topHeight As Integer, rightWidth As Integer, bottomHeight As Integer)
                cxLeftWidth = leftWidth
                cxRightWidth = rightWidth
                cyTopHeight = topHeight
                cyBottomheight = bottomHeight
            End Sub

            Public Shared Widening Operator CType(ByVal mrg As Margin) As System.Windows.Forms.Padding
                Return New Padding(mrg.cxLeftWidth, mrg.cyTopHeight, mrg.cxRightWidth, mrg.cyBottomheight)
            End Operator
            Public Shared Widening Operator CType(ByVal fwPadding As System.Windows.Forms.Padding) As Margin
                Return New Margin(fwPadding.Left, fwPadding.Top, fwPadding.Right, fwPadding.Bottom)
            End Operator
        End Structure

        <StructLayout(LayoutKind.Sequential, Pack:=1)>
        Public Structure Blendfunction
            Public BlendOp As Byte
            Public BlendFlags As Byte
            Public SourceConstantAlpha As Byte
            Public AlphaFormat As Byte
        End Structure

        <StructLayout(LayoutKind.Sequential)>
        Public Structure AppBarData
            Public cbSize As Integer
            Public hWnd As IntPtr
            Public uCallbackMessage As Integer
            Public uEdge As TaskbarPosition
            Public rc As Native.Rect
            Public lParam As IntPtr
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

        <StructLayout(LayoutKind.Sequential)>
        Public Structure WtaOptions
            Public Flags As Wtnca
            Public Mask As Wtnca
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


        Public Enum Abm
            GetTaskBarPos = 5
        End Enum

        Public Enum TaskbarPosition
            Unknown = -1
            Left
            Top
            Right
            Bottom
        End Enum

        Public Enum ShellAddToRecentDocsFlags
            Pidl = &H1
            Path = &H2
        End Enum

        <Flags()>
        Public Enum Wtnca As UInteger
            NoDrawCaption = &H1
            NoDrawIcon = &H2
            NoSysMenu = &H4
            NoMirrorHelp = &H8
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

        Public Enum WindowMessages
            NcCalcSize = &H83
            NcHitTest = &H84
            DwmCompositionChanged = &H31E
            Paint = &HF
            User = &H400
        End Enum
        Public Enum ProgressBarMessages
            SetState = 16
        End Enum

        <Flags()>
        Public Enum DrawThemeFlags
            Composited = 8192
            Glowsize = 2048
            TextColor = 1
        End Enum

        <Flags()>
        Public Enum FlashWindowFlags As UInteger
            ''' <summary>Stop flashing. The system restores the window to its original state.</summary>
            [Stop] = 0
            ''' <summary>Flash the window caption.</summary>
            Caption = 1
            ''' <summary>Flash the taskbar button.</summary>
            Tray = 2
            ''' <summary>
            ''' Flash both the window caption and taskbar button.
            ''' This is equivalent to setting the FLASHW_CAPTION | FLASHW_TRAY flags.
            ''' </summary>
            [All] = 3
            ''' <summary>Flash continuously, until the FLASHW_STOP flag is set.</summary>
            Timer = 4
            ''' <summary>Flash continuously until the window comes to the foreground.</summary>
            TimerNoFg = 12
        End Enum

        <StructLayout(LayoutKind.Sequential)>
        Public Structure FlashWindowInfo
            Private _size As UInt32
            Private _handle As IntPtr
            Private _flags As FlashWindowFlags
            Private _count As UInt32
            Private _timeout As UInt32

            Public Sub New(ByVal handle As IntPtr)
                Me.New(handle, FlashWindowFlags.TimerNoFg Or FlashWindowFlags.Tray, UInt32.MaxValue)
            End Sub
            Public Sub New(ByVal handle As IntPtr, ByVal flags As FlashWindowFlags, ByVal count As UInteger)
                Me.New(handle, flags, count, 0)
            End Sub
            Public Sub New(ByVal handle As IntPtr, ByVal flags As FlashWindowFlags, ByVal count As UInteger, ByVal timeout As UInteger)
                Me._size = Convert.ToUInt32(Marshal.SizeOf(Me))
                Me._handle = handle
                Me._flags = flags
                Me._count = count
                Me._timeout = timeout
            End Sub

            Sub Flash()
                NativeMethods.FlashWindowEx(Me)
            End Sub

        End Structure

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
