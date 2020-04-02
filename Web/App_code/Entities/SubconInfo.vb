Imports Microsoft.VisualBasic

Public Class SubconInfo

    Private _subconid As Integer
    Public Property SubconId() As Integer
        Get
            Return _subconid
        End Get
        Set(ByVal value As Integer)
            _subconid = value
        End Set
    End Property


    Private _subconname As String
    Public Property SubconName() As String
        Get
            Return _subconname
        End Get
        Set(ByVal value As String)
            _subconname = value
        End Set
    End Property


    Private _subconDesc As String
    Public Property SubconDesc() As String
        Get
            Return _subconDesc
        End Get
        Set(ByVal value As String)
            _subconDesc = value
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


    Private _isDeleted As Boolean
    Public Property IsDeleted() As Boolean
        Get
            Return _isDeleted
        End Get
        Set(ByVal value As Boolean)
            _isDeleted = value
        End Set
    End Property




End Class
