Imports Microsoft.VisualBasic

Public Class WCCCODDocumentInfo

    Private _docid As Integer
    Public Property DocId() As Integer
        Get
            Return _docid
        End Get
        Set(ByVal value As Integer)
            _docid = value
        End Set
    End Property


    Private _docName As String
    Public Property DocName() As String
        Get
            Return _docName
        End Get
        Set(ByVal value As String)
            _docName = value
        End Set
    End Property


    Private _docDsc As String
    Public Property DocDesc() As String
        Get
            Return _docDsc
        End Get
        Set(ByVal value As String)
            _docDsc = value
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


    Private _docType As String
    Public Property DocType() As String
        Get
            Return _docType
        End Get
        Set(ByVal value As String)
            _docType = value
        End Set
    End Property


    Private _wccdoclmby As String
    Public Property WCCDocLMBY() As String
        Get
            Return _wccdoclmby
        End Get
        Set(ByVal value As String)
            _wccdoclmby = value
        End Set
    End Property


    Private _wccdoclmdt As DateTime
    Public Property WCCDocLMDT() As DateTime
        Get
            Return _wccdoclmdt
        End Get
        Set(ByVal value As DateTime)
            _wccdoclmdt = value
        End Set
    End Property


    Private _wccdocIsDeleted As Boolean
    Public Property WCCDocIsDeleted() As Boolean
        Get
            Return _wccdocIsDeleted
        End Get
        Set(ByVal value As Boolean)
            _wccdocIsDeleted = value
        End Set
    End Property

End Class
