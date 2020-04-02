Imports Microsoft.VisualBasic

Public Class PermissionInfo
    Inherits CMInfo


    Private _permissionid As Integer
    Public Property PermissionId() As Integer
        Get
            Return _permissionid
        End Get
        Set(ByVal value As Integer)
            _permissionid = value
        End Set
    End Property


    Private _permissiontype As String
    Public Property PermissionType() As String
        Get
            Return _permissiontype
        End Get
        Set(ByVal value As String)
            _permissiontype = value
        End Set
    End Property


    Private _permissioncategory As String
    Public Property PermissionCategory() As String
        Get
            Return _permissioncategory
        End Get
        Set(ByVal value As String)
            _permissioncategory = value
        End Set
    End Property


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


End Class
