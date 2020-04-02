Imports Microsoft.VisualBasic

Public Class RegionInfo

    Private _rgnid As Integer
    Public Property RgnId() As Integer
        Get
            Return _rgnid
        End Get
        Set(ByVal value As Integer)
            _rgnid = value
        End Set
    End Property


    Private _rgnname As String
    Public Property RgnName() As String
        Get
            Return _rgnname
        End Get
        Set(ByVal value As String)
            _rgnname = value
        End Set
    End Property


    Private _rgncode As String
    Public Property RgnCode() As String
        Get
            Return _rgncode
        End Get
        Set(ByVal value As String)
            _rgncode = value
        End Set
    End Property



End Class
