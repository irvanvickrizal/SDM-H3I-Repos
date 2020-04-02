Imports Microsoft.VisualBasic

Public Class BusinessDayInfo
    Private _BDId As Int32
    Public Property BDID() As Int32
        Get
            Return _BDId
        End Get
        Set(ByVal value As Int32)
            _BDId = value
        End Set
    End Property

    Private _offdate As DateTime
    Public Property OffDate() As DateTime
        Get
            Return _offdate
        End Get
        Set(ByVal value As DateTime)
            _offdate = value
        End Set
    End Property

    Private _Description As String
    Public Property Description() As String
        Get
            Return _Description
        End Get
        Set(ByVal value As String)
            _Description = value
        End Set
    End Property

    Private _cminfo As New CMInfo
    Public Property CMAInfo() As CMInfo
        Get
            Return _cminfo
        End Get
        Set(ByVal value As CMInfo)
            _cminfo = value
        End Set
    End Property

End Class
