Imports Microsoft.VisualBasic
Imports CRFramework

Public Class COTransactionNCInfo
    Inherits CRFramework.SiteInfo


    Private _coid As Int32
    Public Property COID() As Int32
        Get
            Return _coid
        End Get
        Set(ByVal value As Int32)
            _coid = value
        End Set
    End Property


    Private _initiatorUser As String
    Public Property InitiatorUser() As String
        Get
            Return _initiatorUser
        End Get
        Set(ByVal value As String)
            _initiatorUser = value
        End Set
    End Property


    Private _wfdesc As String
    Public Property WFDesc() As String
        Get
            Return _wfdesc
        End Get
        Set(ByVal value As String)
            _wfdesc = value
        End Set
    End Property


    Private _lastexecutiondate As DateTime
    Public Property LastExecutionDate() As DateTime
        Get
            Return _lastexecutiondate
        End Get
        Set(ByVal value As DateTime)
            _lastexecutiondate = value
        End Set
    End Property


    Private _pendingRole As String
    Public Property PendingRole() As String
        Get
            Return _pendingRole
        End Get
        Set(ByVal value As String)
            _pendingRole = value
        End Set
    End Property


    Private _docpathstatus As String
    Public Property DocPathStatus() As String
        Get
            Return _docpathstatus
        End Get
        Set(ByVal value As String)
            _docpathstatus = value
        End Set
    End Property


    Private _swid As String
    Public Property SWID() As String
        Get
            Return _swid
        End Get
        Set(ByVal value As String)
            _swid = value
        End Set
    End Property


End Class
