Imports Microsoft.VisualBasic

Public Class WorkflowGroupInfo
    Inherits CMInfo

    Private _wfgrpid As Integer
    Public Property WFGRPID() As Integer
        Get
            Return _wfgrpid
        End Get
        Set(ByVal value As Integer)
            _wfgrpid = value
        End Set
    End Property

    Private _formtype As String
    Public Property FormType() As String
        Get
            Return _formtype
        End Get
        Set(ByVal value As String)
            _formtype = value
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


    Private _wfname As String
    Public Property WFName() As String
        Get
            Return _wfname
        End Get
        Set(ByVal value As String)
            _wfname = value
        End Set
    End Property


End Class
