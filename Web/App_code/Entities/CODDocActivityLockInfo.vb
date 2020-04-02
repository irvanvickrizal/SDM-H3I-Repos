Imports Microsoft.VisualBasic

Public Class CODDocActivityLockInfo
    Inherits CODActivityInfo

    Private _docactivityloclid As Integer
    Public Property DocActivityLockId() As Integer
        Get
            Return _docactivityloclid
        End Get
        Set(ByVal value As Integer)
            _docactivityloclid = value
        End Set
    End Property


    Private _docid As Integer
    Public Property DocId() As Integer
        Get
            Return _docid
        End Get
        Set(ByVal value As Integer)
            _docid = value
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


    Private _disclaimer As String
    Public Property Disclaimer() As String
        Get
            Return _disclaimer
        End Get
        Set(ByVal value As String)
            _disclaimer = value
        End Set
    End Property



End Class
