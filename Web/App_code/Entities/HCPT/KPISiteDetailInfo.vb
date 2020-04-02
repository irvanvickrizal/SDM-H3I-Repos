Imports Microsoft.VisualBasic

Public Class KPISiteDetailInfo
    Inherits KPIInfo

    Private _detailid As Int32
    Public Property DetailId() As Int32
        Get
            Return _detailid
        End Get
        Set(ByVal value As Int32)
            _detailid = value
        End Set
    End Property

    Private _sector As Integer
    Public Property Sector() As Integer
        Get
            Return _sector
        End Get
        Set(ByVal value As Integer)
            _sector = value
        End Set
    End Property

    Private _cellid As String
    Public Property CellId() As String
        Get
            Return _cellid
        End Get
        Set(ByVal value As String)
            _cellid = value
        End Set
    End Property

    Private _antennatype As String
    Public Property AntennaType() As String
        Get
            Return _antennatype
        End Get
        Set(ByVal value As String)
            _antennatype = value
        End Set
    End Property

    Private _antennaheight As Integer
    Public Property AntennaHeight() As Integer
        Get
            Return _antennaheight
        End Get
        Set(ByVal value As Integer)
            _antennaheight = value
        End Set
    End Property

    Private _azimuth As Integer
    Public Property Azimuth() As Integer
        Get
            Return _azimuth
        End Get
        Set(ByVal value As Integer)
            _azimuth = value
        End Set
    End Property

    Private _mechtilt As Integer
    Public Property MechTilt() As Integer
        Get
            Return _mechtilt
        End Get
        Set(ByVal value As Integer)
            _mechtilt = value
        End Set
    End Property

    Private _electilt As Integer
    Public Property ElecTilt() As Integer
        Get
            Return _electilt
        End Get
        Set(ByVal value As Integer)
            _electilt = value
        End Set
    End Property

End Class
