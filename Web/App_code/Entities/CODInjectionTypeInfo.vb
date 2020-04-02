Imports Microsoft.VisualBasic

Public Class CODInjectionTypeInfo


    Private _injectionId As Integer
    Public Property InjectionId() As Integer
        Get
            Return _injectionId
        End Get
        Set(ByVal value As Integer)
            _injectionId = value
        End Set
    End Property



    Private _injectionName As String
    Public Property InjectionName() As String
        Get
            Return _injectionName
        End Get
        Set(ByVal value As String)
            _injectionName = value
        End Set
    End Property


    Private _injectionDesc As String
    Public Property InjectionDesc() As String
        Get
            Return _injectionDesc
        End Get
        Set(ByVal value As String)
            _injectionDesc = value
        End Set
    End Property


    Private _parentDocId As Integer
    Public Property ParentDocId() As Integer
        Get
            Return _parentDocId
        End Get
        Set(ByVal value As Integer)
            _parentDocId = value
        End Set
    End Property


    Private _parentDocName As String
    Public Property ParentDocName() As String
        Get
            Return _parentDocName
        End Get
        Set(ByVal value As String)
            _parentDocName = value
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



    Private _lmdt As DateTime
    Public Property LMDT() As DateTime
        Get
            Return _lmdt
        End Get
        Set(ByVal value As DateTime)
            _lmdt = value
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


    Private _isDeleted As Boolean
    Public Property IsDeleted() As Boolean
        Get
            Return _isDeleted
        End Get
        Set(ByVal value As Boolean)
            _isDeleted = value
        End Set
    End Property


    Private _ddlinjectionid As String
    Public Property DdlInjectionId() As String
        Get
            Return _ddlinjectionid
        End Get
        Set(ByVal value As String)
            _ddlinjectionid = value
        End Set
    End Property


End Class
