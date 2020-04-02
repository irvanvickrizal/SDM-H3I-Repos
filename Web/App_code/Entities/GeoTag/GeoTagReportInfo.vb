Imports Microsoft.VisualBasic

Public Class GeoTagReportInfo
    Inherits CMInfo


    Private _engineerinfo As New UserProfileInfo
    Public Property EngineerInfo() As UserProfileInfo
        Get
            Return _engineerinfo
        End Get
        Set(ByVal value As UserProfileInfo)
            _engineerinfo = value
        End Set
    End Property


    Private _pminfo As New UserProfileInfo
    Public Property PMInfo() As UserProfileInfo
        Get
            Return _pminfo
        End Get
        Set(ByVal value As UserProfileInfo)
            _pminfo = value
        End Set
    End Property


    Private _regionalinfo As New RegionInfo
    Public Property RegionalInfo() As RegionInfo
        Get
            Return _regionalinfo
        End Get
        Set(ByVal value As RegionInfo)
            _regionalinfo = value
        End Set
    End Property


    Private _phonetype As String
    Public Property PhoneType() As String
        Get
            Return _phonetype
        End Get
        Set(ByVal value As String)
            _phonetype = value
        End Set
    End Property


    Private _compatibility As String
    Public Property Compatible() As String
        Get
            Return _compatibility
        End Get
        Set(ByVal value As String)
            _compatibility = value
        End Set
    End Property



    Private _trialstatus As String
    Public Property TrialStatus() As String
        Get
            Return _trialstatus
        End Get
        Set(ByVal value As String)
            _trialstatus = value
        End Set
    End Property

End Class
