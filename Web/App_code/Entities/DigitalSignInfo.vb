Imports Microsoft.VisualBasic

Public Class DigitalSignInfo
    Inherits CMInfo

    Private _dgid As Int32
    Public Property DGID() As Int32
        Get
            Return _dgid
        End Get
        Set(ByVal value As Int32)
            _dgid = value
        End Set
    End Property


    Private _userinfo As New UserProfile
    Public Property UserInfo() As UserProfile
        Get
            Return _userinfo
        End Get
        Set(ByVal value As UserProfile)
            _userinfo = value
        End Set
    End Property


    Private _dgpassword As String
    Public Property DGPassword() As String
        Get
            Return _dgpassword
        End Get
        Set(ByVal value As String)
            _dgpassword = value
        End Set
    End Property

End Class
