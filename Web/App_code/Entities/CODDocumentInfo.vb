Imports Microsoft.VisualBasic

Public Class CODDocumentInfo
    Inherits CMInfo

    Private _docid As Integer
    Public Property DocId() As Integer
        Get
            Return _docid
        End Get
        Set(ByVal value As Integer)
            _docid = value
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


    Private _parentdocname As String
    Public Property ParentDocName() As String
        Get
            Return _parentdocname
        End Get
        Set(ByVal value As String)
            _parentdocname = value
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


    Private _docdesc As String
    Public Property Description() As String
        Get
            Return _docdesc
        End Get
        Set(ByVal value As String)
            _docdesc = value
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


    Private _docurl As String
    Public Property DocURL() As String
        Get
            Return _docurl
        End Get
        Set(ByVal value As String)
            _docurl = value
        End Set
    End Property

End Class
