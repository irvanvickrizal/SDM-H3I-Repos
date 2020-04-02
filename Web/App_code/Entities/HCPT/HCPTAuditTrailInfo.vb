Imports Microsoft.VisualBasic

Public Class HCPTAuditTrailInfo
    Private _sno As Int32
    Public Property SNO() As Int32
        Get
            Return _sno
        End Get
        Set(ByVal value As Int32)
            _sno = value
        End Set
    End Property

    Private _docinf As New DocInfo
    Public Property DocInf() As DocInfo
        Get
            Return _docinf
        End Get
        Set(ByVal value As DocInfo)
            _docinf = value
        End Set
    End Property


    Private _siteinf As New SiteInfo
    Public Property SiteInf() As SiteInfo
        Get
            Return _siteinf
        End Get
        Set(ByVal value As SiteInfo)
            _siteinf = value
        End Set
    End Property

    Private _taskinf As New TaskInfo
    Public Property TaskInf() As TaskInfo
        Get
            Return _taskinf
        End Get
        Set(ByVal value As TaskInfo)
            _taskinf = value
        End Set
    End Property


    Private _usrinf As New UserProfile
    Public Property UserInf() As UserProfile
        Get
            Return _usrinf
        End Get
        Set(ByVal value As UserProfile)
            _usrinf = value
        End Set
    End Property

    Private _eventstarttime As System.Nullable(Of DateTime)
    Public Property EventStartTime() As System.Nullable(Of DateTime)
        Get
            Return _eventstarttime
        End Get
        Set(ByVal value As System.Nullable(Of DateTime))
            _eventstarttime = value
        End Set
    End Property

    Private _eventenddtime As System.Nullable(Of DateTime)
    Public Property EventEndTime() As System.Nullable(Of DateTime)
        Get
            Return _eventenddtime
        End Get
        Set(ByVal value As System.Nullable(Of DateTime))
            _eventenddtime = value
        End Set
    End Property

    Private _remarks As String
    Public Property Remarks() As String
        Get
            Return _remarks
        End Get
        Set(ByVal value As String)
            _remarks = value
        End Set
    End Property

    Private _categories As String
    Public Property Categories() As String
        Get
            Return _categories
        End Get
        Set(ByVal value As String)
            _categories = value
        End Set
    End Property

    Private _status As Integer
    Public Property Status() As Integer
        Get
            Return _status
        End Get
        Set(ByVal value As Integer)
            _status = value
        End Set
    End Property

End Class
