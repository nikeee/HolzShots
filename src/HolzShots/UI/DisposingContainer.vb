Imports System.Collections.ObjectModel
Imports System.ComponentModel

Public Class DisposingContainer
    Inherits Collection(Of IDisposable)
    Implements IDisposable
    Implements IComponent

    Public Property Site As ISite Implements IComponent.Site
    Public Event Disposed As EventHandler Implements IComponent.Disposed

#Region "IDisposable Support"
    Private disposedValue As Boolean

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                For Each c In Me
                    c?.Dispose()
                Next
            End If
        End If
        disposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
