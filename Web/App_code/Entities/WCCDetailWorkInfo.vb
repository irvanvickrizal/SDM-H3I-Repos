Imports Microsoft.VisualBasic

Public Class WCCDetailWorkInfo
    Inherits WCCInfo

    Private _wccdetailworkid As Int32
    Public Property WCCDetailWorkId() As Int32
        Get
            Return _wccdetailworkid
        End Get
        Set(ByVal value As Int32)
            _wccdetailworkid = value
        End Set
    End Property

    Private _SISorSES_SISorSitac As Boolean
    Public Property SISorSES_SISorSITAC() As Boolean
        Get
            Return _SISorSES_SISorSitac
        End Get
        Set(ByVal value As Boolean)
            _SISorSES_SISorSitac = value
        End Set
    End Property

    Private _PKSorAJB50Perc_SISorSITAC As Boolean
    Public Property PKSorAJB50Perc_SISorSITAC() As Boolean
        Get
            Return _PKSorAJB50Perc_SISorSITAC
        End Get
        Set(ByVal value As Boolean)
            _PKSorAJB50Perc_SISorSITAC = value
        End Set
    End Property


    Private _IMB50Perc_SISorSITAC As Boolean
    Public Property IMB50Perc_SISorSITAC() As Boolean
        Get
            Return _IMB50Perc_SISorSITAC
        End Get
        Set(ByVal value As Boolean)
            _IMB50Perc_SISorSITAC = value
        End Set
    End Property


    Private _CAorLC100Perc_SISorSITAC As Boolean
    Public Property CAorLC100Perc_SISorSITAC() As Boolean
        Get
            Return _CAorLC100Perc_SISorSITAC
        End Get
        Set(ByVal value As Boolean)
            _CAorLC100Perc_SISorSITAC = value
        End Set
    End Property


    Private _SITACPermitting As Boolean
    Public Property SITACPermitting_SISorSITAC() As Boolean
        Get
            Return _SITACPermitting
        End Get
        Set(ByVal value As Boolean)
            _SITACPermitting = value
        End Set
    End Property


    Private _BAUT2G3G_CME As Boolean
    Public Property BAUT2G3G_CME() As Boolean
        Get
            Return _BAUT2G3G_CME
        End Get
        Set(ByVal value As Boolean)
            _BAUT2G3G_CME = value
        End Set
    End Property


    Private _SDHPDH_CME As Boolean
    Public Property SDHPDH_CME() As Boolean
        Get
            Return _SDHPDH_CME
        End Get
        Set(ByVal value As Boolean)
            _SDHPDH_CME = value
        End Set
    End Property


    Private _CMEorBAST2G_CME As Boolean
    Public Property CMEorBAST2G_CME() As Boolean
        Get
            Return _CMEorBAST2G_CME
        End Get
        Set(ByVal value As Boolean)
            _CMEorBAST2G_CME = value
        End Set
    End Property


    Private _Additional_CME As Boolean
    Public Property Additional_CME() As Boolean
        Get
            Return _Additional_CME
        End Get
        Set(ByVal value As Boolean)
            _Additional_CME = value
        End Set
    End Property


    Private _survey_TI As Boolean
    Public Property Survey_TI() As Boolean
        Get
            Return _survey_TI
        End Get
        Set(ByVal value As Boolean)
            _survey_TI = value
        End Set
    End Property


    Private _dismantling_TI As Boolean
    Public Property Dismantling_TI() As Boolean
        Get
            Return _dismantling_TI
        End Get
        Set(ByVal value As Boolean)
            _dismantling_TI = value
        End Set
    End Property


    Private _reconfig_TI As Boolean
    Public Property Reconfig_TI() As Boolean
        Get
            Return _reconfig_TI
        End Get
        Set(ByVal value As Boolean)
            _reconfig_TI = value
        End Set
    End Property


    Private _enclosure_TI As Boolean
    Public Property Enclosure_TI() As Boolean
        Get
            Return _enclosure_TI
        End Get
        Set(ByVal value As Boolean)
            _enclosure_TI = value
        End Set
    End Property


    Private _services_TI As Boolean
    Public Property Services_TI() As Boolean
        Get
            Return _services_TI
        End Get
        Set(ByVal value As Boolean)
            _services_TI = value
        End Set
    End Property


    Private _frequencyLicense_TI As Boolean
    Public Property FrequencyLicense_TI() As Boolean
        Get
            Return _frequencyLicense_TI
        End Get
        Set(ByVal value As Boolean)
            _frequencyLicense_TI = value
        End Set
    End Property


    Private _InitialTuning_NPO As Boolean
    Public Property InitialTuning_NPO() As Boolean
        Get
            Return _InitialTuning_NPO
        End Get
        Set(ByVal value As Boolean)
            _InitialTuning_NPO = value
        End Set
    End Property


    Private _ClusterTuning_NPO As Boolean
    Public Property ClusterTuning_NPO() As Boolean
        Get
            Return _ClusterTuning_NPO
        End Get
        Set(ByVal value As Boolean)
            _ClusterTuning_NPO = value
        End Set
    End Property


    Private _IBC_NPO As Boolean
    Public Property IBC_NPO() As Boolean
        Get
            Return _IBC_NPO
        End Get
        Set(ByVal value As Boolean)
            _IBC_NPO = value
        End Set
    End Property


    Private _optimization_NPO As Boolean
    Public Property Optimization_NPO() As Boolean
        Get
            Return _optimization_NPO
        End Get
        Set(ByVal value As Boolean)
            _optimization_NPO = value
        End Set
    End Property


    Private _SiteVerification_NPO As Boolean
    Public Property SiteVerification_NPO() As Boolean
        Get
            Return _SiteVerification_NPO
        End Get
        Set(ByVal value As Boolean)
            _SiteVerification_NPO = value
        End Set
    End Property


    Private _detailRFCovered_NPO As Boolean
    Public Property DetailRFCovered_NPO() As Boolean
        Get
            Return _detailRFCovered_NPO
        End Get
        Set(ByVal value As Boolean)
            _detailRFCovered_NPO = value
        End Set
    End Property


    Private _changeRequest_NPO As Boolean
    Public Property ChangeRequest_NPO() As Boolean
        Get
            Return _changeRequest_NPO
        End Get
        Set(ByVal value As Boolean)
            _changeRequest_NPO = value
        End Set
    End Property


    Private _DesignForMW_NPO As Boolean
    Public Property DesignForMW_NPO() As Boolean
        Get
            Return _DesignForMW_NPO
        End Get
        Set(ByVal value As Boolean)
            _DesignForMW_NPO = value
        End Set
    End Property


    Private _SDHPDH_NPO As Boolean
    Public Property SDHPDH_NPO() As Boolean
        Get
            Return _SDHPDH_NPO
        End Get
        Set(ByVal value As Boolean)
            _SDHPDH_NPO = value
        End Set
    End Property


    Private _SISSES_NPO As Boolean
    Public Property SISSES_NPO() As Boolean
        Get
            Return _SISSES_NPO
        End Get
        Set(ByVal value As Boolean)
            _SISSES_NPO = value
        End Set
    End Property


    Private _HICAP_BSC_COLO_DCS_NPO As Boolean
    Public Property HICAP_BSC_COLO_DCS_NPO() As Boolean
        Get
            Return _HICAP_BSC_COLO_DCS_NPO
        End Get
        Set(ByVal value As Boolean)
            _HICAP_BSC_COLO_DCS_NPO = value
        End Set
    End Property



End Class
