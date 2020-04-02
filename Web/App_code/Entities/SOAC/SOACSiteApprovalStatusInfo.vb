Imports Microsoft.VisualBasic

Public Class SOACSiteApprovalStatusInfo

    Private _soacinf As New ODSOACInfo
    Public Property SOACInfo() As ODSOACInfo
        Get
            Return _soacinf
        End Get
        Set(ByVal value As ODSOACInfo)
            _soacinf = value
        End Set
    End Property


    Private _siteinf As New SiteInfo
    Public Property SiteInf() As SiteInfo
        Get
            Return _siteinf
        End Get
        Set(ByVal value As SiteInfo)
            _siteinf = value
        End Set
    End Property


    Private _SOACMilestoneInfo As New ODSOACMilestoneInfo
    Public Property SOACMilestoneInfo() As ODSOACMilestoneInfo
        Get
            Return _SOACMilestoneInfo
        End Get
        Set(ByVal value As ODSOACMilestoneInfo)
            _SOACMilestoneInfo = value
        End Set
    End Property


    Private _docinfo As New CODDocumentInfo
    Public Property DocInfo() As CODDocumentInfo
        Get
            Return _docinfo
        End Get
        Set(ByVal value As CODDocumentInfo)
            _docinfo = value
        End Set
    End Property


    Private _userlocation As String
    Public Property UserLocation() As String
        Get
            Return _userlocation
        End Get
        Set(ByVal value As String)
            _userlocation = value
        End Set
    End Property


    Private _taskdesc As String
    Public Property TaskDesc() As String
        Get
            Return _taskdesc
        End Get
        Set(ByVal value As String)
            _taskdesc = value
        End Set
    End Property


    Private _cmainfo As New CMInfo
    Public Property CMAInfo() As CMInfo
        Get
            Return _cmainfo
        End Get
        Set(ByVal value As CMInfo)
            _cmainfo = value
        End Set
    End Property


    Private _regionnme As String
    Public Property RegionName() As String
        Get
            Return _regionnme
        End Get
        Set(ByVal value As String)
            _regionnme = value
        End Set
    End Property


    Private _areaname As String
    Public Property AreaName() As String
        Get
            Return _areaname
        End Get
        Set(ByVal value As String)
            _areaname = value
        End Set
    End Property


    Private _companyname As String
    Public Property CompanyName() As String
        Get
            Return _companyname
        End Get
        Set(ByVal value As String)
            _companyname = value
        End Set
    End Property


End Class
