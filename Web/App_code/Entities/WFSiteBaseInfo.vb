Imports Microsoft.VisualBasic

Public Class WFSiteBaseInfo
    Inherits CMInfo

    Private _sno As Integer
    Public Property SNO() As Integer
        Get
            Return _sno
        End Get
        Set(ByVal value As Integer)
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


    Private _wfname As String
    Public Property WFName() As String
        Get
            Return _wfname
        End Get
        Set(ByVal value As String)
            _wfname = value
        End Set
    End Property

    Private _docid As Integer
    Public Property docid() As Integer
        Get
            Return _docid
        End Get
        Set(ByVal value As Integer)
            _docid = value
        End Set
    End Property


    Private _docname As String
    Public Property docname() As String
        Get
            Return _docname
        End Get
        Set(ByVal value As String)
            _docname = value
        End Set
    End Property

    Private _TimestampDocID As Integer
    Public Property TimestampDocID() As Integer
        Get
            Return _TimestampDocID
        End Get
        Set(ByVal value As Integer)
            _TimestampDocID = value
        End Set
    End Property


End Class
