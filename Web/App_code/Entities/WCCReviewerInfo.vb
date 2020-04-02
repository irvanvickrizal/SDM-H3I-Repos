Imports Microsoft.VisualBasic

Public Class WCCReviewerInfo
    Inherits UserProfile


    Private _reguserid As Integer
    Public Property RegisterUserId() As Integer
        Get
            Return _reguserid
        End Get
        Set(ByVal value As Integer)
            _reguserid = value
        End Set
    End Property


    Private _lmby As Integer
    Public Property LMBY() As Integer
        Get
            Return _lmby
        End Get
        Set(ByVal value As Integer)
            _lmby = value
        End Set
    End Property


    Private _modifieduser As String
    Public Property ModifiedUser() As String
        Get
            Return _modifieduser
        End Get
        Set(ByVal value As String)
            _modifieduser = value
        End Set
    End Property


    Private _cdt As DateTime
    Public Property CDT() As DateTime
        Get
            Return _cdt
        End Get
        Set(ByVal value As DateTime)
            _cdt = value
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


End Class
