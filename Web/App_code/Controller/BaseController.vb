Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration

Public Class BaseController
    Implements IDisposable

#Region "fields"
    Private disposedValue As Boolean = False        ' To detect redundant calls
    Private _connection As SqlConnection
    Private _command As SqlCommand
    Private _dataReader As SqlDataReader
#End Region

    Public Sub New()
        _connection = New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("conn").ToString())
    End Sub


#Region "Property"
    Public Property Connection() As SqlConnection
        Get
            Return _connection
        End Get
        Set(ByVal value As SqlConnection)
            _connection = value
        End Set
    End Property

    Public Property Command() As SqlCommand
        Get
            Return _command
        End Get
        Set(ByVal value As SqlCommand)
            _command = value
        End Set
    End Property

    Public Property DataReader() As SqlDataReader
        Get
            Return _dataReader
        End Get
        Set(ByVal value As SqlDataReader)
            _dataReader = value
        End Set
    End Property

#End Region

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                Connection().Dispose()
            End If
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
