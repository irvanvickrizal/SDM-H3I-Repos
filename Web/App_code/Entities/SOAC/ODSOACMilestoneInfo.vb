Imports Microsoft.VisualBasic

Public Class ODSOACMilestoneInfo

    Private _soacinfo As New ODSOACInfo
    Public Property SOACInfo() As ODSOACInfo
        Get
            Return _soacinfo
        End Get
        Set(ByVal value As ODSOACInfo)
            _soacinfo = value
        End Set
    End Property


    Private _uploaddate As Nullable(Of DateTime)
    Public Property UploadDate() As Nullable(Of DateTime)
        Get
            Return _uploaddate
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            _uploaddate = value
        End Set
    End Property


    Private _submitdate As Nullable(Of DateTime)
    Public Property SubmitDate() As Nullable(Of DateTime)
        Get
            Return _submitdate
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            _submitdate = value
        End Set
    End Property


    Private _approveddate As Nullable(Of DateTime)
    Public Property ApprovedDate() As Nullable(Of DateTime)
        Get
            Return _approveddate
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            _approveddate = value
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
