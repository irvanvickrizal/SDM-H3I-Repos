Imports Microsoft.VisualBasic

Public Class DocScopeGroupingInfo

    Private _docgroupid As Int32
    Public Property DocGroupId() As Int32
        Get
            Return _docgroupid
        End Get
        Set(ByVal value As Int32)
            _docgroupid = value
        End Set
    End Property


    Private _dscopeid As String
    Public Property DScopeId() As String
        Get
            Return _dscopeid
        End Get
        Set(ByVal value As String)
            _dscopeid = value
        End Set
    End Property


    Private _dscopename As String
    Public Property DScopeName() As String
        Get
            Return _dscopename
        End Get
        Set(ByVal value As String)
            _dscopename = value
        End Set
    End Property


    Private _docid As Integer
    Public Property DocId() As Integer
        Get
            Return _docid
        End Get
        Set(ByVal value As Integer)
            _docid = value
        End Set
    End Property


    Private _docname As String
    Public Property DocName() As String
        Get
            Return _docname
        End Get
        Set(ByVal value As String)
            _docname = value
        End Set
    End Property


    Private _lmby As String
    Public Property LMBY() As String
        Get
            Return _lmby
        End Get
        Set(ByVal value As String)
            _lmby = value
        End Set
    End Property


    Private _lmdt As DateTime
    Public Property LMDT() As DateTime
        Get
            Return _lmdt
        End Get
        Set(ByVal value As DateTime)
            _lmdt = value
        End Set
    End Property


End Class
