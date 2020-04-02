Imports Microsoft.VisualBasic

Public Class WCCDoneInfo
    Inherits WCCInfo


    Private _initiatorName As String
    Public Property InitiatorName() As String
        Get
            Return _initiatorName
        End Get
        Set(ByVal value As String)
            _initiatorName = value
        End Set
    End Property


    Private _uploadDate As DateTime
    Public Property SubmitDate() As DateTime
        Get
            Return _uploadDate
        End Get
        Set(ByVal value As DateTime)
            _uploadDate = value
        End Set
    End Property

    Private _approverDate As DateTime
    Public Property ApproverDate() As DateTime
        Get
            Return _approverDate
        End Get
        Set(ByVal value As DateTime)
            _approverDate = value
        End Set
    End Property


    Private _approverName As String
    Public Property ApproverName() As String
        Get
            Return _approverName
        End Get
        Set(ByVal value As String)
            _approverName = value
        End Set
    End Property


End Class
