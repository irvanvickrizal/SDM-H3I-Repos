Imports Microsoft.VisualBasic

Public Class DGPassInfo
    Private _dgpassid As Int32
    Public Property DGPassId() As Int32
        Get
            Return _dgpassid
        End Get
        Set(ByVal value As Int32)
            _dgpassid = value
        End Set
    End Property

    Private _usrinfo As New UserProfile
    Public Property UserInfo() As UserProfile
        Get
            Return _usrinfo
        End Get
        Set(ByVal value As UserProfile)
            _usrinfo = value
        End Set
    End Property

    Private _newpassword As String
    Public Property NewPassword() As String
        Get
            Return _newpassword
        End Get
        Set(ByVal value As String)
            _newpassword = value
        End Set
    End Property


    Private _requestdate As Nullable(Of DateTime)
    Public Property RequestDate() As Nullable(Of DateTime)
        Get
            Return _requestdate
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            _requestdate = value
        End Set
    End Property


End Class
