Imports Microsoft.VisualBasic

Public Class WCCInfo

    Private _wccid As Int32
    Public Property WCCID() As Int32
        Get
            Return _wccid
        End Get
        Set(ByVal value As Int32)
            _wccid = value
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


    Private _sconname As String
    Public Property SubconName() As String
        Get
            Return _sconname
        End Get
        Set(ByVal value As String)
            _sconname = value
        End Set
    End Property



    Private _issuanceDate As System.Nullable(Of DateTime)
    Public Property IssuanceDate() As System.Nullable(Of DateTime)
        Get
            Return _issuanceDate
        End Get
        Set(ByVal value As System.Nullable(Of DateTime))
            _issuanceDate = value
        End Set
    End Property


    Private _certificateNumber As String
    Public Property CertificateNumber() As String
        Get
            Return _certificateNumber
        End Get
        Set(ByVal value As String)
            _certificateNumber = value
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


    Private _posubcontractor As String
    Public Property POSubcontractor() As String
        Get
            Return _posubcontractor
        End Get
        Set(ByVal value As String)
            _posubcontractor = value
        End Set
    End Property


    Private _gscopeid As Integer
    Public Property GScopeId() As Integer
        Get
            Return _gscopeid
        End Get
        Set(ByVal value As Integer)
            _gscopeid = value
        End Set
    End Property


    Private _dscopeid As Integer
    Public Property DScopeID() As Integer
        Get
            Return _dscopeid
        End Get
        Set(ByVal value As Integer)
            _dscopeid = value
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




    Private _bautDate As System.Nullable(Of DateTime)
    Public Property BAUTDate() As System.Nullable(Of DateTime)
        Get
            Return _bautDate
        End Get
        Set(ByVal value As System.Nullable(Of DateTime))
            _bautDate = value
        End Set
    End Property


    Private _wctrDate As System.Nullable(Of DateTime)
    Public Property WCTRDate() As System.Nullable(Of DateTime)
        Get
            Return _wctrDate
        End Get
        Set(ByVal value As System.Nullable(Of DateTime))
            _wctrDate = value
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


    Private _cdt As DateTime
    Public Property CDT() As DateTime
        Get
            Return _cdt
        End Get
        Set(ByVal value As DateTime)
            _cdt = value
        End Set
    End Property


    Private _wccStatus As String
    Public Property WCCStatus() As String
        Get
            Return _wccStatus
        End Get
        Set(ByVal value As String)
            _wccStatus = value
        End Set
    End Property


    Private _workflowid As Integer
    Public Property WorkflowId() As Integer
        Get
            Return _workflowid
        End Get
        Set(ByVal value As Integer)
            _workflowid = value
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


    Private _scope As String
    Public Property Scope() As String
        Get
            Return _scope
        End Get
        Set(ByVal value As String)
            _scope = value
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


    Private _remarksofRejection As String
    Public Property RemarksOfRejection() As String
        Get
            Return _remarksofRejection
        End Get
        Set(ByVal value As String)
            _remarksofRejection = value
        End Set
    End Property


    Private _rejectionuser As String
    Public Property RejectionUser() As String
        Get
            Return _rejectionuser
        End Get
        Set(ByVal value As String)
            _rejectionuser = value
        End Set
    End Property


    Private _rejectionDate As DateTime
    Public Property RejectionDate() As DateTime
        Get
            Return _rejectionDate
        End Get
        Set(ByVal value As DateTime)
            _rejectionDate = value
        End Set
    End Property


    Private _activityId As Integer
    Public Property ActivityId() As Integer
        Get
            Return _activityId
        End Get
        Set(ByVal value As Integer)
            _activityId = value
        End Set
    End Property



End Class
