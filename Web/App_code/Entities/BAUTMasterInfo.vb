Imports Microsoft.VisualBasic

Public Class BAUTMasterInfo


    Private _siteid As Int32
    Public Property SiteId() As Int32
        Get
            Return _siteid
        End Get
        Set(ByVal value As Int32)
            _siteid = value
        End Set
    End Property


    Private _siteversion As Integer
    Public Property SiteVersion() As Integer
        Get
            Return _siteversion
        End Get
        Set(ByVal value As Integer)
            _siteversion = value
        End Set
    End Property


    Private _packageid As String
    Public Property PackageId() As String
        Get
            Return _packageid
        End Get
        Set(ByVal value As String)
            _packageid = value
        End Set
    End Property


    Private _pstatus As Boolean
    Public Property PStatus() As Boolean
        Get
            Return _pstatus
        End Get
        Set(ByVal value As Boolean)
            _pstatus = value
        End Set
    End Property


    Private _BAUTApprovedDate As DateTime
    Public Property BAUTApprovedDate() As DateTime
        Get
            Return _BAUTApprovedDate
        End Get
        Set(ByVal value As DateTime)
            _BAUTApprovedDate = value
        End Set
    End Property


    Private _onairdatebaut As Nullable(Of DateTime)
    Public Property OnAirDateBAUT() As Nullable(Of DateTime)
        Get
            Return _onairdatebaut
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            _onairdatebaut = value
        End Set
    End Property


    Private _kpidatebaut As Nullable(Of DateTime)
    Public Property KPIDateBAUT() As Nullable(Of DateTime)
        Get
            Return _kpidatebaut
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            _kpidatebaut = value
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

End Class
