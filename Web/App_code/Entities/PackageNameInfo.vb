Imports Microsoft.VisualBasic

Public Class PackageNameInfo

    Private _packagenameid As Integer
    Public Property PackageNameId() As Integer
        Get
            Return _packagenameid
        End Get
        Set(ByVal value As Integer)
            _packagenameid = value
        End Set
    End Property


    Private _packagename As String
    Public Property PackageName() As String
        Get
            Return _packagename
        End Get
        Set(ByVal value As String)
            _packagename = value
        End Set
    End Property


    Private _isActive As Boolean
    Public Property IsActive() As Boolean
        Get
            Return _isActive
        End Get
        Set(ByVal value As Boolean)
            _isActive = value
        End Set
    End Property



    Private _description As String
    Public Property Description() As String
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
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


    Private _dscopeid As Integer
    Public Property DScopeId() As Integer
        Get
            Return _dscopeid
        End Get
        Set(ByVal value As Integer)
            _dscopeid = value
        End Set
    End Property


    Private _dscopename As String
    Public Property DScopeName() As String
        Get
            Return _dscopename
        End Get
        Set(ByVal value As String)
            _dscopename = value
        End Set
    End Property



    Private _mscopename As String
    Public Property MScopeName() As String
        Get
            Return _mscopename
        End Get
        Set(ByVal value As String)
            _mscopename = value
        End Set
    End Property


End Class
