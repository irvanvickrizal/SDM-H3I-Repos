Imports System.Data
Partial Class fancybox_Form_fb_FormWCCDone
    Inherits System.Web.UI.Page

    Private controller As New WCCController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not Page.IsPostBack) Then
            If Not String.IsNullOrEmpty(Request.QueryString("wid")) Then
                BindForm(Convert.ToInt32(Request.QueryString("wid")))
            End If
        End If
    End Sub

#Region "Custom Methods"
    Private Sub BindForm(ByVal wccid As Int32)
        Dim info As WCCInfo = controller.GetODWCCBaseId(wccid)
        DisplaySubcontractorName.Text = info.SubconName
        DisplayWCCIssuanceDate.Text = String.Format("{0:dd-MMMM-yyyy}", info.IssuanceDate)
        DisplayCertificateNumber.Text = info.CertificateNumber
        DisplaySiteNameEPM.Text = info.SiteName
        DisplaySiteIDEPM.Text = info.SiteNo
        DisplayWorkpackageID.Text = info.PackageId
        HdnPackageId.Value = info.PackageId
        HdnIsRejected.Value = "0"
        DisplayWorkDescription.Text = info.ScopeName
        DisplayPOSubcontractor.Text = info.POSubcontractor
        DisplayPOTelkomsel.Text = info.PONO
        DisplayBAUTBASTDate.Text = String.Format("{0:dd-MMMM-yyyy}", info.BAUTDate)
        DisplayWCTRDate.Text = String.Format("{0:dd-MMMM-yyyy}", info.WCTRDate)
        BindChecklist(wccid)
        BindApprovalDocument(wccid)
    End Sub

    Private Sub BindApprovalDocument(ByVal wccid As Int32)
        Dim dtapprovals As DataTable = controller.WCCGetApprovalDocument(wccid)
        Dim kt As Integer
        Dim strApproval As String = String.Empty
        If dtapprovals.Rows.Count > 0 Then
            Dim rowcount As Integer = 1
            For kt = 0 To dtapprovals.Rows.Count - 1
                strApproval += rowcount.ToString & ". " & dtapprovals.Rows(kt).Item("TaskEvent") & " by <b>" & dtapprovals.Rows(kt).Item("name") & "-" & dtapprovals.Rows(kt).Item("SignTitle") & "</b> on " & String.Format("{0:dd-MMMM-yyyy}", Convert.ToDateTime(dtapprovals.Rows(kt).Item("ApprovalDate"))) & " <br/>"
                rowcount += 1
            Next
            divApproval.InnerHtml = strApproval
        Else
            divApproval.InnerText = "Not Yet Approved by users"
        End If
    End Sub

    Private Sub BindChecklist(ByVal wccid As Int32)
        Dim info As WCCDetailWorkInfo = controller.GetWCCDetailWork(wccid)
        If Not info Is Nothing Then
            ChkSIS_Sitac.Checked = info.SISorSES_SISorSITAC
            ChkPKS_AJB_50Perc.Checked = info.PKSorAJB50Perc_SISorSITAC
            ChkIMB_50Perc.Checked = info.IMB50Perc_SISorSITAC
            ChkCA_LC_100Perc.Checked = info.CAorLC100Perc_SISorSITAC
            ChkSITAC_Permitting.Checked = info.SITACPermitting_SISorSITAC

            Chk2G_3G_BAUT.Checked = info.BAUT2G3G_CME
            ChkSDH_PDH.Checked = info.SDHPDH_CME
            Chk2G_CME_BAST.Checked = info.CMEorBAST2G_CME
            ChkAdditional.Checked = info.Additional_CME

            ChkSurvey.Checked = info.Survey_TI
            ChkDismantling.Checked = info.Dismantling_TI
            ChkReconfig.Checked = info.Reconfig_TI
            ChkEnclosure.Checked = info.Enclosure_TI
            ChkServices.Checked = info.Services_TI
            ChkFreq_License.Checked = info.FrequencyLicense_TI

            ChkInitial_Tuning.Checked = info.InitialTuning_NPO
            ChkCluster_Tuning.Checked = info.ClusterTuning_NPO
            ChkIBC.Checked = info.IBC_NPO
            ChkOptimization.Checked = info.Optimization_NPO
            ChkSiteVerification.Checked = info.SiteVerification_NPO
            ChkDetailed_RF_Covered.Checked = info.DetailRFCovered_NPO

            ChkChange_Request.Checked = info.ChangeRequest_NPO
            ChkDesign_For_MW_Access.Checked = info.DesignForMW_NPO
            ChkNPO_SDH_PDH.Checked = info.SDHPDH_NPO
            ChkNPO_SIS_SES.Checked = info.SISSES_NPO
            ChkHICAP_BSC_COLO_DCS.Checked = info.HICAP_BSC_COLO_DCS_NPO
        End If
    End Sub

#End Region

End Class
