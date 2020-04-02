Imports Microsoft.VisualBasic

Public Class ATPPipelineInfo

    Private _taskpendingid As Int32
    Public Property TaskPendingId() As Int32
        Get
            Return _taskpendingid
        End Get
        Set(ByVal value As Int32)
            _taskpendingid = value
        End Set
    End Property


    Private _originalfilename As String
    Public Property OriginalFilename() As String
        Get
            Return _originalfilename
        End Get
        Set(ByVal value As String)
            _originalfilename = value
        End Set
    End Property


    Private _originalPath As String
    Public Property OriginalPath() As String
        Get
            Return _originalPath
        End Get
        Set(ByVal value As String)
            _originalPath = value
        End Set
    End Property


    Private _pathFolder As String
    Public Property PathFolder() As String
        Get
            Return _pathFolder
        End Get
        Set(ByVal value As String)
            _pathFolder = value
        End Set
    End Property


    Private _packageid As String
    Public Property PackageId() As String
        Get
            Return _packageid
        End Get
        Set(ByVal value As String)
            _packageid = value
        End Set
    End Property


    Private _status As String
    Public Property Status() As String
        Get
            Return _status
        End Get
        Set(ByVal value As String)
            _status = value
        End Set
    End Property

End Class
