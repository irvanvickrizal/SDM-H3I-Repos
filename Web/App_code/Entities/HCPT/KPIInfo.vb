Imports Microsoft.VisualBasic

Public Class KPIInfo

    Private _KPIID As Int32
    Public Property KPIID() As Int32
        Get
            Return _KPIID
        End Get
        Set(ByVal value As Int32)
            _KPIID = value
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


    Private _btstype As String
    Public Property BTSType() As String
        Get
            Return _btstype
        End Get
        Set(ByVal value As String)
            _btstype = value
        End Set
    End Property

    Private _longitude As String
    Public Property Longitude() As String
        Get
            Return _longitude
        End Get
        Set(ByVal value As String)
            _longitude = value
        End Set
    End Property

    Private _latitude As String
    Public Property Latitude() As String
        Get
            Return _latitude
        End Get
        Set(ByVal value As String)
            _latitude = value
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



End Class
