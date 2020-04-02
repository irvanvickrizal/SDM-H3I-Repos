Imports Microsoft.VisualBasic

Public Class WCCReportInfo

    Private _wccid As Int32
    Public Property WCCID() As Int32
        Get
            Return _wccid
        End Get
        Set(ByVal value As Int32)
            _wccid = value
        End Set
    End Property


    Private _PoNo As String
    Public Property PONO() As String
        Get
            Return _PoNo
        End Get
        Set(ByVal value As String)
            _PoNo = value
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


    Private _dscopeid As Integer
    Public Property DScopeId() As Integer
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



    Private _subconname As String
    Public Property SubconName() As String
        Get
            Return _subconname
        End Get
        Set(ByVal value As String)
            _subconname = value
        End Set
    End Property


    Private _certificateno As String
    Public Property CertificateNo() As String
        Get
            Return _certificateno
        End Get
        Set(ByVal value As String)
            _certificateno = value
        End Set
    End Property


    Private _issuancedate As System.Nullable(Of DateTime)
    Public Property IssuanceDate() As System.Nullable(Of DateTime)
        Get
            Return _issuancedate
        End Get
        Set(ByVal value As System.Nullable(Of DateTime))
            _issuancedate = value
        End Set
    End Property


    Private _submitDate As System.Nullable(Of DateTime)
    Public Property SubmitDate() As System.Nullable(Of DateTime)
        Get
            Return _submitDate
        End Get
        Set(ByVal value As System.Nullable(Of DateTime))
            _submitDate = value
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


    Private _ontaskpendingname As String
    Public Property OnTaskPendingName() As String
        Get
            Return _ontaskpendingname
        End Get
        Set(ByVal value As String)
            _ontaskpendingname = value
        End Set
    End Property



    Private _approvername As String
    Public Property ApproverName() As String
        Get
            Return _approvername
        End Get
        Set(ByVal value As String)
            _approvername = value
        End Set
    End Property


    Private _approverDate As System.Nullable(Of DateTime)
    Public Property ApproverDate() As System.Nullable(Of DateTime)
        Get
            Return _approverDate
        End Get
        Set(ByVal value As System.Nullable(Of DateTime))
            _approverDate = value
        End Set
    End Property


    Private _rejectionname As String
    Public Property RejectionName() As String
        Get
            Return _rejectionname
        End Get
        Set(ByVal value As String)
            _rejectionname = value
        End Set
    End Property


    Private _rejectiondate As System.Nullable(Of DateTime)
    Public Property RejectionDate() As System.Nullable(Of DateTime)
        Get
            Return _rejectiondate
        End Get
        Set(ByVal value As System.Nullable(Of DateTime))
            _rejectiondate = value
        End Set
    End Property



    Private _docacceptancedate As System.Nullable(Of DateTime)
    Public Property DocAcceptanceDate() As System.Nullable(Of DateTime)
        Get
            Return _docacceptancedate
        End Get
        Set(ByVal value As System.Nullable(Of DateTime))
            _docacceptancedate = value
        End Set
    End Property


    Private _activityname As String
    Public Property ActivityName() As String
        Get
            Return _activityname
        End Get
        Set(ByVal value As String)
            _activityname = value
        End Set
    End Property


    Private _wccstatus As String
    Public Property WCCStatus() As String
        Get
            Return _wccstatus
        End Get
        Set(ByVal value As String)
            _wccstatus = value
        End Set
    End Property


    Private _regionName As String
    Public Property RegionName() As String
        Get
            Return _regionName
        End Get
        Set(ByVal value As String)
            _regionName = value
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


    Private _rejectionRemarks As String
    Public Property RejectionRemarks() As String
        Get
            Return _rejectionRemarks
        End Get
        Set(ByVal value As String)
            _rejectionRemarks = value
        End Set
    End Property


    Private _rejectionCategory As String
    Public Property RejectionCategory() As String
        Get
            Return _rejectionCategory
        End Get
        Set(ByVal value As String)
            _rejectionCategory = value
        End Set
    End Property


    Private _reuploadname As String
    Public Property ReUploadName() As String
        Get
            Return _reuploadname
        End Get
        Set(ByVal value As String)
            _reuploadname = value
        End Set
    End Property


    Private _reuploadDate As System.Nullable(Of DateTime)
    Public Property ReUploadDate() As System.Nullable(Of DateTime)
        Get
            Return _reuploadDate
        End Get
        Set(ByVal value As System.Nullable(Of DateTime))
            _reuploadDate = value
        End Set
    End Property


End Class
