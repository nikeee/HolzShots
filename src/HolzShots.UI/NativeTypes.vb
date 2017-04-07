Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

Namespace NativeTypes

    Public Enum Tv As Integer
        First = &H1100
        SetExtendedStyle = First + 44
        GetExtendedStyle = First + 45
        SetAutoScrollInfo = First + 59
        NoHScroll = &H8000
        ExAutoSHcroll = &H20
        ExFaceInOutExpandOs = &H40
    End Enum

    <StructLayout(LayoutKind.Sequential)>
    Public Structure Rect
        Public Left As Integer
        Public Top As Integer
        Public Right As Integer
        Public Bottom As Integer

        Public Shared Function ToRectangle(ByVal rct As Rect) As Rectangle
            Return Rectangle.FromLTRB(rct.Left, rct.Top, rct.Right, rct.Bottom)
        End Function

        Public Sub New(ByVal left As Integer, ByVal top As Integer, ByVal right As Integer, ByVal bottom As Integer)
            Me.Left = left
            Me.Top = top
            Me.Right = right
            Me.Bottom = bottom
        End Sub

        ' Implicit operators ftw
        Public Shared Widening Operator CType(ByVal rct As Rect) As Rectangle
            Return Rectangle.FromLTRB(rct.Left, rct.Top, rct.Right, rct.Bottom)
        End Operator
        Public Shared Widening Operator CType(ByVal rectangle As Rectangle) As Rect
            Return New Rect(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom)
        End Operator
    End Structure

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
    Public Enum WTNCA As UInteger
        NoDrawCaption = &H1
        NoDrawIcon = &H2
        NoSysMenu = &H4
        NoMirrorHelp = &H8
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

        Public Shared Widening Operator CType(ByVal mrg As Margin) As Padding
            Return New Padding(mrg.cxLeftWidth, mrg.cyTopHeight, mrg.cxRightWidth, mrg.cyBottomheight)
        End Operator
        Public Shared Widening Operator CType(ByVal fwPadding As Padding) As Margin
            Return New Margin(fwPadding.Left, fwPadding.Top, fwPadding.Right, fwPadding.Bottom)
        End Operator
    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Public Structure WtaOptions
        Public Flags As WTNCA
        Public Mask As WTNCA
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
End Namespace
