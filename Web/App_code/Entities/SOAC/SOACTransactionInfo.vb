Imports Microsoft.VisualBasic

Public Class SOACTransactionInfo

    Private _sno As Int32
    Public Property SNO() As Int32
        Get
            Return _sno
        End Get
        Set(ByVal value As Int32)
            _sno = value
        End Set
    End Property


    Private _docinf As New DocInfo
    Public Property DocInf() As DocInfo
        Get
            Return _docinf
        End Get
        Set(ByVal value As DocInfo)
            _docinf = value
        End Set
    End Property


    Private _roleInf As New RoleInfo
    Public Property RoleInf() As RoleInfo
        Get
            Return _roleInf
        End Get
        Set(ByVal value As RoleInfo)
            _roleInf = value
        End Set
    End Property


    Private _soacinf As New ODSOACInfo
    Public Property SOACInfo() As ODSOACInfo
        Get
            Return _soacinf
        End Get
        Set(ByVal value As ODSOACInfo)
            _soacinf = value
        End Set
    End Property


    Private _soacmilestoneinfo As New ODSOACMilestoneInfo
    Public Property SOACMSInfo() As ODSOACMilestoneInfo
        Get
            Return _soacmilestoneinfo
        End Get
        Set(ByVal value As ODSOACMilestoneInfo)
            _soacmilestoneinfo = value
        End Set
    End Property



    Private _tskid As Integer
    Public Property TaskId() As Integer
        Get
            Return _tskid
        End Get
        Set(ByVal value As Integer)
            _tskid = value
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



    Private _startdatetime As System.Nullable(Of DateTime)
    Public Property StartDateTime() As System.Nullable(Of DateTime)
        Get
            Return _startdatetime
        End Get
        Set(ByVal value As System.Nullable(Of DateTime))
            _startdatetime = value
        End Set
    End Property



    Private _enddatetime As System.Nullable(Of DateTime)
    Public Property EndDateTime() As System.Nullable(Of DateTime)
        Get
            Return _enddatetime
        End Get
        Set(ByVal value As System.Nullable(Of DateTime))
            _enddatetime = value
        End Set
    End Property


    Private _status As Integer
    Public Property Status() As Integer
        Get
            Return _status
        End Get
        Set(ByVal value As Integer)
            _status = value
        End Set
    End Property


    Private _rstatus As Integer
    Public Property RStatus() As Integer
        Get
            Return _rstatus
        End Get
        Set(ByVal value As Integer)
            _rstatus = value
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


    Private _xval As Integer
    Public Property Xval() As Integer
        Get
            Return _xval
        End Get
        Set(ByVal value As Integer)
            _xval = value
        End Set
    End Property


    Private _yval As Integer
    Public Property Yval() As Integer
        Get
            Return _yval
        End Get
        Set(ByVal value As Integer)
            _yval = value
        End Set
    End Property


    Private _ugpid As Integer
    Public Property UGPID() As Integer
        Get
            Return _ugpid
        End Get
        Set(ByVal value As Integer)
            _ugpid = value
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

End Class
