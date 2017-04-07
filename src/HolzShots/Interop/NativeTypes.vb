Imports System.Runtime.InteropServices

Namespace Interop
    Friend Class NativeTypes
        Private Sub New()
        End Sub

#Region "Structs"

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

            Public Shared Widening Operator CType(ByVal mrg As Margin) As Windows.Forms.Padding
                Return New Padding(mrg.cxLeftWidth, mrg.cyTopHeight, mrg.cxRightWidth, mrg.cyBottomheight)
            End Operator
            Public Shared Widening Operator CType(ByVal fwPadding As Windows.Forms.Padding) As Margin
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
            Public rc As Rect
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

#End Region
#Region "Enums"

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

#End Region

    End Class
End Namespace
