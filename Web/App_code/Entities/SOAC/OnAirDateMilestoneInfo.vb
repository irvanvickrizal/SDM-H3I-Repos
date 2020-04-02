Imports Microsoft.VisualBasic

Public Class OnAirDateMilestoneInfo
    Inherits ODSOACInfo


    Private _SNO As Int32
    Public Property SNO() As Int32
        Get
            Return _SNO
        End Get
        Set(ByVal value As Int32)
            _SNO = value
        End Set
    End Property

    Private _signTitle As String
    Public Property SignTitle() As String
        Get
            Return _signTitle
        End Get
        Set(ByVal value As String)
            _signTitle = value
        End Set
    End Property


    Private _roleinf As New RoleInfo
    Public Property RoleInf() As RoleInfo
        Get
            Return _roleinf
        End Get
        Set(ByVal value As RoleInfo)
            _roleinf = value
        End Set
    End Property


    Private _lockstatus As String
    Public Property LockStatus() As String
        Get
            Return _lockstatus
        End Get
        Set(ByVal value As String)
            _lockstatus = value
        End Set
    End Property


   
End Class
