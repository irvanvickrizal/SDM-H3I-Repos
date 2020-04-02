Imports Microsoft.VisualBasic

Public Class SOACAuditTrailInfo
    Inherits ODSOACInfo

    Private _logid As Int32
    Public Property LogId() As Int32
        Get
            Return _logid
        End Get
        Set(ByVal value As Int32)
            _logid = value
        End Set
    End Property



    Private _userprofile As New UserProfile
    Public Property UserInfo() As UserProfile
        Get
            Return _userprofile
        End Get
        Set(ByVal value As UserProfile)
            _userprofile = value
        End Set
    End Property


    Private _docinfo As New CODDocumentInfo
    Public Property DocInfo() As CODDocumentInfo
        Get
            Return _docinfo
        End Get
        Set(ByVal value As CODDocumentInfo)
            _docinfo = value
        End Set
    End Property


    Private _eventstarttime As System.Nullable(Of DateTime)
    Public Property EventStartTime() As System.Nullable(Of DateTime)
        Get
            Return _eventstarttime
        End Get
        Set(ByVal value As System.Nullable(Of DateTime))
            _eventstarttime = value
        End Set
    End Property


    Private _eventendtime As System.Nullable(Of DateTime)
    Public Property EventEndTime() As System.Nullable(Of DateTime)
        Get
            Return _eventendtime
        End Get
        Set(ByVal value As System.Nullable(Of DateTime))
            _eventendtime = value
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


    Private _category As String
    Public Property Category() As String
        Get
            Return _category
        End Get
        Set(ByVal value As String)
            _category = value
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


    Private _taskevent As String
    Public Property TaskEvent() As String
        Get
            Return _taskevent
        End Get
        Set(ByVal value As String)
            _taskevent = value
        End Set
    End Property



    Private _roleinf As New RoleInfo
    Public Property RoleInf() As RoleInfo
        Get
            Return _roleinf
        End Get
        Set(ByVal value As RoleInfo)
            _roleinf = value
        End Set
    End Property


End Class
