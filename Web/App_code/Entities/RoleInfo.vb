Imports Microsoft.VisualBasic

Public Class RoleInfo

    Private _roleid As Integer
    Public Property RoleId() As Integer
        Get
            Return _roleid
        End Get
        Set(ByVal value As Integer)
            _roleid = value
        End Set
    End Property


    Private _rolename As String
    Public Property RoleName() As String
        Get
            Return _rolename
        End Get
        Set(ByVal value As String)
            _rolename = value
        End Set
    End Property


    Private _usertypeid As Integer
    Public Property UserTypeId() As Integer
        Get
            Return _usertypeid
        End Get
        Set(ByVal value As Integer)
            _usertypeid = value
        End Set
    End Property


    Private _usertype As String
    Public Property UserType() As String
        Get
            Return _usertype
        End Get
        Set(ByVal value As String)
            _usertype = value
        End Set
    End Property


End Class
