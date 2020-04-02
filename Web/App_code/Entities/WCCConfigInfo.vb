Imports Microsoft.VisualBasic

Public Class WCCConfigInfo
    Inherits WCCInfo

    Private _wccconfigid As Int32
    Public Property WCCConfigId() As Int32
        Get
            Return _wccconfigid
        End Get
        Set(ByVal value As Int32)
            _wccconfigid = value
        End Set
    End Property


    Private _packageidconfig As String
    Public Property PackageIdConfig() As String
        Get
            Return _packageidconfig
        End Get
        Set(ByVal value As String)
            _packageidconfig = value
        End Set
    End Property


    Private _atprequired As Boolean
    Public Property ATPRequired() As Boolean
        Get
            Return _atprequired
        End Get
        Set(ByVal value As Boolean)
            _atprequired = value
        End Set
    End Property


    Private _qcrequired As Boolean
    Public Property QCRequired() As Boolean
        Get
            Return _qcrequired
        End Get
        Set(ByVal value As Boolean)
            _qcrequired = value
        End Set
    End Property


    Private _subconid As Integer
    Public Property SubconId() As Integer
        Get
            Return _subconid
        End Get
        Set(ByVal value As Integer)
            _subconid = value
        End Set
    End Property


    Private _dscopeid As Integer
    Public Property DScopeIdWCC() As Integer
        Get
            Return _dscopeid
        End Get
        Set(ByVal value As Integer)
            _dscopeid = value
        End Set
    End Property


    Private _lmby As String
    Public Property ModifiedUser() As String
        Get
            Return _lmby
        End Get
        Set(ByVal value As String)
            _lmby = value
        End Set
    End Property


    Private _lmdt As DateTime
    Public Property LMDTWCC() As DateTime
        Get
            Return _lmdt
        End Get
        Set(ByVal value As DateTime)
            _lmdt = value
        End Set
    End Property


    Private _docRequiredId As Integer
    Public Property DocRequiredId() As Integer
        Get
            Return _docRequiredId
        End Get
        Set(ByVal value As Integer)
            _docRequiredId = value
        End Set
    End Property


End Class
