Imports Microsoft.VisualBasic

Public Class HCPT_DocInfo
    Private _docid As Integer
    Public Property DocId() As Integer
        Get
            Return _docid
        End Get
        Set(ByVal value As Integer)
            _docid = value
        End Set
    End Property

    Private _doccode As String
    Public Property DocCode() As String
        Get
            Return _doccode
        End Get
        Set(ByVal value As String)
            _doccode = value
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

    Private _parentid As Integer
    Public Property ParentId() As Integer
        Get
            Return _parentid
        End Get
        Set(ByVal value As Integer)
            _parentid = value
        End Set
    End Property


    Private _doctype As String
    Public Property DocType() As String
        Get
            Return _doctype
        End Get
        Set(ByVal value As String)
            _doctype = value
        End Set
    End Property

    Private _apprrequired As Boolean
    Public Property AppRequired() As Boolean
        Get
            Return _apprrequired
        End Get
        Set(ByVal value As Boolean)
            _apprrequired = value
        End Set
    End Property

    Private _allowb4integration As Boolean
    Public Property AllowB4Integration() As Boolean
        Get
            Return _allowb4integration
        End Get
        Set(ByVal value As Boolean)
            _allowb4integration = value
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

    Private _dgbox As Boolean
    Public Property DGBox() As Boolean
        Get
            Return _dgbox
        End Get
        Set(ByVal value As Boolean)
            _dgbox = value
        End Set
    End Property

    Private _onlineform As String
    Public Property OnlineForm() As String
        Get
            Return _onlineform
        End Get
        Set(ByVal value As String)
            _onlineform = value
        End Set
    End Property

    Private _serialno As Integer
    Public Property SerialNo() As Integer
        Get
            Return _serialno
        End Get
        Set(ByVal value As Integer)
            _serialno = value
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
End Class
