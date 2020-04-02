
Imports Common
Imports BusinessLogic
Imports System.Data
Imports CRFramework


Partial Class CR_frmNewChangeRequest
    Inherits System.Web.UI.Page
    Dim objdb As New DBUtil
    Dim objbl As New BODDLs
    Dim controller As New CRController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindWorkFlow()
            BindSiteAtt()
            BindInitiator()
            If Not String.IsNullOrEmpty(Request.QueryString("id")) Then
                TxtDescriptionOfChange.Text = controller.GetCRLastDescriptionChange(Request.QueryString("wpid"), Convert.ToInt32(Request.QueryString("id")))
                BindOldData(Convert.ToInt32(Request.QueryString("id")))
            Else
                TxtIndicativePriceCostUSD.Value = String.Format("{0:###,##.#0}", 0)
                TxtIndicativePriceCostIDR.Value = String.Format("{0:###,##.#0}", 0)
                TxtPercentagePriceChangeUSD.Value = Convert.ToDecimal(0.0)
                TxtPercentagePriceChangeIDR.Value = Convert.ToDecimal(0.0)
            End If
        End If
    End Sub

    Protected Sub LbtSaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtSave.Click
        Response.Redirect("~/BAUT/frmTI_CR.aspx?id=" & SaveCRForm() & "&wpid=" & Request.QueryString("wpid"))
    End Sub

    Protected Sub LbtCancelClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtCancel.Click
        Response.Redirect("frmListCR.aspx?wpid=" & Request.QueryString("wpid"))
    End Sub

    Protected Sub DdlWorkFlowTypeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlWorkflowType.SelectedIndexChanged
        If DdlWorkflowType.SelectedIndex > 0 Then
            BindSign(Request.QueryString("wpid"), Integer.Parse(DdlWorkflowType.SelectedValue))
        End If
    End Sub

#Region "custom methods"

    Private Sub BindWorkFlow()
        objbl.fillDDL(DdlWorkflowType, "TWorkFlow", False, Constants._DDL_Default_Select)
    End Sub

    Private Sub BindSiteAtt()
        If Not String.IsNullOrEmpty(Request.QueryString("wpid")) Then
            Dim strQuery As String = "select top 1 siteNo, sitename,PoNo,poname,RGNName,TselProjectID from poepmsitenew where workpackageid='" & Request.QueryString("wpid") & "'"
            Dim dtSiteAtt As DataTable = objdb.ExeQueryDT(strQuery, "siteAtt")
            If dtSiteAtt.Rows.Count > 0 Then
                LblSiteID.Text = dtSiteAtt.Rows(0).Item(0).ToString()
                LblSiteName.Text = dtSiteAtt.Rows(0).Item(1).ToString()
                LblArea.Text = dtSiteAtt.Rows(0).Item(4).ToString()
                LblPONo.Text = dtSiteAtt.Rows(0).Item(2).ToString()
                LblEOName.Text = dtSiteAtt.Rows(0).Item(3).ToString()
                LblProjectType.Text = "2G"
                LblProjectID.Text = dtSiteAtt.Rows(0).Item(5).ToString()
                LblDateSubmitted.Text = String.Format("{0:dd-MMM-yyyy}", DateTime.Now)
                LblProjectCategory.Text = "TI"
            End If
        End If
    End Sub

    Private Sub BindInitiator()
        Dim strQuery As String = "select top 1 name, usrType,usrRole from ebastusers_1 where usr_id=" & CommonSite.UserId
        Dim dtInitiator As DataTable = objdb.ExeQueryDT(strQuery, "init")
        If dtInitiator.Rows.Count > 0 Then
            LblInitiatorName.Text = dtInitiator.Rows(0).Item(0)
            LblInitiatorArea.Text = LblArea.Text
        End If
        If dtInitiator.Rows(0).Item(1).ToString.ToLower().Equals("n") Then
            LblInitiatorDepartment.Text = "Nokia Siemens Networks"
        Else
            LblInitiatorDepartment.Text = "Telkomsel"
        End If
    End Sub

    Private Sub BindOldData(ByVal crid As Int32)
        Dim info As CRInfo = controller.GetCRDetail(crid)
        If (Not String.IsNullOrEmpty(info.DescriptionofChange)) Then
            TxtDescriptionOfChange.Text = info.DescriptionofChange
        End If
        ChkRegulatoryRequirement.Checked = info.IsRegulatoryRequirement
        ChkSiteCondition.Checked = info.IsSiteCondition
        ChkDesignChange.Checked = info.IsDesignChange
        ChkTechnicalError.Checked = info.IsTechnicalError
        ChkOther.Checked = info.IsOther
        TxtDescription_JustificationComments.Text = info.JustificationComments
        ChkDesignImpact.Checked = info.IsDesignImpact
        ChkBudgetImpact.Checked = info.IsBudgetImpact
        TxtContractUSD.Value = String.Format("{0:###,##.#0}", info.ContractUSD)
        TxtContractIDR.Value = String.Format("{0:###,##.#0}", info.ContractIDR)
        TxtImplementationUSD.Value = String.Format("{0:###,##.#0}", info.ImplementationUSD)
        TxtImplementationIDR.Value = String.Format("{0:###,##.#0}", info.ImplementationIDR)
        TxtIndicativePriceCostUSD.Value = String.Format("{0:###,##.#0}", info.IndicativePriceCostUSD)
        TxtIndicativePriceCostIDR.Value = String.Format("{0:###,##.#0}", info.IndicativePriceCostIDR)
        TxtPercentagePriceChangeUSD.Value = String.Format("{0:###,##.#0}", info.PercentagePriceUSD)
        TxtPercentagePriceChangeIDR.Value = String.Format("{0:###,##.#0}", info.PercentagePriceIDR)
        TxtScheduleImpacts.Text = info.ScheduleImpact
        TxtOtherImpacts.Text = info.OtherImpact
        DdlWorkflowType.SelectedValue = info.WFID
        If info.WFID > 0 Then
            BindSign(Request.QueryString("wpid"), info.WFID)
        End If
    End Sub

    Private Function SaveCRForm() As String
        Dim info As New CRInfo
        If String.IsNullOrEmpty(Request.QueryString("id")) Then
            info.CRID = 0
        Else
            info.CRID = Convert.ToInt32(Request.QueryString("id"))
        End If
        info.CRNo = ""
        info.PackageId = Request.QueryString("wpid")
        info.DescriptionofChange = TxtDescriptionOfChange.Text
        info.IsRegulatoryRequirement = IIf(ChkRegulatoryRequirement.Checked = True, True, False)
        info.IsSiteCondition = IIf(ChkSiteCondition.Checked = True, True, False)
        info.IsDesignChange = IIf(ChkDesignChange.Checked = True, True, False)
        info.IsTechnicalError = IIf(ChkTechnicalError.Checked = True, True, False)
        info.IsOther = IIf(ChkOther.Checked = True, True, False)
        info.JustificationComments = TxtDescription_JustificationComments.Text
        info.IsDesignImpact = IIf(ChkDesignImpact.Checked = True, True, False)
        info.IsNoImpact = IIf(ChkNoImpact.Checked = True, True, False)
        info.IsBudgetImpact = IIf(ChkBudgetImpact.Checked = True, True, False)
        info.ContractUSD = Convert.ToDouble(TxtContractUSD.Value)
        info.ContractIDR = Convert.ToDouble(TxtContractIDR.Value)
        info.ImplementationUSD = Convert.ToDouble(TxtImplementationUSD.Value)
        info.ImplementationIDR = Convert.ToDouble(TxtImplementationIDR.Value)
        info.IndicativePriceCostUSD = Convert.ToDouble(TxtIndicativePriceCostUSD.Value)
        info.IndicativePriceCostIDR = Convert.ToDouble(TxtIndicativePriceCostIDR.Value)
        info.PercentagePriceUSD = Convert.ToDecimal(TxtPercentagePriceChangeUSD.Value)
        info.PercentagePriceIDR = Convert.ToDecimal(TxtPercentagePriceChangeIDR.Value)
        info.ScheduleImpact = TxtScheduleImpacts.Text
        info.OrgDocPath = ""
        info.DocPath = ""
        info.OtherImpact = TxtOtherImpacts.Text
        info.LMBY = CommonSite.UserId
        info.LMDT = DateTime.Now
        info.IsUploaded = False
        info.WFID = Integer.Parse(DdlWorkflowType.SelectedValue)
        Return Convert.ToString(controller.InsertUpdateCR(info))
    End Function

    Private Sub BindSign(ByVal packageid As String, ByVal wfid As Integer)
        BindTselSignature(4, packageid, wfid)
        BindNSNSignature(1, packageid, wfid)
    End Sub

    Private Sub BindTselSignature(ByVal grpid As Integer, ByVal packageid As String, ByVal workflowid As Integer)
        RptDigitalSignTelkomsel.DataSource = controller.GetUserApproverByGrouping(packageid, grpid, workflowid)
        RptDigitalSignTelkomsel.DataBind()
    End Sub

    Private Sub BindNSNSignature(ByVal grpid As Integer, ByVal packageid As String, ByVal workflowid As Integer)
        RptDigitalSignNSN.DataSource = controller.GetUserApproverByGrouping(packageid, grpid, workflowid)
        RptDigitalSignNSN.DataBind()
    End Sub

#End Region
End Class
