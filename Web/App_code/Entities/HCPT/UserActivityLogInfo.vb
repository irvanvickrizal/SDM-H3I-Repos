Imports Microsoft.VisualBasic

Public Class UserActivityLogInfo
    Inherits UserProfileInfo

    Private _activityDate As System.Nullable(Of DateTime)
    Public Property ActivityDate() As System.Nullable(Of DateTime)
        Get
            Return _activityDate
        End Get
        Set(ByVal value As System.Nullable(Of DateTime))
            _activityDate = value
        End Set
    End Property


    Private _description As String
    Public Property Description() As String
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
        End Set
    End Property

    Private _ipaddress As String
    Public Property IPAddress() As String
        Get
            Return _ipaddress
        End Get
        Set(ByVal value As String)
            _ipaddress = value
        End Set
    End Property


End Class
