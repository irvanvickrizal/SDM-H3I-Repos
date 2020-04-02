Imports Microsoft.VisualBasic

Public Class CODEmailBCCInfo
    Inherits CMInfo

    Private _sno As Integer
    Public Property SNO() As Integer
        Get
            Return _sno
        End Get
        Set(ByVal value As Integer)
            _sno = value
        End Set
    End Property


    Private _emaildoctype As String
    Public Property EmailDocType() As String
        Get
            Return _emaildoctype
        End Get
        Set(ByVal value As String)
            _emaildoctype = value
        End Set
    End Property


    Private _userinfo As New UserProfileInfo
    Public Property UserInfo() As UserProfileInfo
        Get
            Return _userinfo
        End Get
        Set(ByVal value As UserProfileInfo)
            _userinfo = value
        End Set
    End Property


    Private _roleInf As New RoleInfo
    Public Property RoleInf() As RoleInfo
        Get
            Return _roleInf
        End Get
        Set(ByVal value As RoleInfo)
            _roleInf = value
        End Set
    End Property

End Class
