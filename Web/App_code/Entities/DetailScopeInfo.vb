Imports Microsoft.VisualBasic
Imports System.Text

Public Class DetailScopeInfo
    Inherits ScopeInfo

    Private _dscopeid As Integer
    Public Property DScopeId() As Integer
        Get
            Return _dscopeid
        End Get
        Set(ByVal value As Integer)
            _dscopeid = value
        End Set
    End Property


    Private _dscopeName As String
    Public Property DScopeName() As String
        Get
            Return _dscopeName
        End Get
        Set(ByVal value As String)
            _dscopeName = value
        End Set
    End Property

    Private _dscopedesc As String
    Public Property DScopeDesc() As String
        Get
            Return _dscopedesc
        End Get
        Set(ByVal value As String)
            _dscopedesc = value
        End Set
    End Property


    Private _dscopeLMBY As String
    Public Property DScopeLMBY() As String
        Get
            Return _dscopeLMBY
        End Get
        Set(ByVal value As String)
            _dscopeLMBY = value
        End Set
    End Property


    Private _dscopeLMDT As DateTime
    Public Property DScopeLMDT() As DateTime
        Get
            Return _dscopeLMDT
        End Get
        Set(ByVal value As DateTime)
            _dscopeLMDT = value
        End Set
    End Property

   


    Private _BAUTSITInfo As CODScopeBAUTSITInfo
    Public Property ScopeBAUTSITInfo() As CODScopeBAUTSITInfo
        Get
            Return _BAUTSITInfo
        End Get
        Set(ByVal value As CODScopeBAUTSITInfo)
            _BAUTSITInfo = value
        End Set
    End Property


    Private _LTInfo As New CODScopeLTPartnerInfo
    Public Property ScopeLTPartnerInfo() As CODScopeLTPartnerInfo
        Get
            Return _LTInfo
        End Get
        Set(ByVal value As CODScopeLTPartnerInfo)
            _LTInfo = value
        End Set
    End Property

   


End Class
