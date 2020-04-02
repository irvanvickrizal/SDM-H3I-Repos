Imports Microsoft.VisualBasic

Public Class SOACAttachmentDocInfo
    Inherits CODDocumentInfo

    Private _soacid As Int32
    Public Property SOACID() As Int32
        Get
            Return _soacid
        End Get
        Set(ByVal value As Int32)
            _soacid = value
        End Set
    End Property


End Class
