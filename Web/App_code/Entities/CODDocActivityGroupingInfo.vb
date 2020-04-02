Imports Microsoft.VisualBasic

Public Class CODDocActivityGroupingInfo
    Inherits DocInfo

    Private _docactivityid As Integer
    Public Property DocActivityId() As Integer
        Get
            Return _docactivityid
        End Get
        Set(ByVal value As Integer)
            _docactivityid = value
        End Set
    End Property



    Private _activityId As Integer
    Public Property ActivityId() As Integer
        Get
            Return _activityId
        End Get
        Set(ByVal value As Integer)
            _activityId = value
        End Set
    End Property


    Private _activityname As String
    Public Property ActivityName() As String
        Get
            Return _activityname
        End Get
        Set(ByVal value As String)
            _activityname = value
        End Set
    End Property



    Private _parentdoctype As String
    Public Property ParentDocType() As String
        Get
            Return _parentdoctype
        End Get
        Set(ByVal value As String)
            _parentdoctype = value
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


    Private _lmdt As DateTime
    Public Property LMDT() As DateTime
        Get
            Return _lmdt
        End Get
        Set(ByVal value As DateTime)
            _lmdt = value
        End Set
    End Property



    Private _modifiedUser As String
    Public Property ModifiedUser() As String
        Get
            Return _modifiedUser
        End Get
        Set(ByVal value As String)
            _modifiedUser = value
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
