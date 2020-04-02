Imports Microsoft.VisualBasic

Public Class DocChangeInfo
    Inherits DocInfo


    Private _docSubtituteId As Integer
    Public Property DocSubtituteId() As Integer
        Get
            Return _docSubtituteId
        End Get
        Set(ByVal value As Integer)
            _docSubtituteId = value
        End Set
    End Property


    Private _docchangeid As Integer
    Public Property DocChangeId() As Integer
        Get
            Return _docchangeid
        End Get
        Set(ByVal value As Integer)
            _docchangeid = value
        End Set
    End Property

    Private _docnamechange As String
    Public Property DocNameChange() As String
        Get
            Return _docnamechange
        End Get
        Set(ByVal value As String)
            _docnamechange = value
        End Set
    End Property

    Private _lastmodifiedBy As String
    Public Property LastModifiedBy() As String
        Get
            Return _lastmodifiedBy
        End Get
        Set(ByVal value As String)
            _lastmodifiedBy = value
        End Set
    End Property


    Private _lastmodifiedDate As DateTime
    Public Property LastModifiedDate() As DateTime
        Get
            Return _lastmodifiedDate
        End Get
        Set(ByVal value As DateTime)
            _lastmodifiedDate = value
        End Set
    End Property


End Class
