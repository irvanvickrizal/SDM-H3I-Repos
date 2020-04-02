Imports Microsoft.VisualBasic

Public Class WCCPendingInfo
    Inherits WCCInfo


    Private _userLocation As String
    Public Property UserLocation() As String
        Get
            Return _userLocation
        End Get
        Set(ByVal value As String)
            _userLocation = value
        End Set
    End Property


    Private _submitDate As System.Nullable(Of DateTime)
    Public Property SubmitDate() As System.Nullable(Of DateTime)
        Get
            Return _submitDate
        End Get
        Set(ByVal value As System.Nullable(Of DateTime))
            _submitDate = value
        End Set
    End Property


End Class
