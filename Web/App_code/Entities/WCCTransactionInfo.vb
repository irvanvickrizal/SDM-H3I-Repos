Imports Microsoft.VisualBasic

Public Class WCCTransactionInfo
    Inherits WCCInfo

    Private _sno As Int32
    Public Property SNO() As Int32
        Get
            Return _sno
        End Get
        Set(ByVal value As Int32)
            _sno = value
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


    Private _tskid As Integer
    Public Property TSKID() As Integer
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


    Private _ugpid As Integer
    Public Property UGPId() As Integer
        Get
            Return _ugpid
        End Get
        Set(ByVal value As Integer)
            _ugpid = value
        End Set
    End Property


    Private _pageNo As Integer
    Public Property PageNo() As Integer
        Get
            Return _pageNo
        End Get
        Set(ByVal value As Integer)
            _pageNo = value
        End Set
    End Property

    Private _sorderno As Integer
    Public Property SOrderNo() As Integer
        Get
            Return _sorderno
        End Get
        Set(ByVal value As Integer)
            _sorderno = value
        End Set
    End Property


End Class
