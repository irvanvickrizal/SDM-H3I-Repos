Imports Microsoft.VisualBasic

Public Class WorkflowDefinitionInfo
    Inherits WorkflowInfo

    Private _sorder As Integer
    Public Property SOrder() As Integer
        Get
            Return _sorder
        End Get
        Set(ByVal value As Integer)
            _sorder = value
        End Set
    End Property

    Private _roleinfo As New RoleInfo
    Public Property RoleInf() As RoleInfo
        Get
            Return _roleinfo
        End Get
        Set(ByVal value As RoleInfo)
            _roleinfo = value
        End Set
    End Property

    Private _usertypeinfo As New UserTypeInfo
    Public Property UserTypeInf() As UserTypeInfo
        Get
            Return _usertypeinfo
        End Get
        Set(ByVal value As UserTypeInfo)
            _usertypeinfo = value
        End Set
    End Property

    Private _tskinfo As New TaskInfo
    Public Property TskInfo() As TaskInfo
        Get
            Return _tskinfo
        End Get
        Set(ByVal value As TaskInfo)
            _tskinfo = value
        End Set
    End Property

    Private _escroleinfo As RoleInfo
    Public Property EscRoleInfo() As RoleInfo
        Get
            Return _escroleinfo
        End Get
        Set(ByVal value As RoleInfo)
            _escroleinfo = value
        End Set
    End Property

    Private _esctime As Integer
    Public Property EscTime() As Integer
        Get
            Return _esctime
        End Get
        Set(ByVal value As Integer)
            _esctime = value
        End Set
    End Property


    Private _cminfo As New CMInfo
    Public Property CMAWFInfo() As CMInfo
        Get
            Return _cminfo
        End Get
        Set(ByVal value As CMInfo)
            _cminfo = value
        End Set
    End Property


End Class
