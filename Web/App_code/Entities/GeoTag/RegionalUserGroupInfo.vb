Imports Microsoft.VisualBasic

Public Class RegionalUserGroupInfo
    Inherits CMInfo

    Private _grpid As Int32
    Public Property GrpId() As Int32
        Get
            Return _grpid
        End Get
        Set(ByVal value As Int32)
            _grpid = value
        End Set
    End Property


    Private _usrinfo As New UserProfileInfo
    Public Property UsrInfo() As UserProfileInfo
        Get
            Return _usrinfo
        End Get
        Set(ByVal value As UserProfileInfo)
            _usrinfo = value
        End Set
    End Property


    Private _rgninfo As New RegionInfo
    Public Property RgnInfo() As RegionInfo
        Get
            Return _rgninfo
        End Get
        Set(ByVal value As RegionInfo)
            _rgninfo = value
        End Set
    End Property
End Class
