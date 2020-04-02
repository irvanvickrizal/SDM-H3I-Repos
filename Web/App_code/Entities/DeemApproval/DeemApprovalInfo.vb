Imports Microsoft.VisualBasic
Imports BusinessLogic

Public Class DeemApprovalInfo

    Private _oddaid As Integer
    Public Property ODDAID() As Integer
        Get
            Return _oddaid
        End Get
        Set(ByVal value As Integer)
            _oddaid = value
        End Set
    End Property

    Private _docinfo As New DocInfo
    Public Property DocInf() As DocInfo
        Get
            Return _docinfo
        End Get
        Set(ByVal value As DocInfo)
            _docinfo = value
        End Set
    End Property

    Private _taskgroup As String
    Public Property TaskGroup() As String
        Get
            Return _taskgroup
        End Get
        Set(ByVal value As String)
            _taskgroup = value
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

    Private _totaldoc As Integer
    Public Property TotalDoc() As Integer
        Get
            Return _totaldoc
        End Get
        Set(ByVal value As Integer)
            _totaldoc = value
        End Set
    End Property

    Private _sladoc As Integer
    Public Property SLADoc() As Integer
        Get
            Return _sladoc
        End Get
        Set(ByVal value As Integer)
            _sladoc = value
        End Set
    End Property

    Private _warningSLANotifDay As Integer
    Public Property WarningSLANotifDay() As Integer
        Get
            Return _warningSLANotifDay
        End Get
        Set(ByVal value As Integer)
            _warningSLANotifDay = value
        End Set
    End Property

    Private _autoexecuteafterSLADay As Integer
    Public Property AutoExeAfterSLA() As Integer
        Get
            Return _autoexecuteafterSLADay
        End Get
        Set(ByVal value As Integer)
            _autoexecuteafterSLADay = value
        End Set
    End Property

    Private _hasdocgroup As Boolean
    Public Property HasDocGroup() As Boolean
        Get
            Return _hasdocgroup
        End Get
        Set(ByVal value As Boolean)
            _hasdocgroup = value
        End Set
    End Property

    Private _docgroupinfo As New DocInfo
    Public Property DocGroupInfo() As DocInfo
        Get
            Return _docgroupinfo
        End Get
        Set(ByVal value As DocInfo)
            _docgroupinfo = value
        End Set
    End Property

    Private _transinfo As New TransactionTypeInfo
    Public Property TransInfo() As TransactionTypeInfo
        Get
            Return _transinfo
        End Get
        Set(ByVal value As TransactionTypeInfo)
            _transinfo = value
        End Set
    End Property

    Private _DAExecutionStatusInfo As DeemApprovalExecutionInfo
    Public Property DAExecutionStatusInfo() As DeemApprovalExecutionInfo
        Get
            Return _DAExecutionStatusInfo
        End Get
        Set(ByVal value As DeemApprovalExecutionInfo)
            _DAExecutionStatusInfo = value
        End Set
    End Property



    Private _cminfo As New CMInfo
    Public Property CMAInfo() As CMInfo
        Get
            Return _cminfo
        End Get
        Set(ByVal value As CMInfo)
            _cminfo = value
        End Set
    End Property

    Private _usrtypeinfo As New UserTypeInfo
    Public Property USRTypeInfo() As UserTypeInfo
        Get
            Return _usrtypeinfo
        End Get
        Set(ByVal value As UserTypeInfo)
            _usrtypeinfo = value
        End Set
    End Property


End Class
