Imports Microsoft.VisualBasic


Public Class GeoTagInfo
    Inherits CRFramework.SiteInfo

    Private _ATPPDocPhotoId As String
    Public Property ATPDocPhotoId() As String
        Get
            Return _ATPPDocPhotoId
        End Get
        Set(ByVal value As String)
            _ATPPDocPhotoId = value
        End Set
    End Property


    Private _uploadjobid As String
    Public Property UploadJobId() As String
        Get
            Return _uploadjobid
        End Get
        Set(ByVal value As String)
            _uploadjobid = value
        End Set
    End Property


    Private _ReadyDate As Nullable(Of DateTime)
    Public Property ReadyDate() As Nullable(Of DateTime)
        Get
            Return _ReadyDate
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            _ReadyDate = value
        End Set
    End Property

    Private _GenerateDate As Nullable(Of DateTime)
    Public Property NewProperty() As Nullable(Of DateTime)
        Get
            Return _GenerateDate
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            _GenerateDate = value
        End Set
    End Property


    Private _atpdocpath As String
    Public Property ATPDOCPath() As String
        Get
            Return _atpdocpath
        End Get
        Set(ByVal value As String)
            _atpdocpath = value
        End Set
    End Property


    Private _atphtmldocpath As String
    Public Property ATPHTMLDocPath() As String
        Get
            Return _atphtmldocpath
        End Get
        Set(ByVal value As String)
            _atphtmldocpath = value
        End Set
    End Property



    Private _atpsitedocpath As String
    Public Property ATPSiteDocPath() As String
        Get
            Return _atpsitedocpath
        End Get
        Set(ByVal value As String)
            _atpsitedocpath = value
        End Set
    End Property


    Private _statusChecklist As String
    Public Property StatusChecklist() As String
        Get
            Return _statusChecklist
        End Get
        Set(ByVal value As String)
            _statusChecklist = value
        End Set
    End Property


    Private _useruploadid As Integer
    Public Property UserUploadId() As Integer
        Get
            Return _useruploadid
        End Get
        Set(ByVal value As Integer)
            _useruploadid = value
        End Set
    End Property


    Private _useruploadname As String
    Public Property UserUploadName() As String
        Get
            Return _useruploadname
        End Get
        Set(ByVal value As String)
            _useruploadname = value
        End Set
    End Property


    Private _companyname As String
    Public Property CompanyName() As String
        Get
            Return _companyname
        End Get
        Set(ByVal value As String)
            _companyname = value
        End Set
    End Property


    Private _swi As Int32
    Public Property SWID() As Int32
        Get
            Return _swi
        End Get
        Set(ByVal value As Int32)
            _swi = value
        End Set
    End Property


    Private _sitedocpath As String
    Public Property SiteDocPath() As String
        Get
            Return _sitedocpath
        End Get
        Set(ByVal value As String)
            _sitedocpath = value
        End Set
    End Property


    Private _siteorgdocpath As String
    Public Property SiteOrgDocPath() As String
        Get
            Return _siteorgdocpath
        End Get
        Set(ByVal value As String)
            _siteorgdocpath = value
        End Set
    End Property


End Class
