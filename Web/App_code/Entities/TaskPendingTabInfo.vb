Imports Microsoft.VisualBasic

Public Class TaskPendingTabInfo

    Private _tabid As Integer
    Public Property TabId() As Integer
        Get
            Return _tabid
        End Get
        Set(ByVal value As Integer)
            _tabid = value
        End Set
    End Property


    Private _tabname As String
    Public Property TabName() As String
        Get
            Return _tabname
        End Get
        Set(ByVal value As String)
            _tabname = value
        End Set
    End Property


    Private _tabDesc As String
    Public Property tabDesc() As String
        Get
            Return _tabDesc
        End Get
        Set(ByVal value As String)
            _tabDesc = value
        End Set
    End Property


    Private _lmby As String
    Public Property LMBY() As String
        Get
            Return _lmby
        End Get
        Set(ByVal value As String)
            _lmby = value
        End Set
    End Property


    Private _lmdt As DateTime
    Public Property LMDT() As DateTime
        Get
            Return _lmdt
        End Get
        Set(ByVal value As DateTime)
            _lmdt = value
        End Set
    End Property



End Class
