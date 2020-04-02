Imports Microsoft.VisualBasic

Public Class ATPDocWithGeoTagMergeInfo
    Inherits UserProfile


    Private _mergedocid As Int32
    Public Property MergedocId() As Int32
        Get
            Return _mergedocid
        End Get
        Set(ByVal value As Int32)
            _mergedocid = value
        End Set
    End Property


    Private _mergedocpath As String
    Public Property MergeDocPath() As String
        Get
            Return _mergedocpath
        End Get
        Set(ByVal value As String)
            _mergedocpath = value
        End Set
    End Property


    Private _atpphotodocid As String
    Public Property ATPPhotoDocId() As String
        Get
            Return _atpphotodocid
        End Get
        Set(ByVal value As String)
            _atpphotodocid = value
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



    Private _isuploaded As Boolean
    Public Property IsUploaded() As Boolean
        Get
            Return _isuploaded
        End Get
        Set(ByVal value As Boolean)
            _isuploaded = value
        End Set
    End Property


    Private _sconid As Integer
    Public Property SCONID() As Integer
        Get
            Return _sconid
        End Get
        Set(ByVal value As Integer)
            _sconid = value
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
    Public Property PoName() As String
        Get
            Return _poname
        End Get
        Set(ByVal value As String)
            _poname = value
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
    Public Property siteName() As String
        Get
            Return _sitename
        End Get
        Set(ByVal value As String)
            _sitename = value
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


    Private _packageid As String
    Public Property PackageId() As String
        Get
            Return _packageid
        End Get
        Set(ByVal value As String)
            _packageid = value
        End Set
    End Property


End Class
