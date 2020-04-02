Imports Microsoft.VisualBasic

Public Class WCCFlowGroupingInfo


    Private _flowgroupingid As Integer
    Public Property FlowGroupingId() As Integer
        Get
            Return _flowgroupingid
        End Get
        Set(ByVal value As Integer)
            _flowgroupingid = value
        End Set
    End Property


    Private _wfid As Integer
    Public Property WFID() As Integer
        Get
            Return _wfid
        End Get
        Set(ByVal value As Integer)
            _wfid = value
        End Set
    End Property



    Private _flowname As String
    Public Property FlowName() As String
        Get
            Return _flowname
        End Get
        Set(ByVal value As String)
            _flowname = value
        End Set
    End Property


    Private _lmby As String
    Public Property LMBY() As String
        Get
            Return _lmby
        End Get
        Set(ByVal value As String)
            _lmby = value
        End Set
    End Property


    Private _lmdt As DateTime
    Public Property LMDT() As DateTime
        Get
            Return _lmdt
        End Get
        Set(ByVal value As DateTime)
            _lmdt = value
        End Set
    End Property

    Private _docid As Integer
    Public Property docid() As Integer
        Get
            Return _docid
        End Get
        Set(ByVal value As Integer)
            _docid = value
        End Set
    End Property

    Private _docname As String
    Public Property docname() As String
        Get
            Return _docname
        End Get
        Set(ByVal value As String)
            _docname = value
        End Set
    End Property


End Class
