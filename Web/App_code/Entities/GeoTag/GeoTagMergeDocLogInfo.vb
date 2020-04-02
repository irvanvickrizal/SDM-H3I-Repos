Imports Microsoft.VisualBasic

Public Class GeoTagMergeDocLogInfo
    Inherits UserProfile


    Private _preparationlogid As Int32
    Public Property PreparationLogId() As Int32
        Get
            Return _preparationlogid
        End Get
        Set(ByVal value As Int32)
            _preparationlogid = value
        End Set
    End Property



    Private _preparationid As Int32
    Public Property PreparationId() As Int32
        Get
            Return _preparationid
        End Get
        Set(ByVal value As Int32)
            _preparationid = value
        End Set
    End Property

    Private _atpphotodocid As String
    Public Property ATPPhotoDocId() As String
        Get
            Return _atpphotodocid
        End Get
        Set(ByVal value As String)
            _atpphotodocid = value
        End Set
    End Property


    Private _rolename As String
    Public Property Rolename() As String
        Get
            Return _rolename
        End Get
        Set(ByVal value As String)
            _rolename = value
        End Set
    End Property


    Private _executeDate As DateTime
    Public Property ExecuteDate() As DateTime
        Get
            Return _executeDate
        End Get
        Set(ByVal value As DateTime)
            _executeDate = value
        End Set
    End Property


    Private _preparationStatus As String
    Public Property PreparationStatus() As String
        Get
            Return _preparationStatus
        End Get
        Set(ByVal value As String)
            _preparationStatus = value
        End Set
    End Property

End Class
