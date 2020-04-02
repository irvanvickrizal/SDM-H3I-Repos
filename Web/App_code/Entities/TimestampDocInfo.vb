Imports Microsoft.VisualBasic

Public Class TimestampDocInfo
    Inherits CMInfo

    Private _Docid As Integer
    Public Property Docid() As Integer
        Get
            Return _Docid
        End Get
        Set(ByVal value As Integer)
            _Docid = value
        End Set
    End Property

    Private _DocName As String
    Public Property DocName() As String
        Get
            Return _DocName
        End Get
        Set(ByVal value As String)
            _DocName = value
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
