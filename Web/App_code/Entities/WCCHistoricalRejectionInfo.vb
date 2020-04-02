Imports Microsoft.VisualBasic

Public Class WCCHistoricalRejectionInfo

    Private _rejectiontransid As Int32
    Public Property RejectionTransId() As Int32
        Get
            Return _rejectiontransid
        End Get
        Set(ByVal value As Int32)
            _rejectiontransid = value
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


    Private _docType As String
    Public Property DocType() As String
        Get
            Return _docType
        End Get
        Set(ByVal value As String)
            _docType = value
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


    Private _remarks As String
    Public Property Remarks() As String
        Get
            Return _remarks
        End Get
        Set(ByVal value As String)
            _remarks = value
        End Set
    End Property


    Private _RejectionDate As System.Nullable(Of DateTime)
    Public Property RejectionDate() As System.Nullable(Of DateTime)
        Get
            Return _RejectionDate
        End Get
        Set(ByVal value As System.Nullable(Of DateTime))
            _RejectionDate = value
        End Set
    End Property


    Private _rejectionUser As Integer
    Public Property RejectionUser() As Integer
        Get
            Return _rejectionUser
        End Get
        Set(ByVal value As Integer)
            _rejectionUser = value
        End Set
    End Property


    Private _reuploadDate As System.Nullable(Of DateTime)
    Public Property ReuploadDate() As System.Nullable(Of DateTime)
        Get
            Return _reuploadDate
        End Get
        Set(ByVal value As System.Nullable(Of DateTime))
            _reuploadDate = value
        End Set
    End Property


    Private _uploadUser As Integer
    Public Property UploadUser() As Integer
        Get
            Return _uploadUser
        End Get
        Set(ByVal value As Integer)
            _uploadUser = value
        End Set
    End Property


    Private _Wccid As Int32
    Public Property WCCID() As Int32
        Get
            Return _Wccid
        End Get
        Set(ByVal value As Int32)
            _Wccid = value
        End Set
    End Property


    Private _initiatorUser As Integer
    Public Property InitiatorUser() As Integer
        Get
            Return _initiatorUser
        End Get
        Set(ByVal value As Integer)
            _initiatorUser = value
        End Set
    End Property

End Class
