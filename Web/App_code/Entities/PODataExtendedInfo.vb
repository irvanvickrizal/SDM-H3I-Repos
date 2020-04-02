Imports Microsoft.VisualBasic

Public Class PODataExtendedInfo
    Inherits CMInfo

    Private _poid As Int32
    Public Property POID() As Int32
        Get
            Return _poid
        End Get
        Set(ByVal value As Int32)
            _poid = value
        End Set
    End Property


    Private _poidraw As Int32
    Public Property POIDRaw() As Int32
        Get
            Return _poidraw
        End Get
        Set(ByVal value As Int32)
            _poidraw = value
        End Set
    End Property


    Private _pono As String
    Public Property PONO() As String
        Get
            Return _pono
        End Get
        Set(ByVal value As String)
            _pono = value
        End Set
    End Property


    Private _podate As System.Nullable(Of DateTime)
    Public Property PODate() As System.Nullable(Of DateTime)
        Get
            Return _podate
        End Get
        Set(ByVal value As System.Nullable(Of DateTime))
            _podate = value
        End Set
    End Property


    Private _newpodata As System.Nullable(Of DateTime)
    Public Property NewPODate() As System.Nullable(Of DateTime)
        Get
            Return _newpodata
        End Get
        Set(ByVal value As System.Nullable(Of DateTime))
            _newpodata = value
        End Set
    End Property


    Private _podatestring As String
    Public Property NewPODateString() As String
        Get
            Return _podatestring
        End Get
        Set(ByVal value As String)
            _podatestring = value
        End Set
    End Property


End Class
