Imports Microsoft.VisualBasic

Public Class Timestampapprovaldocinfo


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

    Private _lmby As Integer
    Public Property LMBY() As Integer
        Get
            Return _lmby
        End Get
        Set(ByVal value As Integer)
            _lmby = value
        End Set
    End Property

    Private _lmdt As Nullable(Of DateTime)
    Public Property LMDT() As Nullable(Of DateTime)
        Get
            Return _lmdt
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            _lmdt = value
        End Set
    End Property
    Private _modifieduser As String
    Public Property ModifiedUser() As String
        Get
            Return _modifieduser
        End Get
        Set(ByVal value As String)
            _modifieduser = value
        End Set
    End Property




End Class
