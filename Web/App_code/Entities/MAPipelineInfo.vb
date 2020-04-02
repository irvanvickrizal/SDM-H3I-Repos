Imports Microsoft.VisualBasic

Public Class MAPipelineInfo
    Inherits DocInfo


    Private _sno As Int32
    Public Property SNO() As Int32
        Get
            Return _sno
        End Get
        Set(ByVal value As Int32)
            _sno = value
        End Set
    End Property


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


    Private _pono As String
    Public Property PONO() As String
        Get
            Return _pono
        End Get
        Set(ByVal value As String)
            _pono = value
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


    Private _siteno As String
    Public Property SiteNo() As String
        Get
            Return _siteno
        End Get
        Set(ByVal value As String)
            _siteno = value
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


    Private _scope As String
    Public Property Scope() As String
        Get
            Return _scope
        End Get
        Set(ByVal value As String)
            _scope = value
        End Set
    End Property


    Private _submitDate As DateTime
    Public Property SubmitDate() As DateTime
        Get
            Return _submitDate
        End Get
        Set(ByVal value As DateTime)
            _submitDate = value
        End Set
    End Property

    Private _xval As Integer
    Public Property XVal() As Integer
        Get
            Return _xval
        End Get
        Set(ByVal value As Integer)
            _xval = value
        End Set
    End Property


    Private _yval As Integer
    Public Property YVal() As Integer
        Get
            Return _yval
        End Get
        Set(ByVal value As Integer)
            _yval = value
        End Set
    End Property


    Private _pageno As Integer
    Public Property PageNo() As Integer
        Get
            Return _pageno
        End Get
        Set(ByVal value As Integer)
            _pageno = value
        End Set
    End Property


    Private _swid As Int32
    Public Property SWID() As Int32
        Get
            Return _swid
        End Get
        Set(ByVal value As Int32)
            _swid = value
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

    Private _startmultipledate As DateTime
    Public Property StartMultipleDate() As DateTime
        Get
            Return _startmultipledate
        End Get
        Set(ByVal value As DateTime)
            _startmultipledate = value
        End Set
    End Property


    Private _username As String
    Public Property UserName() As String
        Get
            Return _username
        End Get
        Set(ByVal value As String)
            _username = value
        End Set
    End Property


    Private _userid As Integer
    Public Property USERID() As Integer
        Get
            Return _userid
        End Get
        Set(ByVal value As Integer)
            _userid = value
        End Set
    End Property


    Private _roleid As Integer
    Public Property RoleId() As Integer
        Get
            Return _roleid
        End Get
        Set(ByVal value As Integer)
            _roleid = value
        End Set
    End Property


    Private _userLogin As String
    Public Property UserLogin() As String
        Get
            Return _userLogin
        End Get
        Set(ByVal value As String)
            _userLogin = value
        End Set
    End Property

End Class
