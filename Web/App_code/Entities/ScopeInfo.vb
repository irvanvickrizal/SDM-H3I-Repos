Imports Microsoft.VisualBasic

Public Class ScopeInfo

    Private _scopeid As Integer
    Public Property ScopeId() As Integer
        Get
            Return _scopeid
        End Get
        Set(ByVal value As Integer)
            _scopeid = value
        End Set
    End Property


    Private _scopename As String
    Public Property ScopeName() As String
        Get
            Return _scopename
        End Get
        Set(ByVal value As String)
            _scopename = value
        End Set
    End Property


    Private _scopedesc As String
    Public Property ScopeDesc() As String
        Get
            Return _scopedesc
        End Get
        Set(ByVal value As String)
            _scopedesc = value
        End Set
    End Property


    Private _scopeLMBY As String
    Public Property ScopeLMBY() As String
        Get
            Return _scopeLMBY
        End Get
        Set(ByVal value As String)
            _scopeLMBY = value
        End Set
    End Property


    Private _scopeLMDT As DateTime
    Public Property ScopeLMDT() As DateTime
        Get
            Return _scopeLMDT
        End Get
        Set(ByVal value As DateTime)
            _scopeLMDT = value
        End Set
    End Property

End Class
