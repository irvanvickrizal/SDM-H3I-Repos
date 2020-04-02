Imports Microsoft.VisualBasic

Public Class TaskInfo
    Private _tskid As Integer
    Public Property TskId() As Integer
        Get
            Return _tskid
        End Get
        Set(ByVal value As Integer)
            _tskid = value
        End Set
    End Property

    Private _tskname As String
    Public Property TskName() As String
        Get
            Return _tskname
        End Get
        Set(ByVal value As String)
            _tskname = value
        End Set
    End Property

End Class
