Imports Microsoft.VisualBasic

Public Class DashboardInfo

    Private _mdashboardid As Integer
    Public Property MDashboardId() As Integer
        Get
            Return _mdashboardid
        End Get
        Set(ByVal value As Integer)
            _mdashboardid = value
        End Set
    End Property


    Private _dashboardname As String
    Public Property DashboardName() As String
        Get
            Return _dashboardname
        End Get
        Set(ByVal value As String)
            _dashboardname = value
        End Set
    End Property



    Private _dashboarddesc As String
    Public Property DashboardDesc() As String
        Get
            Return _dashboarddesc
        End Get
        Set(ByVal value As String)
            _dashboarddesc = value
        End Set
    End Property


    Private _formname As String
    Public Property FormName() As String
        Get
            Return _formname
        End Get
        Set(ByVal value As String)
            _formname = value
        End Set
    End Property


    Private _isDefault As Boolean
    Public Property IsDefault() As Boolean
        Get
            Return _isDefault
        End Get
        Set(ByVal value As Boolean)
            _isDefault = value
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
