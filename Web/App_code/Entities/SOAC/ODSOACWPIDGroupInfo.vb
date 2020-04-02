Imports Microsoft.VisualBasic

Public Class ODSOACWPIDGroupInfo
    Inherits ODSOACInfo

    Private _packageid As String
    Public Property PackageId() As String
        Get
            Return _packageid
        End Get
        Set(ByVal value As String)
            _packageid = value
        End Set
    End Property

End Class
