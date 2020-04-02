Imports Microsoft.VisualBasic

Public Class CODocumentReplacementInfo

    Private _codocumentreplacementid As Int32
    Public Property CODocumentReplacementId() As Int32
        Get
            Return _codocumentreplacementid
        End Get
        Set(ByVal value As Int32)
            _codocumentreplacementid = value
        End Set
    End Property


    Private _coid As Int32
    Public Property COID() As Int32
        Get
            Return _coid
        End Get
        Set(ByVal value As Int32)
            _coid = value
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


    Private _isUploaded As Boolean
    Public Property IsUploaded() As Boolean
        Get
            Return _isUploaded
        End Get
        Set(ByVal value As Boolean)
            _isUploaded = value
        End Set
    End Property


    Private _lmby As String
    Public Property LMBY() As String
        Get
            Return _lmby
        End Get
        Set(ByVal value As String)
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

End Class
