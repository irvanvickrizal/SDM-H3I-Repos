Imports Microsoft.VisualBasic

Public Class CMInfo

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


    Private _CDB As Integer
    Public Property CDB() As Integer
        Get
            Return _CDB
        End Get
        Set(ByVal value As Integer)
            _CDB = value
        End Set
    End Property


    Private _CDT As Nullable(Of DateTime)
    Public Property CDT() As Nullable(Of DateTime)
        Get
            Return _CDT
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            _CDT = value
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


    Private _createduser As String
    Public Property CreatedUser() As String
        Get
            Return _createduser
        End Get
        Set(ByVal value As String)
            _createduser = value
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
