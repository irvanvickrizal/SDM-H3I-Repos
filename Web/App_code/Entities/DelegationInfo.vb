Imports Microsoft.VisualBasic

Public Class DelegationInfo

    Private _delegationid As Int32
    Public Property DelegationId() As Int32
        Get
            Return _delegationid
        End Get
        Set(ByVal value As Int32)
            _delegationid = value
        End Set
    End Property



    Private _userid As Integer
    Public Property UserId() As Integer
        Get
            Return _userid
        End Get
        Set(ByVal value As Integer)
            _userid = value
        End Set
    End Property


    Private _userdelegationid As Integer
    Public Property UserDelegationId() As Integer
        Get
            Return _userdelegationid
        End Get
        Set(ByVal value As Integer)
            _userdelegationid = value
        End Set
    End Property


    Private _userdelegationname As String
    Public Property UserDelegationName() As String
        Get
            Return _userdelegationname
        End Get
        Set(ByVal value As String)
            _userdelegationname = value
        End Set
    End Property



    Private _status As String
    Public Property Status() As String
        Get
            Return _status
        End Get
        Set(ByVal value As String)
            _status = value
        End Set
    End Property


    Private _CDT As DateTime
    Public Property CDT() As DateTime
        Get
            Return _CDT
        End Get
        Set(ByVal value As DateTime)
            _CDT = value
        End Set
    End Property


    Private _LMDT As System.Nullable(Of DateTime)
    Public Property LMDT() As System.Nullable(Of DateTime)
        Get
            Return _LMDT
        End Get
        Set(ByVal value As System.Nullable(Of DateTime))
            _LMDT = value
        End Set
    End Property


    Private _LMBY As Integer
    Public Property LMBY() As Integer
        Get
            Return _LMBY
        End Get
        Set(ByVal value As Integer)
            _LMBY = value
        End Set
    End Property


    Private _usermodifiedname As String
    Public Property UserModified() As String
        Get
            Return _usermodifiedname
        End Get
        Set(ByVal value As String)
            _usermodifiedname = value
        End Set
    End Property



End Class
