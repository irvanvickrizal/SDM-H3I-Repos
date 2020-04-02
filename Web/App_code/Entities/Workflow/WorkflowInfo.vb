Imports Microsoft.VisualBasic

Public Class WorkflowInfo
    Private _wfid As Integer
    Public Property WFID() As Integer
        Get
            Return _wfid
        End Get
        Set(ByVal value As Integer)
            _wfid = value
        End Set
    End Property

    Private _wfname As String
    Public Property WFName() As String
        Get
            Return _wfname
        End Get
        Set(ByVal value As String)
            _wfname = value
        End Set
    End Property

    Private _wfcode As String
    Public Property WFCode() As String
        Get
            Return _wfcode
        End Get
        Set(ByVal value As String)
            _wfcode = value
        End Set
    End Property

    Private _dscopeid As New ScopeInfo
    Public Property DscopeInfo() As ScopeInfo
        Get
            Return _dscopeid
        End Get
        Set(ByVal value As ScopeInfo)
            _dscopeid = value
        End Set
    End Property


    Private _SLATotal As Integer
    Public Property SLATotal() As Integer
        Get
            Return _SLATotal
        End Get
        Set(ByVal value As Integer)
            _SLATotal = value
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


    Private _cminfo As New CMInfo
    Public Property CMAInfo() As CMInfo
        Get
            Return _cminfo
        End Get
        Set(ByVal value As CMInfo)
            _cminfo = value
        End Set
    End Property

End Class
