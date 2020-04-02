Imports Microsoft.VisualBasic

Public Class SerialNoInfo


    Private _imagePath As String
    Public Property ImagePath() As String
        Get
            Return _imagePath
        End Get
        Set(ByVal value As String)
            _imagePath = value
        End Set
    End Property

    Private _index As String
    Public Property Index() As String
        Get
            Return _index
        End Get
        Set(ByVal value As String)
            _index = value
        End Set
    End Property

    Private _rawSerialNo As String
    Public Property rawSerialNumber() As String
        Get
            Return _rawSerialNo
        End Get
        Set(ByVal value As String)
            _rawSerialNo = value
        End Set
    End Property

    Private _serialNumber As String
    Public Property SerialNumber() As String
        Get
            Return _serialNumber
        End Get
        Set(ByVal value As String)
            _serialNumber = value
        End Set
    End Property

    Private _uploaded As String
    Public Property Uploaded() As String
        Get
            Return _uploaded
        End Get
        Set(ByVal value As String)
            _uploaded = value
        End Set
    End Property

End Class
