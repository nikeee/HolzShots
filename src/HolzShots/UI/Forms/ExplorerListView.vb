Imports System.Drawing
Imports System.Security.Permissions
Imports System.Windows.Forms

Namespace UI.Forms
    Public Class ExplorerListView
        Inherits ListView

        Protected Overrides ReadOnly Property CreateParams() As System.Windows.Forms.CreateParams
            <SecurityPermission(SecurityAction.LinkDemand, Flags:=SecurityPermissionFlag.UnmanagedCode)>
            Get
                Dim cp As CreateParams = MyBase.CreateParams
                If Environment.IsVistaOrHigher AndAlso Not Scrollable Then
                    cp.Style = CInt(cp.Style Or NativeTypes.Tv.NoHScroll)
                End If
                Return cp
            End Get
        End Property

        Protected Overrides Sub OnHandleCreated(ByVal e As System.EventArgs)
            MyBase.OnHandleCreated(e)
            If Environment.IsVistaOrHigher Then
                Dim hndl = NativeMethods.SendMessage(Me.Handle, DirectCast(NativeTypes.Tv.GetExtendedStyle, Integer), IntPtr.Zero, IntPtr.Zero)
                hndl = New IntPtr(hndl.ToInt32 Or NativeTypes.Tv.ExAutoSHcroll) ' Or NativeMethods.TVS_EX_FADEINOUTEXPANDOS)
                NativeMethods.SendMessage(Me.Handle, DirectCast(NativeTypes.Tv.SetExtendedStyle, Integer), IntPtr.Zero, hndl)
                NativeMethods.SetWindowTheme(Me.Handle, "explorer", 0)
            End If
        End Sub

        Private Structure EmbeddedControl
            Public ReadOnly Item As ListViewItem.ListViewSubItem
            Public ReadOnly Control As Control
            Public ReadOnly Dock As DockStyle
            Public Sub New(item As ListViewItem.ListViewSubItem, control As Control)
                Me.Item = item
                Me.Control = control
                Me.Dock = DockStyle.Fill
            End Sub
        End Structure

        Private ReadOnly _ecs As New List(Of EmbeddedControl)

        Public Sub AddControl(item As ListViewItem.ListViewSubItem, control As Control)
            If control Is Nothing OrElse control Is Nothing Then Throw New ArgumentNullException()
            InnerPadding = New Padding(2)
            _ecs.Add(New EmbeddedControl(item, control))

            Controls.Add(control)
        End Sub

        Public Property InnerPadding As Padding = New Padding(1)

        <SecurityPermission(SecurityAction.LinkDemand, Flags:=SecurityPermissionFlag.UnmanagedCode)>
        Protected Overrides Sub WndProc(ByRef m As Message)
            If View = View.Details Then
                Select Case m.Msg
                    Case NativeTypes.WindowMessages.Paint
                        For Each ec As EmbeddedControl In _ecs

                            Dim rc As Rectangle = ec.Item.Bounds

                            If ec.Dock = DockStyle.Fill Then
                                With rc
                                    .X += InnerPadding.Left
                                    .Y += InnerPadding.Top
                                    .Width -= InnerPadding.Left
                                    .Height -= InnerPadding.Top
                                    .Width -= InnerPadding.Right
                                    .Height -= InnerPadding.Bottom
                                End With
                                ec.Control.Bounds = rc
                            Else
                                ec.Control.Location = rc.Location
                            End If
                        Next
                End Select
            End If
            MyBase.WndProc(m)
        End Sub
    End Class
End Namespace
