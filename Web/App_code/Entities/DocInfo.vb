Imports Microsoft.VisualBasic

Public Class DocInfo

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


    Private _parentid As Integer
    Public Property ParentId() As Integer
        Get
            Return _parentid
        End Get
        Set(ByVal value As Integer)
            _parentid = value
        End Set
    End Property


    Private _doctype As String
    Public Property DocType() As String
        Get
            Return _doctype
        End Get
        Set(ByVal value As String)
            _doctype = value
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
