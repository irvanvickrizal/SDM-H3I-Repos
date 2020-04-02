Imports Microsoft.VisualBasic

Public Class ODSOACValueDescriptionInfo

    Private _descriptionid As Integer
    Public Property DescriptionId() As Integer
        Get
            Return _descriptionid
        End Get
        Set(ByVal value As Integer)
            _descriptionid = value
        End Set
    End Property


    Private _valuedesc As String
    Public Property ValueDescription() As String
        Get
            Return _valuedesc
        End Get
        Set(ByVal value As String)
            _valuedesc = value
        End Set
    End Property


End Class
