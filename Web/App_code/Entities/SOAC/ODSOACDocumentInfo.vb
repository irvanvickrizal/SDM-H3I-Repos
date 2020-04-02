Imports Microsoft.VisualBasic

Public Class ODSOACDocumentInfo
    Inherits ODSOACInfo


    Private _sno As Int32
    Public Property SNO() As Int32
        Get
            Return _sno
        End Get
        Set(ByVal value As Int32)
            _sno = value
        End Set
    End Property



    Private _docinfo As New CODDocumentInfo
    Public Property DocInfo() As CODDocumentInfo
        Get
            Return _docinfo
        End Get
        Set(ByVal value As CODDocumentInfo)
            _docinfo = value
        End Set
    End Property


End Class
