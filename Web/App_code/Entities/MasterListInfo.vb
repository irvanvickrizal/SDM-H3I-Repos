Imports Microsoft.VisualBasic
Imports System.Text

Public Class MasterListInfo


    Private _listid As Integer
    Public Property listId() As Integer
        Get
            Return _listid
        End Get
        Set(ByVal value As Integer)
            _listid = value
        End Set
    End Property


    Private _SN As String
    Public Property SN() As String
        Get
            Return _SN
        End Get
        Set(ByVal value As String)
            _SN = value
        End Set
    End Property

    Private _DOCName As String
    Public Property DOCName() As String
        Get
            Return _DOCName
        End Get
        Set(ByVal value As String)
            _DOCName = value
        End Set
    End Property

    Private _SerialOrder As Integer
    Public Property SerialOrder() As Integer
        Get
            Return _SerialOrder
        End Get
        Set(ByVal value As Integer)
            _SerialOrder = value
        End Set
    End Property

    Private _Parentid As Integer
    Public Property ParentId() As Integer
        Get
            Return _Parentid
        End Get
        Set(ByVal value As Integer)
            _Parentid = value
        End Set
    End Property


    Private _ListLMBY As String
    Public Property ListLMBY() As String
        Get
            Return _ListLMBY
        End Get
        Set(ByVal value As String)
            _ListLMBY = value
        End Set
    End Property


    Private _ListLMDT As DateTime
    Public Property ListLMDT() As DateTime
        Get
            Return _ListLMDT
        End Get
        Set(ByVal value As DateTime)
            _ListLMDT = value
        End Set
    End Property




    Private _BAUTSITInfo As CODScopeBAUTSITInfo
    Public Property ScopeBAUTSITInfo() As CODScopeBAUTSITInfo
        Get
            Return _BAUTSITInfo
        End Get
        Set(ByVal value As CODScopeBAUTSITInfo)
            _BAUTSITInfo = value
        End Set
    End Property


    Private _LTInfo As New CODScopeLTPartnerInfo
    Public Property ScopeLTPartnerInfo() As CODScopeLTPartnerInfo
        Get
            Return _LTInfo
        End Get
        Set(ByVal value As CODScopeLTPartnerInfo)
            _LTInfo = value
        End Set
    End Property




End Class
