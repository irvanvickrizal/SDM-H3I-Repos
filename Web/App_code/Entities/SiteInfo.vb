Imports Microsoft.VisualBasic

Public Class SiteInfo


    Private _siteid As Int32
    Public Property SiteID() As Int32
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



    Private _siteno As String
    Public Property SiteNo() As String
        Get
            Return _siteno
        End Get
        Set(ByVal value As String)
            _siteno = value
        End Set
    End Property

    Private _sitename As String
    Public Property SiteName() As String
        Get
            Return _sitename
        End Get
        Set(ByVal value As String)
            _sitename = value
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


    Private _pono As String
    Public Property PONO() As String
        Get
            Return _pono
        End Get
        Set(ByVal value As String)
            _pono = value
        End Set
    End Property


    Private _poname As String
    Public Property POName() As String
        Get
            Return _poname
        End Get
        Set(ByVal value As String)
            _poname = value
        End Set
    End Property


    Private _workpackageName As String
    Public Property WorkpackageName() As String
        Get
            Return _workpackageName
        End Get
        Set(ByVal value As String)
            _workpackageName = value
        End Set
    End Property


    Private _siteidpo As String
    Public Property SiteIdPO() As String
        Get
            Return _siteidpo
        End Get
        Set(ByVal value As String)
            _siteidpo = value
        End Set
    End Property


    Private _sitenamepo As String
    Public Property SiteNamePO() As String
        Get
            Return _sitenamepo
        End Get
        Set(ByVal value As String)
            _sitenamepo = value
        End Set
    End Property


    Private _fldtype As String
    Public Property FLDType() As String
        Get
            Return _fldtype
        End Get
        Set(ByVal value As String)
            _fldtype = value
        End Set
    End Property


    Private _scope As String
    Public Property Scope() As String
        Get
            Return _scope
        End Get
        Set(ByVal value As String)
            _scope = value
        End Set
    End Property


    Private _projectid As String
    Public Property ProjectID() As String
        Get
            Return _projectid
        End Get
        Set(ByVal value As String)
            _projectid = value
        End Set
    End Property


    Private _rgnname As String
    Public Property RegionName() As String
        Get
            Return _rgnname
        End Get
        Set(ByVal value As String)
            _rgnname = value
        End Set
    End Property


    Private _araname As String
    Public Property AreaName() As String
        Get
            Return _araname
        End Get
        Set(ByVal value As String)
            _araname = value
        End Set
    End Property

    Private _phaseTI As String
    Public Property PhaseTI() As String
        Get
            Return _phaseTI
        End Get
        Set(ByVal value As String)
            _phaseTI = value
        End Set
    End Property


    Private _podate As Nullable(Of DateTime)
    Public Property PODate() As Nullable(Of DateTime)
        Get
            Return _podate
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            _podate = value
        End Set
    End Property
	
	Private _poid As Int32
    Public Property POID() As Int32
        Get
            Return _poid
        End Get
        Set(ByVal value As Int32)
            _poid = value
        End Set
    End Property

    Private _hotaspo As String
    Public Property HOTAsPO() As String
        Get
            Return _hotaspo
        End Get
        Set(ByVal value As String)
            _hotaspo = value
        End Set
    End Property

End Class
