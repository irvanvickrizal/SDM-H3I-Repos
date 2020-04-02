Imports CRFramework
Imports System.Data
Imports System.Collections.Generic

Partial Class WCC_frmODWCCFinal
    Inherits System.Web.UI.Page
    Private controller As New WCCController
    Private crcontroller As New CRController
    Dim acontroller As New CODActivityController
    Dim objmail As New TakeMail
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            If Not String.IsNullOrEmpty(Request.QueryString("wid")) Then
                BindForm(Convert.ToInt32(Request.QueryString("wid")))
            End If
        End If
    End Sub

    Protected Sub LbtSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtSubmit.Click
        Dim isCompliting As Boolean = True
        If (controller.IsDocUploadCompleted(Convert.ToInt32(Request.QueryString("wid"))) = True) Then
            If (controller.IsValidWCCWorkflow(Convert.ToInt32(Request.QueryString("wid"))) = False) Then
                isCompliting = False
                If (Not ClientScript.IsStartupScriptRegistered("alert")) Then
                    Page.ClientScript.RegisterStartupScript _
                                (Me.GetType(), "alert", "WFNYD();", True)
                End If
            End If
        Else
            isCompliting = False
            If (Not ClientScript.IsStartupScriptRegistered("alert")) Then
                Page.ClientScript.RegisterStartupScript _
            (Me.GetType(), "alert", "DocNYC();", True)
            End If
        End If

        If isCompliting = True Then
            Dim detailworkinfo As New WCCDetailWorkInfo
            detailworkinfo.SISorSES_SISorSITAC = ChkSIS_Sitac.Checked
            detailworkinfo.PKSorAJB50Perc_SISorSITAC = ChkPKS_AJB_50Perc.Checked
            detailworkinfo.IMB50Perc_SISorSITAC = ChkIMB_50Perc.Checked
            detailworkinfo.CAorLC100Perc_SISorSITAC = ChkCA_LC_100Perc.Checked
            detailworkinfo.SITACPermitting_SISorSITAC = ChkSITAC_Permitting.Checked

            detailworkinfo.BAUT2G3G_CME = Chk2G_3G_BAUT.Checked
            detailworkinfo.SDHPDH_CME = ChkSDH_PDH.Checked
            detailworkinfo.CMEorBAST2G_CME = Chk2G_CME_BAST.Checked
            detailworkinfo.Additional_CME = ChkAdditional.Checked

            detailworkinfo.Survey_TI = ChkSurvey.Checked
            detailworkinfo.Dismantling_TI = ChkDismantling.Checked
            detailworkinfo.Reconfig_TI = ChkReconfig.Checked
            detailworkinfo.Enclosure_TI = ChkEnclosure.Checked
            detailworkinfo.Services_TI = ChkServices.Checked
            detailworkinfo.FrequencyLicense_TI = ChkFreq_License.Checked

            detailworkinfo.InitialTuning_NPO = ChkInitial_Tuning.Checked
            detailworkinfo.ClusterTuning_NPO = ChkCluster_Tuning.Checked
            detailworkinfo.IBC_NPO = ChkIBC.Checked
            detailworkinfo.Optimization_NPO = ChkOptimization.Checked
            detailworkinfo.SiteVerification_NPO = ChkSiteVerification.Checked
            detailworkinfo.DetailRFCovered_NPO = ChkDetailed_RF_Covered.Checked

            detailworkinfo.ChangeRequest_NPO = ChkChange_Request.Checked
            detailworkinfo.DesignForMW_NPO = ChkDesign_For_MW_Access.Checked
            detailworkinfo.SDHPDH_NPO = ChkNPO_SDH_PDH.Checked
            detailworkinfo.SISSES_NPO = ChkNPO_SIS_SES.Checked
            detailworkinfo.HICAP_BSC_COLO_DCS_NPO = ChkHICAP_BSC_COLO_DCS.Checked
            detailworkinfo.WCCID = Convert.ToInt32(Request.QueryString("wid"))
            WCCSubmit(Integer.Parse(DdlWorkflow.SelectedValue), detailworkinfo)
        End If
    End Sub

    Protected Sub GvSupportingDocuments_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvSupportingDocuments.RowCommand
        If (e.CommandName.Equals("uploaddoc")) Then
            Dim row As GridViewRow = CType(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            Dim LblDocName As Label = CType(row.FindControl("LblDocName"), Label)
            PrepareUploadDocument(Convert.ToInt32(e.CommandArgument.ToString()), LblDocName.Text)
        ElseIf (e.CommandName.Equals("deletedoc")) Then
            DeleteDocument(Convert.ToInt32(e.CommandArgument.ToString()))
        ElseIf (e.CommandName.Equals("deletesitedoc")) Then
            DeleteSiteDocument(Convert.ToInt32(e.CommandArgument.ToString()))
        End If
    End Sub

    Protected Sub GvSupportingDocuments_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvSupportingDocuments.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim LblParentDocType As Label = CType(e.Row.FindControl("LblParentDocType"), Label)
            Dim ViewDocLink As HtmlAnchor = CType(e.Row.FindControl("ViewDocLink"), HtmlAnchor)
            Dim LblSWId As Label = CType(e.Row.FindControl("LblSWId"), Label)
            Dim ImgUpload As ImageButton = CType(e.Row.FindControl("ImgUpload"), ImageButton)
            Dim ImgDeleteDocument As ImageButton = CType(e.Row.FindControl("ImgDeleteDocument"), ImageButton)
            Dim ImgDeleteSiteDoc As ImageButton = CType(e.Row.FindControl("ImgDeleteSiteDoc"), ImageButton)
            Dim LblDocId As Label = CType(e.Row.FindControl("LblDocId"), Label)
            Dim activityid As Integer = acontroller.GetActivityIdBaseRole(CommonSite.RollId)
            If (Not LblParentDocType Is Nothing) Then
                If (LblParentDocType.Text.ToLower.Equals("baut")) Then
                    ImgUpload.Visible = False
                    ImgDeleteDocument.Visible = False
                    ViewDocLink.HRef = "../fancybox_Form/fb_ViewDocument.aspx?swid=" & LblSWId.Text & "&parent=baut"
                    If Not ImgDeleteSiteDoc Is Nothing And Not LblDocId Is Nothing Then
                        Dim isInActivityRole As Boolean = acontroller.IsDocIsInActivityRole(Integer.Parse(LblDocId.Text), activityid, "baut")
                        If isInActivityRole = True Then
                            ImgDeleteSiteDoc.Visible = False
                        Else
                            ImgDeleteSiteDoc.Visible = True
                        End If
                    End If
                Else
                    If Not ImgDeleteSiteDoc Is Nothing And Not LblDocId Is Nothing Then
                        Dim isInActivityRole As Boolean = acontroller.IsDocIsInActivityRole(Integer.Parse(LblDocId.Text), activityid, "wcc")
                        If isInActivityRole = True Then
                            ImgDeleteSiteDoc.Visible = False
                        Else
                            ImgDeleteSiteDoc.Visible = True
                        End If
                    End If

                    If (ViewDocLink.Visible = True) Then
                        ImgDeleteDocument.Visible = True
                        ImgUpload.Visible = False
                        ViewDocLink.HRef = "../fancybox_Form/fb_ViewDocument.aspx?swid=" & LblSWId.Text & "&parent=wcc"
                    Else
                        ImgDeleteDocument.Visible = False
                        ImgUpload.Visible = True
                    End If
                End If
            End If

        End If
    End Sub

    Protected Sub LbtUploadDocument_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtUploadDocument.Click
        UploadDocument(Convert.ToInt32(HdnSiteDocId.Value), FUAdditionalDoc, errorMessage)
    End Sub

    Protected Sub LbtEditWorkflow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtEditWorkflow.Click
        LbtUpdateWorkflow.Visible = True
        LbtCancelWorkflow.Visible = True
        LbtEditWorkflow.Visible = False
        DdlWorkflow.Enabled = True
    End Sub

    Protected Sub LbtUpdateWorkflow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtUpdateWorkflow.Click
        If DdlWorkflow.SelectedIndex > 0 Then
            WorkflowUpdate(Integer.Parse(DdlWorkflow.SelectedValue), Convert.ToInt32(Request.QueryString("wid")))
            BindWorkflowType(Integer.Parse(DdlWorkflow.SelectedValue), False)
            BindApproval(DisplayWorkpackageID.Text, Integer.Parse(DdlWorkflow.SelectedValue))
        End If
    End Sub

    Protected Sub LbtCancelWorkflow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtCancelWorkflow.Click
        If DdlWorkflow.SelectedIndex > 0 Then
            BindWorkflowType(Integer.Parse(DdlWorkflow.SelectedValue), False)
            BindApproval(DisplayWorkpackageID.Text, Integer.Parse(DdlWorkflow.SelectedValue))
        End If
    End Sub

    Protected Sub DdlWorkflow_IndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlWorkflow.SelectedIndexChanged
        If (DdlWorkflow.SelectedIndex > 0) Then
            BindApproval(DisplayWorkpackageID.Text, Integer.Parse(DdlWorkflow.SelectedValue))
        End If
    End Sub

    Protected Sub LbtBackListDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtBackListDoc.Click
        MvCoreDocPanel.SetActiveView(VwListSupportingDocuments)
        FUAdditionalDoc.Controls.Clear()
        HdnSiteDocId.Value = "0"
    End Sub

    Protected Sub LbtAddDocument_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtAddDocument.Click
        BindOtherAdditionalDocument(Integer.Parse(HdnDScopeId.Value), False, Integer.Parse(ConfigurationManager.AppSettings("WCCDOCID")), HdnPackageId.Value, acontroller.GetActivityIdBaseRole(CommonSite.RollId), Convert.ToInt32(Request.QueryString("wid")))
    End Sub

    Protected Sub GvAdditionalDocument_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvAdditionalDocument.RowCommand
        If e.CommandName.Equals("adddoc") Then
            Dim row As GridViewRow = CType(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            Dim LblParentDocType As Label = CType(row.FindControl("LblParentDocType"), Label)
            If Not LblParentDocType Is Nothing Then
                Dim sitedocinfo As New WCCSitedocInfo
                sitedocinfo.DocId = Integer.Parse(e.CommandArgument.ToString())
                sitedocinfo.LMBY = CommonSite.UserId
                sitedocinfo.ParentDocType = LblParentDocType.Text
                sitedocinfo.WCCID = Convert.ToInt32(Request.QueryString("wid"))
                sitedocinfo.IsUploaded = False
                sitedocinfo.DocPath = String.Empty
                sitedocinfo.OrgDocPath = String.Empty
                AddOtherAdditionalDocument(sitedocinfo)
            End If
        End If
    End Sub

    Protected Sub LbtClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtClose.Click
        BindSiteDocuments(Convert.ToInt32(Request.QueryString("wid")))
        MvCoreDocPanel.SetActiveView(VwListSupportingDocuments)
    End Sub

    Protected Sub LbtEditPoSubcon_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtEditPoSubcon.Click
        MvPOSubcon.SetActiveView(VwPOSubconEdit)
        TxtPOSubcontractorEdit.Text = DisplayPOSubcontractor.Text
    End Sub

    Protected Sub LbtCancelEditPOSubcon_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtCancelEditPOSubcon.Click
        MvPOSubcon.SetActiveView(VwPOSubconDisplay)
        TxtPOSubcontractorEdit.Text = ""
    End Sub

    Protected Sub LbtUpdatePOSubcon_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtUpdatePOSubcon.Click
        Dim isAlreadyPOCreated As String = controller.CheckingPOSubconIsAlreadyCreated(TxtPOSubcontractorEdit.Text)
        If String.IsNullOrEmpty(isAlreadyPOCreated) Then
            controller.UpdatePOSubcon(TxtPOSubcontractorEdit.Text, Convert.ToInt32(Request.QueryString("wid")))
            MvPOSubcon.SetActiveView(VwPOSubconDisplay)
            DisplayPOSubcontractor.Text = TxtPOSubcontractorEdit.Text
            TxtPOSubcontractorEdit.Text = ""
        Else
            If (Not ClientScript.IsStartupScriptRegistered("alert")) Then
                Page.ClientScript.RegisterStartupScript _
                (Me.GetType(), "alert", "POSubconAlreadyExist('" & TxtPOSubcontractorEdit.Text & "','" & isAlreadyPOCreated & "');", True)
            End If
        End If
    End Sub

    Protected Sub LbtEditWCTRDate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtEditWCTRDate.Click
        MvWCTRDate.SetActiveView(VwWCTRDateEdit)
        TxtWCTRDate.Text = DisplayWCTRDate.Text
    End Sub

    Protected Sub LbtUpdateWCTRDate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtUpdateWCTRDate.Click
        controller.UpdateWCTRDate(TxtWCTRDate.Text, Convert.ToInt32(Request.QueryString("wid")))
        MvWCTRDate.SetActiveView(VwWCTRDateDisplay)
        DisplayWCTRDate.Text = TxtWCTRDate.Text
        TxtWCTRDate.Text = ""
    End Sub

    Protected Sub LbtCancelEditWCTRDate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtCancelEditWCTRDate.Click
        MvWCTRDate.SetActiveView(VwWCTRDateDisplay)
        TxtWCTRDate.Text = ""
    End Sub

#Region "Custom Methods"
    Private Sub BindForm(ByVal wccid As Int32)
        Dim info As WCCInfo = controller.GetODWCCBaseId(wccid)
        DisplaySubcontractorName.Text = info.SubconName
        DisplaySiteNameEPM.Text = info.SiteName
        DisplaySiteIDEPM.Text = info.SiteNo
        DisplayWorkpackageID.Text = info.PackageId
        HdnPackageId.Value = info.PackageId
        HdnDScopeId.Value = info.DScopeID
        DisplayWorkDescription.Text = info.ScopeName
        MvPOSubcon.SetActiveView(VwPOSubconDisplay)
        DisplayPOSubcontractor.Text = info.POSubcontractor
        DisplayPOTelkomsel.Text = info.PONO
        If info.BAUTDate.HasValue Then
            DisplayBAUTBASTDate.Text = String.Format("{0:dd-MMMM-yyyy}", info.BAUTDate)
        End If

        DisplayWCTRDate.Text = String.Format("{0:dd-MMMM-yyyy}", info.WCTRDate)
        MvWCTRDate.SetActiveView(VwWCTRDateDisplay)
        BindChecklist(wccid)
        BindSiteDocuments(wccid)
        BindWorkflowType(info.WorkflowId, True)
        BindApproval(info.PackageId, info.WorkflowId)
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

    Private Sub BindApproval(ByVal packageid As String, ByVal workflowid As Integer)
        GvApprovers.DataSource = controller.GetApprovalByWorkflow(packageid, workflowid)
        GvApprovers.DataBind()
    End Sub

    Private Sub BindWorkflowType(ByVal wfid As Integer, ByVal isBinding As Boolean)
        DdlWorkflow.DataSource = controller.GetWCCWorkflowGrouping()
        DdlWorkflow.DataTextField = "FlowName"
        DdlWorkflow.DataValueField = "WFID"
        DdlWorkflow.DataBind()

        DdlWorkflow.Items.Insert(0, "--workflow Type--")

        If (wfid > 0) Then
            DdlWorkflow.SelectedValue = Convert.ToString(wfid)
        End If
        LbtEditWorkflow.Visible = True
        LbtUpdateWorkflow.Visible = False
        LbtCancelWorkflow.Visible = False
        DdlWorkflow.Enabled = False
    End Sub

    Private Sub BindSiteDocuments(ByVal wccid As Int32)
        MvCoreDocPanel.SetActiveView(VwListSupportingDocuments)
        GvSupportingDocuments.DataSource = controller.GetSiteDocuments(wccid, Integer.Parse(ConfigurationManager.AppSettings("WCCDOCID")))
        GvSupportingDocuments.DataBind()
    End Sub

    Private Sub PrepareUploadDocument(ByVal sitedocid As Int32, ByVal docname As String)
        MvCoreDocPanel.SetActiveView(VwUploadDocument)
        DisplayDocName.Text = docname
        HdnSiteDocId.Value = Convert.ToString(sitedocid)
    End Sub

    Private Sub DeleteDocument(ByVal sitedocid As Int32)
        Dim auditinfo As New WCCAuditInfo
        auditinfo.WCCID = Convert.ToInt32(Request.QueryString("wid"))
        auditinfo.DocId = controller.GetSiteDocBaseSWID(sitedocid).DocId
        auditinfo.Task = -2
        auditinfo.UserId = CommonSite.UserId
        auditinfo.RoleId = CommonSite.RollId
        auditinfo.EvenStartTime = Nothing
        auditinfo.EventEndTime = Nothing
        auditinfo.Remarks = Nothing
        auditinfo.Categories = Nothing
        controller.WCCAuditTrail_I(auditinfo)
        controller.DeleteSiteDocument(sitedocid, CommonSite.UserId)
        BindSiteDocuments(Convert.ToInt32(Request.QueryString("wid")))
    End Sub

    Private Sub WorkflowUpdate(ByVal wfid As Integer, ByVal wccid As Int32)
        controller.UpdateWCCWorkflow(wccid, wfid)
    End Sub

    Sub UploadDocument(ByVal sitedocid As Int32, ByVal fuDoc As FileUpload, ByVal errorMessage As Label)
        If fuDoc.HasFile Then
            Dim FileNamePath As String = String.Empty
            Dim FileNameOnly As String = String.Empty
            Dim ReFileName As String = String.Empty
            Dim ft As String = String.Empty
            Dim path As String = String.Empty

            ft = ConfigurationManager.AppSettings("Type") & DisplayPOTelkomsel.Text & "-" & DisplayWorkpackageID.Text & "\" & "WCC\AttachmentDoc\"
            Dim fpath As String = ConfigurationManager.AppSettings("BufferPath")
            path = ConfigurationManager.AppSettings("Fpath") & DisplaySiteIDEPM.Text & ft
            Dim DocPath As String = ""
            Dim err As Boolean = False
            'Dim strResult As String = DOInsertTrans(packageid, path)

            FileNamePath = fuDoc.PostedFile.FileName
            FileNameOnly = System.IO.Path.GetFileName(fuDoc.PostedFile.FileName)
            ReFileName = Format(CDate(DateTime.Now), "ddMMyyyyHHss") & FileNameOnly
            Dim orgDocpath As String = DisplaySiteIDEPM.Text & ft & ReFileName
            DocPath = DisplaySiteIDEPM.Text & ft & LocalFileUpload.ConvertAnyFormatToPDF(fuDoc, path)

            If Not err Then
                Dim isSucceed As Boolean = controller.UploadSiteDocument(DocPath, orgDocpath, Convert.ToString(CommonSite.UserId), sitedocid)
                If isSucceed = False Then
                    Response.Write("<script language='javascript'>alert('Update failed please check your uploaded file name..');</script>")
                    Exit Sub
                Else
                    Dim auditinfo As New WCCAuditInfo
                    auditinfo.WCCID = Convert.ToInt32(Request.QueryString("wid"))
                    auditinfo.DocId = controller.GetSiteDocBaseSWID(sitedocid).DocId
                    auditinfo.Task = 1
                    auditinfo.UserId = CommonSite.UserId
                    auditinfo.RoleId = CommonSite.RollId
                    auditinfo.EvenStartTime = Nothing
                    auditinfo.EventEndTime = Nothing
                    auditinfo.Remarks = Nothing
                    auditinfo.Categories = Nothing
                    controller.WCCAuditTrail_I(auditinfo)
                    BindSiteDocuments(Convert.ToInt32(Request.QueryString("wid")))
                End If
            Else
                Response.Write("<script language='javascript'>alert('Document upload failed');</script>")
            End If
        Else
            errorMessage.Text = "Please choose File first!"
            errorMessage.ForeColor = Drawing.Color.Red
        End If
    End Sub

    Private Sub WCCSubmit(ByVal wfid As Integer, ByVal detailworkinfo As WCCDetailWorkInfo)
        controller.WCCDetailWork_I(detailworkinfo)
        Dim dtworkflow As DataTable = crcontroller.GetWorkflowDetail(wfid)
        Dim isSucceed As Boolean = True
        If dtworkflow.Rows.Count > 0 Then

            controller.DeleteWCCTransaction(Convert.ToInt32(Request.QueryString("wid")))
            controller.WCCSubmitDocument(Convert.ToInt32(Request.QueryString("wid")))
            Dim aa As Integer = 0
            Dim sorder As Integer = 0
            For aa = 0 To dtworkflow.Rows.Count - 1
                sorder = dtworkflow.Rows(aa).Item("sorder")
                Dim info As New WCCTransactionInfo
                info.WCCID = Convert.ToInt32(Request.QueryString("wid"))
                info.WFID = wfid
                info.TSKID = dtworkflow.Rows(aa).Item("Tsk_id")
                info.UGPId = dtworkflow.Rows(aa).Item("GrpId")
                info.RoleId = dtworkflow.Rows(aa).Item("RoleId")
                info.PageNo = 1
                info.LMBY = CommonSite.UserId
                info.Status = 1
                info.SOrderNo = sorder
                info.StartDateTime = Nothing
                info.EndDateTime = Nothing
                If sorder = 1 Then
                    info.Status = 0
                End If
                info.XVal = 0
                info.YVal = 0
                info.RStatus = 2
                If (controller.WCCTransaction_I(info) = False) Then
                    isSucceed = False
                    Exit For
                End If
            Next
        End If
        Dim auditinfo As New WCCAuditInfo
        auditinfo.WCCID = Convert.ToInt32(Request.QueryString("wid"))
        auditinfo.DocId = Integer.Parse(ConfigurationManager.AppSettings("WCCDOCID"))
        auditinfo.Task = 1
        auditinfo.UserId = CommonSite.UserId
        auditinfo.RoleId = CommonSite.RollId
        auditinfo.EvenStartTime = Nothing
        auditinfo.EventEndTime = Nothing
        auditinfo.Remarks = Nothing
        auditinfo.Categories = Nothing

        If isSucceed = True Then
            controller.WCCAuditTrail_I(auditinfo)
            Dim historicalinfo As New WCCHistoricalRejectionInfo
            historicalinfo.DocId = Integer.Parse(ConfigurationManager.AppSettings("WCCDOCID"))
            historicalinfo.WCCID = Convert.ToInt32(Request.QueryString("wid"))
            historicalinfo.ReuploadDate = New DateTime()
            historicalinfo.UploadUser = CommonSite.UserId
            controller.WCCRejectionHistoricalLog_Reupload(historicalinfo)
            Dim sno As Int32 = controller.GetSNOInitiator(Convert.ToInt32(Request.QueryString("wid")))
            SendMailNextUser(controller.GetWCCTaskPending_Mail(sno, "Approval"), Convert.ToInt32(Request.QueryString("wid")), String.Empty)
            If (Not ClientScript.IsStartupScriptRegistered("alert")) Then
                Page.ClientScript.RegisterStartupScript _
                            (Me.GetType(), "alert", "WCCSubmitSucceed();", True)
            End If
        Else
            controller.WCCSubmitRollBack(Convert.ToInt32(Request.QueryString("wid")), Integer.Parse(ConfigurationManager.AppSettings("WCCDOCID")))
            If (Not ClientScript.IsStartupScriptRegistered("alert")) Then
                Page.ClientScript.RegisterStartupScript _
                            (Me.GetType(), "alert", "WCCSubmitFailed();", True)
            End If
        End If
    End Sub

    Private Sub SendMailNextUser(ByVal emails As List(Of CRFramework.EmailInfo), ByVal wccid As Int32, ByVal remarks As String)
        Dim getEmailInfo As New CRFramework.EmailInfo
        Dim appruser As String = String.Empty
        Dim appvduser As String = String.Empty
        Dim appvddate As String = String.Empty
        Dim statusType As String = String.Empty
        Dim userlist As New List(Of Common_NSNFramework.UserInfo)
        For Each getEmailInfo In emails
            Dim info As New Common_NSNFramework.UserInfo
            appruser = getEmailInfo.Username
            appvduser = getEmailInfo.ExecuteBy
            appvddate = String.Format("{0:dd-MMMM-yyyy mm:hh:ss}", getEmailInfo.ExecuteDate)
            statusType = getEmailInfo.StatusType
            info.UserId = getEmailInfo.Userid
            info.Email = getEmailInfo.Email
            info.Handphone = getEmailInfo.PhoneNo
            info.UserType = ""
            userlist.Add(info)
        Next
        Dim sb As New StringBuilder
        If emails.Count > 1 Then
            sb.Append("Dear Sir / Madam,<br/>")
        Else
            sb.Append("Dear " & appruser & ", <br/>")
        End If
        Dim company As String = String.Empty
        If CommonSite.UserType = "N" Then
            company = "Nokia Siemens Networks"
        Else
            company = "Telkomsel"
        End If
        Dim wcinfo As WCCInfo = controller.GetODWCCBaseId(wccid)
        Dim emailsubject As String = String.Empty
        If statusType.Equals("Approved") Then
            sb.Append("Following detail of WCC Document already approved by " & appvduser & " " & company & "<br/>")
            emailsubject = "WCC Approved"
        ElseIf statusType.Equals("Rejected") Then
            sb.Append("Following detail of WCC Document was rejected by " & appvduser & " " & company & "<br/>")
            emailsubject = "WCC Rejected"
        Else
            sb.Append("Following detail WCCC Document is waiting your approval/review <br/>")
            emailsubject = "WCC Document Waiting"
        End If
        sb.Append("WCC No : " & wcinfo.CertificateNumber & "<br/>")
        sb.Append("SiteNo :" & wcinfo.SiteNo & "<br/>")
        sb.Append("SiteName :" & wcinfo.SiteName & "<br/>")
        sb.Append("Scope :" & wcinfo.Scope & "<br/>")
        sb.Append("PO Telkomsel :" & wcinfo.PONO & "<br/>")
        sb.Append("PO Name :" & wcinfo.POName & "<br/>")
        sb.Append("PO Partner :" & wcinfo.POSubcontractor & "<br/>")
        sb.Append("Workpackageid :" & wcinfo.PackageId & "<br/>")

        If statusType.Equals("Rejected") Then
            sb.Append("Reason of Rejection : " & remarks & "<br/><br/>")
        End If
        sb.Append("<a href='https://www.telkomsel.nsnebast.com'>Click here</a>" & " to go to e-BAST<br/>")
        sb.Append("Powered By EBAST" & "<br/>")
        sb.Append("<img src='http://www.telkomsel.nsnebast.com/images/nsn-logo.gif' alt='companylogo' />")
        objmail.SendMailUserGroup(userlist, sb.ToString(), emailsubject)
    End Sub

    Private Sub BindOtherAdditionalDocument(ByVal dscopeid As Integer, ByVal isDeleted As Boolean, ByVal wccdocid As Integer, ByVal packageid As String, ByVal activityid As Integer, ByVal wccid As Int32)
        MvCoreDocPanel.SetActiveView(VwAdditionalDocument)
        GvAdditionalDocument.DataSource = controller.GetOthersDocWCCAdditionalDocument(dscopeid, False, wccdocid, packageid, activityid, wccid)
        GvAdditionalDocument.DataBind()
    End Sub

    Private Sub AddOtherAdditionalDocument(ByVal docinfo As WCCSitedocInfo)
        controller.SiteFolder_I(docinfo)
        BindOtherAdditionalDocument(Integer.Parse(HdnDScopeId.Value), False, Integer.Parse(ConfigurationManager.AppSettings("WCCDOCID")), HdnPackageId.Value, acontroller.GetActivityIdBaseRole(CommonSite.RollId), Convert.ToInt32(Request.QueryString("wid")))
    End Sub

    Private Sub DeleteSiteDocument(ByVal sitedocid As Int32)
        controller.DeleteSiteFolderDocument(sitedocid)
        BindSiteDocuments(Convert.ToInt32(Request.QueryString("wid")))
        MvCoreDocPanel.SetActiveView(VwListSupportingDocuments)
    End Sub

#End Region

End Class
