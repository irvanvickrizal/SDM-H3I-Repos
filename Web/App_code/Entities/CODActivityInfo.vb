Imports Microsoft.VisualBasic

Public Class CODActivityInfo


    Private _activityid As Integer
    Public Property ActivityId() As Integer
        Get
            Return _activityid
        End Get
        Set(ByVal value As Integer)
            _activityid = value
        End Set
    End Property



    Private _activityName As String
    Public Property ActivityName() As String
        Get
            Return _activityName
        End Get
        Set(ByVal value As String)
            _activityName = value
        End Set
    End Property



    Private _activityDesc As String
    Public Property ActivityDesc() As String
        Get
            Return _activityDesc
        End Get
        Set(ByVal value As String)
            _activityDesc = value
        End Set
    End Property


    Private _lmdt As DateTime
    Public Property Lmdt() As DateTime
        Get
            Return _lmdt
        End Get
        Set(ByVal value As DateTime)
            _lmdt = value
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



    Private _isDeleted As Boolean
    Public Property IsDeleted() As Boolean
        Get
            Return _isDeleted
        End Get
        Set(ByVal value As Boolean)
            _isDeleted = value
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




End Class
