Imports Microsoft.VisualBasic

Public Class UnManageDocInfo
    Inherits CODDocumentInfo


    Private _undocid As Integer
    Public Property UnDocId() As Integer
        Get
            Return _undocid
        End Get
        Set(ByVal value As Integer)
            _undocid = value
        End Set
    End Property


    Private _parentdoctype As String
    Public Property ParentDocType() As String
        Get
            Return _parentdoctype
        End Get
        Set(ByVal value As String)
            _parentdoctype = value
        End Set
    End Property


End Class
