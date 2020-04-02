Imports Microsoft.VisualBasic

Public Class CODActivityRoleGroupingInfo
    Inherits RoleInfo

    Private _roleactivityid As Integer
    Public Property RoleActivityId() As Integer
        Get
            Return _roleactivityid
        End Get
        Set(ByVal value As Integer)
            _roleactivityid = value
        End Set
    End Property


    Private _activityid As Integer
    Public Property ActivityId() As Integer
        Get
            Return _activityid
        End Get
        Set(ByVal value As Integer)
            _activityid = value
        End Set
    End Property



    Private _activityname As String
    Public Property ActivityName() As String
        Get
            Return _activityname
        End Get
        Set(ByVal value As String)
            _activityname = value
        End Set
    End Property



    Private _lmby As Integer
    Public Property LMBY() As Integer
        Get
            Return _lmby
        End Get
        Set(ByVal value As Integer)
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



    Private _modifiedUser As String
    Public Property ModifiedUser() As String
        Get
            Return _modifiedUser
        End Get
        Set(ByVal value As String)
            _modifiedUser = value
        End Set
    End Property


    Private _cdt As DateTime
    Public Property CDT() As DateTime
        Get
            Return _cdt
        End Get
        Set(ByVal value As DateTime)
            _cdt = value
        End Set
    End Property

End Class
