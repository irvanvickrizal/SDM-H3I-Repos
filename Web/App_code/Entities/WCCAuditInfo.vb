Imports Microsoft.VisualBasic

Public Class WCCAuditInfo
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


    Private _wccauditid As Int32
    Public Property WCCAuditId() As Int32
        Get
            Return _wccauditid
        End Get
        Set(ByVal value As Int32)
            _wccauditid = value
        End Set
    End Property

    Private _task As Integer
    Public Property Task() As Integer
        Get
            Return _task
        End Get
        Set(ByVal value As Integer)
            _task = value
        End Set
    End Property


    Private _userid As Integer
    Public Property UserId() As Integer
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


    Private _signTitle As String
    Public Property SignTitle() As String
        Get
            Return _signTitle
        End Get
        Set(ByVal value As String)
            _signTitle = value
        End Set
    End Property


    Private _taskEvent As String
    Public Property TaskEvent() As String
        Get
            Return _taskEvent
        End Get
        Set(ByVal value As String)
            _taskEvent = value
        End Set
    End Property


    Private _taskDesc As String
    Public Property TaskDesc() As String
        Get
            Return _taskDesc
        End Get
        Set(ByVal value As String)
            _taskDesc = value
        End Set
    End Property

    Private _eventstarttime As Nullable(Of DateTime)
    Public Property EvenStartTime() As Nullable(Of DateTime)
        Get
            Return _eventstarttime
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            _eventstarttime = value
        End Set
    End Property


    Private _eventendtimt As Nullable(Of DateTime)
    Public Property EventEndTime() As Nullable(Of DateTime)
        Get
            Return _eventendtimt
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            _eventendtimt = value
        End Set
    End Property


    Private _remarks As String
    Public Property Remarks() As String
        Get
            Return _remarks
        End Get
        Set(ByVal value As String)
            _remarks = value
        End Set
    End Property


    Private _categories As String
    Public Property Categories() As String
        Get
            Return _categories
        End Get
        Set(ByVal value As String)
            _categories = value
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


End Class
