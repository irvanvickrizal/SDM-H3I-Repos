Imports Microsoft.VisualBasic

Public Class TransactionTypeInfo
    Private _transid As Integer
    Public Property TransId() As Integer
        Get
            Return _transid
        End Get
        Set(ByVal value As Integer)
            _transid = value
        End Set
    End Property

    Private _transactiontype As String
    Public Property TransactionType() As String
        Get
            Return _transactiontype
        End Get
        Set(ByVal value As String)
            _transactiontype = value
        End Set
    End Property

    Private _transtable As String
    Public Property TransTable() As String
        Get
            Return _transtable
        End Get
        Set(ByVal value As String)
            _transtable = value
        End Set
    End Property

    Private _coddoctable As String
    Public Property CodDocTable() As String
        Get
            Return _coddoctable
        End Get
        Set(ByVal value As String)
            _coddoctable = value
        End Set
    End Property

    Private _docparentid As Integer
    Public Property DocParentId() As Integer
        Get
            Return _docparentid
        End Get
        Set(ByVal value As Integer)
            _docparentid = value
        End Set
    End Property


    Private _description As String
    Public Property Description() As String
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
        End Set
    End Property

    Private _cminfo As New CMInfo
    Public Property CMAInfo() As CMInfo
        Get
            Return _cminfo
        End Get
        Set(ByVal value As CMInfo)
            _cminfo = value
        End Set
    End Property


End Class
