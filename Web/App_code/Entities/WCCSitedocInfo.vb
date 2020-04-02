Imports Microsoft.VisualBasic

Public Class WCCSitedocInfo


    Private _wccsitedocid As Int32
    Public Property WCCSiteDocId() As Int32
        Get
            Return _wccsitedocid
        End Get
        Set(ByVal value As Int32)
            _wccsitedocid = value
        End Set
    End Property


    Private _wccid As Int32
    Public Property WCCID() As Int32
        Get
            Return _wccid
        End Get
        Set(ByVal value As Int32)
            _wccid = value
        End Set
    End Property


    Private _docid As Integer
    Public Property DocId() As Integer
        Get
            Return _docid
        End Get
        Set(ByVal value As Integer)
            _docid = value
        End Set
    End Property


    Private _docname As String
    Public Property DocName() As String
        Get
            Return _docname
        End Get
        Set(ByVal value As String)
            _docname = value
        End Set
    End Property



    Private _parentdoctype As String
    Public Property ParentDocType() As String
        Get
            Return _parentdoctype
        End Get
        Set(ByVal value As String)
            _parentdoctype = value
        End Set
    End Property



    Private _docPerc As Double
    Public Property DocPerc() As Double
        Get
            Return _docPerc
        End Get
        Set(ByVal value As Double)
            _docPerc = value
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


    Private _lmby As String
    Public Property LMBY() As String
        Get
            Return _lmby
        End Get
        Set(ByVal value As String)
            _lmby = value
        End Set
    End Property


    Private _docpath As String
    Public Property DocPath() As String
        Get
            Return _docpath
        End Get
        Set(ByVal value As String)
            _docpath = value
        End Set
    End Property


    Private _orgdocpath As String
    Public Property OrgDocPath() As String
        Get
            Return _orgdocpath
        End Get
        Set(ByVal value As String)
            _orgdocpath = value
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


    Private _canDeleted As Boolean
    Public Property CanDeleted() As Boolean
        Get
            Return _canDeleted
        End Get
        Set(ByVal value As Boolean)
            _canDeleted = value
        End Set
    End Property



    Private _rstatus As Integer
    Public Property Rstatus() As Integer
        Get
            Return _rstatus
        End Get
        Set(ByVal value As Integer)
            _rstatus = value
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


    Private _pono As String
    Public Property PoNo() As String
        Get
            Return _pono
        End Get
        Set(ByVal value As String)
            _pono = value
        End Set
    End Property


    Private _posubcon As String
    Public Property POSubcon() As String
        Get
            Return _posubcon
        End Get
        Set(ByVal value As String)
            _posubcon = value
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
