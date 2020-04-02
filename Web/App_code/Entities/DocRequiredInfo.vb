Imports Microsoft.VisualBasic

Public Class DocRequiredInfo

    Private _reqId As Integer
    Public Property DocReqId() As Integer
        Get
            Return _reqId
        End Get
        Set(ByVal value As Integer)
            _reqId = value
        End Set
    End Property


    Private _requiredDoc As String
    Public Property RequiredDoc() As String
        Get
            Return _requiredDoc
        End Get
        Set(ByVal value As String)
            _requiredDoc = value
        End Set
    End Property


End Class
