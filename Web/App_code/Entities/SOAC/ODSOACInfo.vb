Imports Microsoft.VisualBasic

Public Class ODSOACInfo
    Inherits CMInfo


    Private _soacid As Int32
    Public Property SOACID() As Int32
        Get
            Return _soacid
        End Get
        Set(ByVal value As Int32)
            _soacid = value
        End Set
    End Property


    Private _siteonairdate As String
    Public Property SiteOnAirDate() As String
        Get
            Return _siteonairdate
        End Get
        Set(ByVal value As String)
            _siteonairdate = value
        End Set
    End Property


    Private _onairdate As System.Nullable(Of DateTime)
    Public Property OnAirDate() As System.Nullable(Of DateTime)
        Get
            Return _onairdate
        End Get
        Set(ByVal value As System.Nullable(Of DateTime))
            _onairdate = value
        End Set
    End Property


    Private _RolloutAgreement As String
    Public Property RolloutAgreement() As String
        Get
            Return _RolloutAgreement
        End Get
        Set(ByVal value As String)
            _RolloutAgreement = value
        End Set
    End Property


    Private _rolloutagreementDate As System.Nullable(Of DateTime)
    Public Property RolloutAgreementDate() As System.Nullable(Of DateTime)
        Get
            Return _rolloutagreementDate
        End Get
        Set(ByVal value As System.Nullable(Of DateTime))
            _rolloutagreementDate = value
        End Set
    End Property


    Private _porefno As String
    Public Property PORefNo() As String
        Get
            Return _porefno
        End Get
        Set(ByVal value As String)
            _porefno = value
        End Set
    End Property


    Private _porefnodate As System.Nullable(Of DateTime)
    Public Property PORefNoDate() As System.Nullable(Of DateTime)
        Get
            Return _porefnodate
        End Get
        Set(ByVal value As System.Nullable(Of DateTime))
            _porefnodate = value
        End Set
    End Property


    Private _finalco As String
    Public Property FinalCO() As String
        Get
            Return _finalco
        End Get
        Set(ByVal value As String)
            _finalco = value
        End Set
    End Property


    Private _finalCODate As System.Nullable(Of DateTime)
    Public Property FinalCODate() As System.Nullable(Of DateTime)
        Get
            Return _finalCODate
        End Get
        Set(ByVal value As System.Nullable(Of DateTime))
            _finalCODate = value
        End Set
    End Property


    Private _noticerefno As String
    Public Property NoticeRefNo() As String
        Get
            Return _noticerefno
        End Get
        Set(ByVal value As String)
            _noticerefno = value
        End Set
    End Property


    Private _noticerefnoDate As System.Nullable(Of DateTime)
    Public Property NoticeRefNoDate() As System.Nullable(Of DateTime)
        Get
            Return _noticerefnoDate
        End Get
        Set(ByVal value As System.Nullable(Of DateTime))
            _noticerefnoDate = value
        End Set
    End Property


    Private _wfid As Integer
    Public Property WFID() As Integer
        Get
            Return _wfid
        End Get
        Set(ByVal value As Integer)
            _wfid = value
        End Set
    End Property


    Private _workflow As String
    Public Property Workflow() As String
        Get
            Return _workflow
        End Get
        Set(ByVal value As String)
            _workflow = value
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


    Private _isUplaoded As Boolean
    Public Property IsUploaded() As Boolean
        Get
            Return _isUplaoded
        End Get
        Set(ByVal value As Boolean)
            _isUplaoded = value
        End Set
    End Property


    Private _isrejected As Boolean
    Public Property IsRejected() As Boolean
        Get
            Return _isrejected
        End Get
        Set(ByVal value As Boolean)
            _isrejected = value
        End Set
    End Property


    Private _remarksofrejection As String
    Public Property RemarksOfRejection() As String
        Get
            Return _remarksofrejection
        End Get
        Set(ByVal value As String)
            _remarksofrejection = value
        End Set
    End Property


    Private _rejectionuserid As Integer
    Public Property RejectionUserId() As Integer
        Get
            Return _rejectionuserid
        End Get
        Set(ByVal value As Integer)
            _rejectionuserid = value
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


    Private _soacstatus As String
    Public Property SOACStatus() As String
        Get
            Return _soacstatus
        End Get
        Set(ByVal value As String)
            _soacstatus = value
        End Set
    End Property

    Private _rejectionDate As System.Nullable(Of DateTime)
    Public Property RejectionDate() As System.Nullable(Of DateTime)
        Get
            Return _rejectionDate
        End Get
        Set(ByVal value As System.Nullable(Of DateTime))
            _rejectionDate = value
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



    Private _delayday As Integer
    Public Property DelayDay() As Integer
        Get
            Return _delayday
        End Get
        Set(ByVal value As Integer)
            _delayday = value
        End Set
    End Property

End Class
