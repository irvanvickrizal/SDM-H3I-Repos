Imports Microsoft.VisualBasic

Public Class GeoTagMergeDocumentInfo
    Inherits UserProfile


    Private _preparationId As Int32
    Public Property PreparationId() As Int32
        Get
            Return _preparationId
        End Get
        Set(ByVal value As Int32)
            _preparationId = value
        End Set
    End Property


    Private _atpphotodocid As String
    Public Property ATPPhotoDocId() As String
        Get
            Return _atpphotodocid
        End Get
        Set(ByVal value As String)
            _atpphotodocid = value
        End Set
    End Property


    Private _originaldocpath As String
    Public Property OriginalDocPath() As String
        Get
            Return _originaldocpath
        End Get
        Set(ByVal value As String)
            _originaldocpath = value
        End Set
    End Property


    Private _createdDate As DateTime
    Public Property CreatedDate() As DateTime
        Get
            Return _createdDate
        End Get
        Set(ByVal value As DateTime)
            _createdDate = value
        End Set
    End Property


    Private _lmby As Integer
    Public Property LMBY() As Integer
        Get
            Return _lmby
        End Get
        Set(ByVal value As Integer)
            _lmby = value
        End Set
    End Property


    Private _lmdt As DateTime
    Public Property LMDT() As DateTime
        Get
            Return _lmdt
        End Get
        Set(ByVal value As DateTime)
            _lmdt = value
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
