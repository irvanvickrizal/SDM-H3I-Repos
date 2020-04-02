Imports Microsoft.VisualBasic

Public Class ODSOACValueInfo
    Inherits ODSOACInfo


    Private _valueid As Int32
    Public Property ValueId() As Int32
        Get
            Return _valueid
        End Get
        Set(ByVal value As Int32)
            _valueid = value
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


    Private _valudesc As String
    Public Property ValueDesc() As String
        Get
            Return _valudesc
        End Get
        Set(ByVal value As String)
            _valudesc = value
        End Set
    End Property


    Private _poeuro As Double
    Public Property POEuro() As Double
        Get
            Return _poeuro
        End Get
        Set(ByVal value As Double)
            _poeuro = value
        End Set
    End Property


    Private _pousd As Double
    Public Property POUSD() As Double
        Get
            Return _pousd
        End Get
        Set(ByVal value As Double)
            _pousd = value
        End Set
    End Property


    Private _poidr As Double
    Public Property POIDR() As Double
        Get
            Return _poidr
        End Get
        Set(ByVal value As Double)
            _poidr = value
        End Set
    End Property


    Private _implEuro As Double
    Public Property ImplEuro() As Double
        Get
            Return _implEuro
        End Get
        Set(ByVal value As Double)
            _implEuro = value
        End Set
    End Property


    Private _implUSD As Double
    Public Property ImplUSD() As Double
        Get
            Return _implUSD
        End Get
        Set(ByVal value As Double)
            _implUSD = value
        End Set
    End Property


    Private _implIDR As Double
    Public Property ImplIDR() As Double
        Get
            Return _implIDR
        End Get
        Set(ByVal value As Double)
            _implIDR = value
        End Set
    End Property


    Private _basteuro As Double
    Public Property BASTEuro() As Double
        Get
            Return _basteuro
        End Get
        Set(ByVal value As Double)
            _basteuro = value
        End Set
    End Property


    Private _bastUSD As Double
    Public Property BASTUSD() As Double
        Get
            Return _bastUSD
        End Get
        Set(ByVal value As Double)
            _bastUSD = value
        End Set
    End Property


    Private _bastIDR As Double
    Public Property BASTIDR() As Double
        Get
            Return _bastIDR
        End Get
        Set(ByVal value As Double)
            _bastIDR = value
        End Set
    End Property


    Private _deltaeuro As Double
    Public Property DeltaEURO() As Double
        Get
            Return _deltaeuro
        End Get
        Set(ByVal value As Double)
            _deltaeuro = value
        End Set
    End Property


    Private _deltaUSD As Double
    Public Property DeltaUSD() As Double
        Get
            Return _deltaUSD
        End Get
        Set(ByVal value As Double)
            _deltaUSD = value
        End Set
    End Property


    Private _deltaIDR As Double
    Public Property DeltaIDR() As Double
        Get
            Return _deltaIDR
        End Get
        Set(ByVal value As Double)
            _deltaIDR = value
        End Set
    End Property


    Private _pousdtotal As Double
    Public Property POUSDTotal() As Double
        Get
            Return _pousdtotal
        End Get
        Set(ByVal value As Double)
            _pousdtotal = value
        End Set
    End Property


    Private _poidrtotal As Double
    Public Property POIDRTotal() As Double
        Get
            Return _poidrtotal
        End Get
        Set(ByVal value As Double)
            _poidrtotal = value
        End Set
    End Property


    Private _impusdtotal As Double
    Public Property ImpUSDTotal() As Double
        Get
            Return _impusdtotal
        End Get
        Set(ByVal value As Double)
            _impusdtotal = value
        End Set
    End Property


    Private _impidrtotal As Double
    Public Property ImpIDRTotal() As Double
        Get
            Return _impidrtotal
        End Get
        Set(ByVal value As Double)
            _impidrtotal = value
        End Set
    End Property


    Private _bastusdtotal As Double
    Public Property BastUSDTotal() As Double
        Get
            Return _bastusdtotal
        End Get
        Set(ByVal value As Double)
            _bastusdtotal = value
        End Set
    End Property


    Private _bastidrtotal As Double
    Public Property BastIDRTotal() As Double
        Get
            Return _bastidrtotal
        End Get
        Set(ByVal value As Double)
            _bastidrtotal = value
        End Set
    End Property


    Private _deltausdtotal As Double
    Public Property DeltaUSDTotal() As Double
        Get
            Return _deltausdtotal
        End Get
        Set(ByVal value As Double)
            _deltausdtotal = value
        End Set
    End Property


    Private _deltaidrtotal As Double
    Public Property DeltaIDRTotal() As Double
        Get
            Return _deltaidrtotal
        End Get
        Set(ByVal value As Double)
            _deltaidrtotal = value
        End Set
    End Property



    Private _descinfo As New ODSOACValueDescriptionInfo
    Public Property DescInfo() As ODSOACValueDescriptionInfo
        Get
            Return _descinfo
        End Get
        Set(ByVal value As ODSOACValueDescriptionInfo)
            _descinfo = value
        End Set
    End Property

End Class
