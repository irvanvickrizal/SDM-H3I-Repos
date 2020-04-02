Imports Microsoft.VisualBasic

Public Class Timestampinfo


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

    Private _TDocName As String
    Public Property TDocName() As String
        Get
            Return _TDocName
        End Get
        Set(ByVal value As String)
            _TDocName = value
        End Set
    End Property

End Class
