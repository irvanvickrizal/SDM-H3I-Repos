Imports Microsoft.VisualBasic

Public Class CODScopeBAUTSITInfo

    Private _msit As Integer
    Public Property MSIT() As Integer
        Get
            Return _msit
        End Get
        Set(ByVal value As Integer)
            _msit = value
        End Set
    End Property

    Private _docinfo As New DocInfo
    Public Property DocDependencyInfo() As DocInfo
        Get
            Return _docinfo
        End Get
        Set(ByVal value As DocInfo)
            _docinfo = value
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
