Imports Microsoft.VisualBasic

Public Class DocNotMandatoryInfo
    Inherits CODInjectionTypeInfo


    Private _docid As Integer
    Public Property DocId() As Integer
        Get
            Return _docid
        End Get
        Set(ByVal value As Integer)
            _docid = value
        End Set
    End Property


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
