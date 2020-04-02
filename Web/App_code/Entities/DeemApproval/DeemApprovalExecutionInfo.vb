Imports Microsoft.VisualBasic

Public Class DeemApprovalExecutionInfo
    Private _daexeid As Int32
    Public Property DAExeId() As Int32
        Get
            Return _daexeid
        End Get
        Set(ByVal value As Int32)
            _daexeid = value
        End Set
    End Property

    Private _transactionId As Int32
    Public Property TransactionId() As Int32
        Get
            Return _transactionId
        End Get
        Set(ByVal value As Int32)
            _transactionId = value
        End Set
    End Property

    Private _tskid As Integer
    Public Property TskId() As Integer
        Get
            Return _tskid
        End Get
        Set(ByVal value As Integer)
            _tskid = value
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

    Private _docsubmitdate As System.Nullable(Of DateTime)
    Public Property DocSubmitDate() As System.Nullable(Of DateTime)
        Get
            Return _docsubmitdate
        End Get
        Set(ByVal value As System.Nullable(Of DateTime))
            _docsubmitdate = value
        End Set
    End Property

    Private _dastatus As String
    Public Property DAStatus() As String
        Get
            Return _dastatus
        End Get
        Set(ByVal value As String)
            _dastatus = value
        End Set
    End Property

    Private _SLADate As Nullable(Of DateTime)
    Public Property SLADate() As Nullable(Of DateTime)
        Get
            Return _SLADate
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            _SLADate = value
        End Set
    End Property

    Private _notifdate As Nullable(Of DateTime)
    Public Property NotifDate() As Nullable(Of DateTime)
        Get
            Return _notifdate
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            _notifdate = value
        End Set
    End Property

    Private _executionDate As Nullable(Of DateTime)
    Public Property ExecutionDate() As Nullable(Of DateTime)
        Get
            Return _executionDate
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            _executionDate = value
        End Set
    End Property

    Private _nextroleid As Integer
    Public Property NextRoleId() As Integer
        Get
            Return _nextroleid
        End Get
        Set(ByVal value As Integer)
            _nextroleid = value
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

End Class
