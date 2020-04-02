Imports Microsoft.VisualBasic

Public Class WCCDocumentInfo
    Inherits DetailScopeInfo

    Private _wccdocid As Int32
    Public Property WCCDOCId() As Int32
        Get
            Return _wccdocid
        End Get
        Set(ByVal value As Int32)
            _wccdocid = value
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


    Private _isMandatory As Boolean
    Public Property IsMandatory() As Boolean
        Get
            Return _isMandatory
        End Get
        Set(ByVal value As Boolean)
            _isMandatory = value
        End Set
    End Property


    Private _docIsDeleted As Boolean
    Public Property DocIsDeleted() As Boolean
        Get
            Return _docIsDeleted
        End Get
        Set(ByVal value As Boolean)
            _docIsDeleted = value
        End Set
    End Property


    Private _docwccLMBY As String
    Public Property DOCWCCLMBY() As String
        Get
            Return _docwccLMBY
        End Get
        Set(ByVal value As String)
            _docwccLMBY = value
        End Set
    End Property


    Private _docwccLMDT As DateTime
    Public Property DOCWCCLMDT() As DateTime
        Get
            Return _docwccLMDT
        End Get
        Set(ByVal value As DateTime)
            _docwccLMDT = value
        End Set
    End Property


    Private _parentDocType As String
    Public Property ParentDocType() As String
        Get
            Return _parentDocType
        End Get
        Set(ByVal value As String)
            _parentDocType = value
        End Set
    End Property


    Private _canDeleted As Boolean
    Public Property CanDeleted() As Boolean
        Get
            Return _canDeleted
        End Get
        Set(ByVal value As Boolean)
            _canDeleted = value
        End Set
    End Property


    Private _docpath As String
    Public Property DocPath() As String
        Get
            Return _docpath
        End Get
        Set(ByVal value As String)
            _docpath = value
        End Set
    End Property


    Private _isuploaded As Boolean
    Public Property IsUploaded() As Boolean
        Get
            Return _isuploaded
        End Get
        Set(ByVal value As Boolean)
            _isuploaded = value
        End Set
    End Property

End Class
