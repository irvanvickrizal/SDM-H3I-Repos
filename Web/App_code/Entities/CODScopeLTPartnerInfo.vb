Imports Microsoft.VisualBasic

Public Class CODScopeLTPartnerInfo

    Private _ltid As Integer
    Public Property LTID() As Integer
        Get
            Return _ltid
        End Get
        Set(ByVal value As Integer)
            _ltid = value
        End Set
    End Property


    Private _ltvalue As Integer
    Public Property LTValue() As Integer
        Get
            Return _ltvalue
        End Get
        Set(ByVal value As Integer)
            _ltvalue = value
        End Set
    End Property

    Private _activityInfo As New CODActivityInfo
    Public Property ActivityInfo() As CODActivityInfo
        Get
            Return _activityInfo
        End Get
        Set(ByVal value As CODActivityInfo)
            _activityInfo = value
        End Set
    End Property


    Private _cmainfo As New CMInfo
    Public Property CMAInfo() As CMInfo
        Get
            Return _cmainfo
        End Get
        Set(ByVal value As CMInfo)
            _cmainfo = value
        End Set
    End Property


End Class
