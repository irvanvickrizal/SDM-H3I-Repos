Imports Microsoft.VisualBasic

Public Class WCCDeletionInfo
    Inherits WCCInfo


    Private _wccdeleteid As Int32
    Public Property WCCDeletedId() As Int32
        Get
            Return _wccdeleteid
        End Get
        Set(ByVal value As Int32)
            _wccdeleteid = value
        End Set
    End Property


    Private _wcccremarkDeleted As String
    Public Property WCCDeletionRemarks() As String
        Get
            Return _wcccremarkDeleted
        End Get
        Set(ByVal value As String)
            _wcccremarkDeleted = value
        End Set
    End Property


    Private _wccDeletedDate As DateTime
    Public Property WCCDeletedDate() As DateTime
        Get
            Return _wccDeletedDate
        End Get
        Set(ByVal value As DateTime)
            _wccDeletedDate = value
        End Set
    End Property


    Private _userdeletionid As Integer
    Public Property UserDeletionId() As Integer
        Get
            Return _userdeletionid
        End Get
        Set(ByVal value As Integer)
            _userdeletionid = value
        End Set
    End Property



    Private _userdeletion As String
    Public Property UserDeletion() As String
        Get
            Return _userdeletion
        End Get
        Set(ByVal value As String)
            _userdeletion = value
        End Set
    End Property


End Class
