Imports Microsoft.VisualBasic

Public Class WCCDocAuthorityInfo

    Private _wccauthortyid As Integer
    Public Property WCCAuthortyId() As Integer
        Get
            Return _wccauthortyid
        End Get
        Set(ByVal value As Integer)
            _wccauthortyid = value
        End Set
    End Property


    Private _roleid As Integer
    Public Property RoleId() As Integer
        Get
            Return _roleid
        End Get
        Set(ByVal value As Integer)
            _roleid = value
        End Set
    End Property


    Private _rolename As String
    Public Property RoleName() As String
        Get
            Return _rolename
        End Get
        Set(ByVal value As String)
            _rolename = value
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


    Private _username As String
    Public Property UserName() As String
        Get
            Return _username
        End Get
        Set(ByVal value As String)
            _username = value
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


End Class
