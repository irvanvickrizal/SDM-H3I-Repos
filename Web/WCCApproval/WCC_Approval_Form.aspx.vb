Imports System.Data
Imports Common_NSNFramework
Imports common
Imports System.Collections.Generic
Imports System.IO

Partial Class WCCApproval_WCC_Approval_Form
    Inherits System.Web.UI.Page
    Private dbutils As New DBUtil
    Dim dbutils_nsn As New DBUtils_NSN
    Dim objmail As New TakeMail
    Dim controller As New WCCController
    Dim acontroller As New CODActivityController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim sm As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        'sm.RegisterPostBackControl(Me.LbtApprove)
        
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")

        If Not Page.IsPostBack Then
            If Not String.IsNullOrEmpty(Request.QueryString("wid")) Then
                MvCorePanel.SetActiveView(VwApprovalDocument)
                BindForm(Convert.ToInt32(Request.QueryString("wid")))
            End If
        End If
    End Sub

    Protected Sub LbtApproved_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtApprove.Click
        If (Not String.IsNullOrEmpty(Request.QueryString("wid"))) Then
            If Request.Browser.Browser = "IE" Then
                If (controller.WCCDocApproved(Convert.ToInt32(Request.QueryString("wid")), CommonSite.UserId, Integer.Parse(ConfigurationManager.AppSettings("WCCDOCID"))) = True) Then
                    If controller.CheckIsWCCDone(Convert.ToInt32(Request.QueryString("wid"))) Then
                        Dim seqNoString As String = controller.GetSeqNumbering(DateTime.Now.Year, DisplayWorkpackageID.Text)
                        Dim regionname As String = dbutils.ExeQueryScalarString("select top 1 cr.RgnName from codregion cr " & _
                                                                          "inner join codsite cs on cr.rgn_id = cs.rgn_id " & _
                                                                          "inner join poepmsitenew po on po.siteid = cs.site_id where po.workpackageid='" & DisplayWorkpackageID.Text & "'")

                        Dim regioncode() As String = regionname.Split("-")
                        Dim romawiMonth As String = GetRomawiMonth()
                        'Dim certificateNo As String = DisplaySiteIDEPM.Text & "/" & areaname & "/" & DateTime.Now.Year & "/" & seqNoString
                        Dim certificateNo As String = seqNoString & "/eWCC/TI/" & regioncode(0) & "/" & romawiMonth & "/" & DateTime.Now.Year.ToString()
                        Dim certificatenofilename As String = seqNoString & "_eWCC_TI_" & regioncode(0) & "_" & romawiMonth & "_" & DateTime.Now.Year.ToString()
                        controller.UpdateWCCCertificateNo(Convert.ToInt32(Request.QueryString("wid")), certificateNo)
                        controller.WCCIssuanceDate_U(Convert.ToInt32(Request.QueryString("wid")), DateTime.Now)
                        GeneratePDFForm(certificatenofilename, Convert.ToInt32(Request.QueryString("wid")))
                    End If
                    SendMailNextUser(controller.GetWCCTaskPending_Mail(GetSNOID(), "approval"), Convert.ToInt32(Request.QueryString("wid")), String.Empty)
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowsCloseWCCApprover();", True)
                Else
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error while doing the transaction.');", True)
                End If
            Else
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Please use Internet Explorer 7 or Higher');", True)
            End If
            
        End If
    End Sub

    Protected Sub LbtReject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtReject.Click
        HdnIsRejected.Value = "1"
        BindReasons()
        BindSiteDocuments(Convert.ToInt32(Request.QueryString("wid")))
        MvCorePanel.SetActiveView(vwRejectPanel)
    End Sub

    Protected Sub LbtCancelSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtCancelSubmit.Click
        HdnIsRejected.Value = "0"
        BindSiteDocuments(Convert.ToInt32(Request.QueryString("wid")))
        MvCorePanel.SetActiveView(VwApprovalDocument)
    End Sub

    Protected Sub LbtBackApproval_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtBackApproval.Click
        Response.Redirect("../Dashboard/frmSiteDocCount_WCC.aspx")
    End Sub

    Protected Sub LbtSubmitReject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtSubmitReject.Click
        Dim categories As String = String.Empty
        Dim MyItem As ListItem
        Dim countThick As Integer = 0
        Dim i As Integer
        For Each MyItem In CbList.Items
            If MyItem.Selected = True Then
                If countThick = 1 Then
                    categories = categories & ", "
                End If
                categories = categories & MyItem.Text
                countThick = 1
            End If
        Next
        Dim strTake As String = String.Empty
        For i = 0 To GvSupportingDocuments.Rows.Count - 1
            Dim chk As New HtmlInputCheckBox
            Dim swid As New Label
            chk = GvSupportingDocuments.Rows(i).Cells(1).FindControl("ChkRejected")
            swid = GvSupportingDocuments.Rows(i).Cells(1).FindControl("LblSWId")
            If Not chk Is Nothing And Not swid Is Nothing Then
                If chk.Checked = True Then
                    controller.WCCDeleteAdditionalDocument(Convert.ToInt32(swid.Text))
                End If
            End If
        Next
        SendMailNextUser(controller.GetWCCTaskPending_Mail(GetSNOID(), "Rejection"), Convert.ToInt32(Request.QueryString("wid")), "Categories : " & categories & "<br/><br/> Remarks : " & TxtRemarksReject.Text)
        SubmitSignReject(TxtRemarksReject.Text, categories)
    End Sub

    Protected Sub GvSupportingDocuments_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvSupportingDocuments.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ChkRejected As HtmlInputCheckBox = CType(e.Row.FindControl("ChkRejected"), HtmlInputCheckBox)
            Dim LblParentDocType As Label = CType(e.Row.FindControl("LblParentDocType"), Label)
            Dim LblSWId As Label = CType(e.Row.FindControl("LblSWId"), Label)
            Dim LblDocId As Label = CType(e.Row.FindControl("LblDocId"), Label)
            Dim ViewDocLink As HtmlAnchor = CType(e.Row.FindControl("ViewDocLink"), HtmlAnchor)
            If Not ChkRejected Is Nothing Then
                ChkRejected.Disabled = True
            End If

            If Not LblParentDocType Is Nothing And Not LblSWId Is Nothing And Not LblDocId Is Nothing Then
                If LblParentDocType.Text.ToLower().Equals("wcc") Then
                    If ViewDocLink.Visible = True Then
                        ViewDocLink.HRef = "../fancybox_Form/fb_ViewDocument.aspx?swid=" & LblSWId.Text & "&parent=wcc"
                    End If
                    If HdnIsRejected.Value.Equals("1") Then
                        ChkRejected.Disabled = False
                    Else
                        ChkRejected.Disabled = True
                    End If

                Else
                    ChkRejected.Disabled = True
                    ViewDocLink.HRef = "../fancybox_Form/fb_ViewDocument.aspx?swid=" & LblSWId.Text & "&parent=baut"
                End If

            End If
        End If
    End Sub

#Region "Custom Methods"

    Private Sub BindForm(ByVal wccid As Int32)
        divPrint.Visible = False
        Dim info As WCCInfo = controller.GetODWCCBaseId(wccid)
        DisplaySubcontractorName.Text = info.SubconName
        If info.IssuanceDate.HasValue Then
            DisplayWCCIssuanceDate.Text = String.Format("{0:dd-MMMM-yyyy}", info.IssuanceDate)
        End If

        DisplaySiteNameEPM.Text = info.SiteName
        DisplaySiteIDEPM.Text = info.SiteNo
        DisplayWorkpackageID.Text = info.PackageId
        HdnPackageId.Value = info.PackageId
        HdnIsRejected.Value = "0"
        DisplayWorkDescription.Text = info.ScopeName
        DisplayPOSubcontractor.Text = info.POSubcontractor
        DisplayPOTelkomsel.Text = info.PONO
        GetLabelDocAcceptance(info.ActivityId, DisplayDocAcceptanceLabel)
        If info.BAUTDate.HasValue Then
            LbtApprove.Attributes.Add("onclick", "javascript:return BautApproved();")
            DisplayBAUTBASTDate.Text = String.Format("{0:dd-MMMM-yyyy}", info.BAUTDate)
        Else
            GetDocAcceptanceDate(info.PackageId, info.ActivityId)
        End If

        DisplayWCTRDate.Text = String.Format("{0:dd-MMMM-yyyy}", info.WCTRDate)
        BindChecklist(wccid)
        BindSiteDocuments(wccid)
        BindApprovalDocument(wccid)
    End Sub

    Private Sub BindFinalWCC(ByVal wccid As Int32)
        Dim info As WCCInfo = controller.GetODWCCBaseId(wccid)
        LblSubconNamePrint.Text = info.SubconName
        LblWCCIssuanceDatePrint.Text = String.Format("{0:dd-MMMM-yyyy}", info.IssuanceDate)
        LblCertificateNoPrint.Text = info.CertificateNumber
        LblSiteNamePrint.Text = info.SiteName
        LblSiteIDPrint.Text = info.SiteNo
        LblWPIDPrint.Text = info.PackageId
        LblWorkDescriptionPrint.Text = info.ScopeName
        LblPOSubconPrint.Text = info.POSubcontractor
        LblPOTselPrint.Text = info.PONO
        LblDocAcceptanceDateTypePrint.Text = DisplayDocAcceptanceLabel.Text
        LblDocAcceptanceDatePrint.Text = String.Format("{0:dd-MMMM-yyyy}", info.BAUTDate)
        DisplayWCTRDate.Text = String.Format("{0:dd-MMMM-yyyy}", info.WCTRDate)
        BindFinalChecklist(wccid)
        Dim dtapprovalsPrint As DataTable = controller.WCCGetApprovalDocument(wccid)
        Dim kt As Integer
        Dim strApproval As String = String.Empty
        If dtapprovalsPrint.Rows.Count > 0 Then
            Dim rowcount As Integer = 1
            For kt = 0 To dtapprovalsPrint.Rows.Count - 1
                strApproval += rowcount.ToString & ". " & dtapprovalsPrint.Rows(kt).Item("TaskEvent") & " by <b>" & dtapprovalsPrint.Rows(kt).Item("name") & "-" & dtapprovalsPrint.Rows(kt).Item("SignTitle") & "</b> on " & String.Format("{0:dd-MMMM-yyyy}", Convert.ToDateTime(dtapprovalsPrint.Rows(kt).Item("ApprovalDate"))) & " <br/>"
                rowcount += 1
            Next
            dvListApprovalPrint.InnerHtml = strApproval
        Else
            dvListApprovalPrint.InnerText = "Not Yet Approved by users"
        End If
    End Sub

    Private Sub BindFinalChecklist(ByVal wccid As Int32)
        Dim info As WCCDetailWorkInfo = controller.GetWCCDetailWork(wccid)
        If Not info Is Nothing Then
            ChkSISorSESPrint.Checked = info.SISorSES_SISorSITAC
            ChkPKSorAJBPrint.Checked = info.PKSorAJB50Perc_SISorSITAC
            Chk50PercIMBPrint.Checked = info.IMB50Perc_SISorSITAC
            Chk100PercCAorLCPrint.Checked = info.CAorLC100Perc_SISorSITAC
            ChkSitacorPermittingPrint.Checked = info.SITACPermitting_SISorSITAC

            Chk2Gor3GBAUTPrint.Checked = info.BAUT2G3G_CME
            ChkSDHorPDHPrint.Checked = info.SDHPDH_CME
            Chk2GCMEBastPrint.Checked = info.CMEorBAST2G_CME
            ChkAdditionalPrint.Checked = info.Additional_CME

            ChkSurveyPrint_TI.Checked = info.Survey_TI
            ChkDismantlingPrint_TI.Checked = info.Dismantling_TI
            ChkReconfigPrint_TI.Checked = info.Reconfig_TI
            ChkEnclosurePrint_TI.Checked = info.Enclosure_TI
            ChkServicesPrint_TI.Checked = info.Services_TI
            ChkFreqLicensePrint_TI.Checked = info.FrequencyLicense_TI

            ChkInitialTuningPrint_NPO.Checked = info.InitialTuning_NPO
            ChkClusterPrint_NPO.Checked = info.ClusterTuning_NPO
            ChkIBCPrint_NPO.Checked = info.IBC_NPO
            ChkOptimizationPrint_NPO.Checked = info.Optimization_NPO
            ChkSiteVerificationPrint_NPO.Checked = info.SiteVerification_NPO
            ChkDetailRFCovAndCapNWPPrint_NPO.Checked = info.DetailRFCovered_NPO

            ChkChangeReqPrint.Checked = info.ChangeRequest_NPO
            ChkDesignForMWAccessPrint.Checked = info.DesignForMW_NPO
            ChkSDHorPDHPrint_NPO.Checked = info.SDHPDH_NPO
            ChkSISorSESPrint_NPO.Checked = info.SISSES_NPO
            ChkHICAPorBSCorCOLOorDCSPrint_NPO.Checked = info.HICAP_BSC_COLO_DCS_NPO
        End If
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

    Private Sub BindSiteDocuments(ByVal wccid As Int32)
        GvSupportingDocuments.DataSource = controller.GetSiteDocuments(wccid, Integer.Parse(ConfigurationManager.AppSettings("WCCDOCID")))
        GvSupportingDocuments.DataBind()
    End Sub

    Private Sub SubmitSignReject(ByVal remarks As String, ByVal catReason As String)
        If (controller.WCCDocRejected(Convert.ToInt32(Request.QueryString("wid")), CommonSite.UserId, Integer.Parse(ConfigurationManager.AppSettings("WCCDOCID")), catReason, remarks) = True) Then
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowsCloseWCCRejection();", True)
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error while doing the transaction.');", True)
        End If

    End Sub

    Sub BindReasons()
        CbList.DataSource = dbutils_nsn.GetReasons(ConfigurationManager.AppSettings("ATP"))
        CbList.DataTextField = "ReasonName"
        CbList.DataValueField = "ReasonId"
        CbList.DataBind()
    End Sub

    Private Sub SendMailNextUser(ByVal emails As List(Of CRFramework.EmailInfo), ByVal wccid As Int32, ByVal remarks As String)
        Dim getEmailInfo As New CRFramework.EmailInfo
        Dim appruser As String = String.Empty
        Dim appvduser As String = String.Empty
        Dim appvddate As String = String.Empty
        Dim statusType As String = String.Empty
        Dim userlist As New List(Of UserInfo)
        For Each getEmailInfo In emails
            Dim info As New UserInfo
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
            Dim rejectorname As String = dbutils.ExeQueryScalarString("select top 1 name from ebastusers_1 where usr_id=" & CommonSite.UserId)
            sb.Append("Following detail of WCC Document was rejected by " & rejectorname & " " & company & "<br/>")
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

    Private Function GetSNOID() As Int32
        If Not String.IsNullOrEmpty(Request.QueryString("sno")) Then
            Return Convert.ToInt32(Request.QueryString("sno"))
        Else
            Return 0
        End If
    End Function

    Private Sub GetLabelDocAcceptance(ByVal activityid As Integer, ByVal labeldocacceptance As Label)
        Dim lockinfo As CODDocActivityLockInfo = acontroller.GetDocActivityLockBaseActivity(activityid)
        labeldocacceptance.Text = lockinfo.Disclaimer
    End Sub

    Private Function GeneratePDFForm(ByVal certificateno As String, ByVal wccid As Int32) As Boolean
        divPrint.Visible = True
        BindFinalWCC(wccid)
        divChecking.Visible = False
        Dim FileNameOnly As String
        Dim Filenameorg As String
        Dim ReFilename As String
        Dim secpath As String = ""
        Dim ft As String
        Dim path As String
        FileNameOnly = "-WCC-"
        Filenameorg = Format(CDate(DateTime.Now), "ddMMyyyyHHss") & FileNameOnly & certificateno
        ReFilename = Filenameorg & ".htm"
        ft = ConfigurationManager.AppSettings("Type") & DisplayPOTelkomsel.Text & "-" & Request.QueryString("wpid") & "\" & "WCC\"
        path = ConfigurationManager.AppSettings("Fpath") & DisplaySiteIDEPM.Text & ft
        Dim DocPath As String = ""
        Dim orgDocPath As String = ""
        DocPath = DisplaySiteIDEPM.Text & ft & secpath & CreatePDFFile(Replace(path, "  \", "\"), certificateno)
        orgDocPath = DisplaySiteIDEPM.Text & ft & secpath & ReFilename
        controller.UpdateDocPath(Convert.ToInt32(Request.QueryString("wid")), DocPath, orgDocPath)
    End Function

    Private Function CreatePDFFile(ByVal StrPath As String, ByVal certificateno As String) As String

        StrPath = StrPath.Replace(" \", "\").ToString 'bugfix100816 causing error when the path containing space before \
        Dim filenameorg As String
        Dim ReFileName As String
        filenameorg = Format(CDate(DateTime.Now), "ddMMyyyyHHss") & "-WCC-" & certificateno
        ReFileName = filenameorg & ".htm"
        If (System.IO.File.Exists(StrPath & ReFileName)) Then
            System.IO.File.Delete(StrPath & ReFileName)
        End If
        If Not System.IO.Directory.Exists(StrPath) Then
            System.IO.Directory.CreateDirectory(StrPath)
        End If

        Dim sw As HtmlTextWriter = New HtmlTextWriter(New StreamWriter(StrPath & ReFileName, False, System.Text.UnicodeEncoding.UTF8))
        sw.WriteLine("<html><head><style type=""text/css"">")
        sw.WriteLine(".panelDetail{margin-top:20px;padding:30px;background:#FFF;width:960px;height: 700px;}")
        sw.WriteLine(".headerTitle{font-family:Verdana;font-size:18px;font-weight:bolder;}")
        sw.WriteLine(".formPanel{width:960px;text-align:center;margin-top:10px;}")
        sw.WriteLine(".formPanel2{width:960px;margin-top: 10px; border-bottom-color: #000;border-bottom-style: dashed;border-bottom-width: 1px; padding-bottom: 5px;}")
        sw.WriteLine(".labelText{font-family:verdana;font-size:12px;text-align:left;}")
        sw.WriteLine(".labelFieldText{font-family:verdana;font-size:12px;font-weight:bolder;text-align:left;}")
        sw.WriteLine(".divCenter{margin-top:5px;text-align:center;}")
        sw.WriteLine(".lblRemarks{font-family: Verdana; font-size: 11px; font-weight: bolder; text-align: center;}")
        sw.WriteLine("</style></head>")
        sw.WriteLine("<body>")
        divPrint.RenderControl(sw)
        sw.WriteLine("</body>")
        sw.WriteLine("</html>")
        sw.Close()
        sw.Dispose()
        'Return Common.TakeFileUpload.ConvertAnyFormatToPDFHtmlNew(StrPath & ReFileName, StrPath, filenameorg)
        Return "null"
    End Function

    Private Function GetRomawiMonth() As String
        Dim romawi As String = String.Empty
        Dim month As Integer = DateTime.Now.Month
        If month = 1 Then
            romawi = "I"
        ElseIf month = 2 Then
            romawi = "II"
        ElseIf month = 3 Then
            romawi = "III"
        ElseIf month = 4 Then
            romawi = "IV"
        ElseIf month = 5 Then
            romawi = "V"
        ElseIf month = 6 Then
            romawi = "VI"
        ElseIf month = 7 Then
            romawi = "VII"
        ElseIf month = 8 Then
            romawi = "VIII"
        ElseIf month = 9 Then
            romawi = "IX"
        ElseIf month = 10 Then
            romawi = "X"
        ElseIf month = 11 Then
            romawi = "XI"
        ElseIf month = 12 Then
            romawi = "XII"
        End If
        Return romawi
    End Function

    Private Sub GetDocAcceptanceDate(ByVal packageid As String, ByVal activityid As Integer)
        Dim lockinfo As CODDocActivityLockInfo = acontroller.GetDocActivityLockBaseActivity(activityid)
        Dim acceptancedate As System.Nullable(Of DateTime) = controller.GetDocAcceptanceDate(packageid, lockinfo.DocId)
        If lockinfo.DocId = Integer.Parse(ConfigurationManager.AppSettings("BAUTID")) Then
            If acceptancedate.HasValue Then
                LbtApprove.Attributes.Add("onclick", "javascript:return BautApproved();")
                DisplayBAUTBASTDate.Text = String.Format("{0:dd-MMMM-yyyy}", acceptancedate)
                controller.UpdateAcceptanceDate(acceptancedate, Request.QueryString("wid"))
            Else
                LbtApprove.Attributes.Add("onclick", "javascript:return BautNotYetApproved();")
            End If
        End If
    End Sub

#End Region

End Class
