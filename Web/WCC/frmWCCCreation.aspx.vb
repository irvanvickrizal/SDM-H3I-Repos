Imports System.Data
Imports System.Collections.Generic
Imports Common

Partial Class WCC_frmWCCCreation
    Inherits System.Web.UI.Page
    Dim subconid As Integer
    Dim scopecontroller As New ScopeController
    Dim wcccontroller As New WCCController
    Dim acontroller As New CODActivityController
    Dim objdb As New DBUtil

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            LbtSave.Visible = True
            LbtSaveWithoutBAUT.Visible = False
            subconid = PartnerController.GetSubconIdByUser(CommonSite.UserId)
            If subconid = 0 Then
                Response.Redirect("~/USR/frmPartnerSetup.aspx")
            Else
                hdnWCCID.Value = "0"
                HdnSubconId.Value = Convert.ToString(subconid)
                DisplayCompanyName.Text = PartnerController.GetSubconNameBySubconId(subconid)
            End If
        End If
    End Sub

    Protected Sub BtnSearchClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtSearch.Click
        Dim isAuthorized As Integer = objdb.ExeQueryScalar("select count(*) from WCCDocCreation_Authority where role_id=" & CommonSite.RollId)
        If isAuthorized > 0 Then
            If Not String.IsNullOrEmpty(TxtPackageId.Text) Then
                SearchData(TxtPackageId.Text)
            Else
                If (Not ClientScript.IsStartupScriptRegistered("alert")) Then
                    Page.ClientScript.RegisterStartupScript _
                    (Me.GetType(), "alert", "NoWPIDSearch();", True)
                End If
            End If
        Else
            If (Not ClientScript.IsStartupScriptRegistered("alert")) Then
                Page.ClientScript.RegisterStartupScript _
                (Me.GetType(), "alert", "forbiddenCreation();", True)
            End If
        End If
        
    End Sub

    Protected Sub DdlGeneralScope_IndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlGeneralScope.SelectedIndexChanged
        If (DdlGeneralScope.SelectedIndex > 0) Then
            BindScopeDetail(DdlScopeDetail, Integer.Parse(DdlGeneralScope.SelectedValue))
        Else
            BindScopeDetail(DdlScopeDetail, 0)
        End If
    End Sub

    

    Protected Sub BtnSaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtSave.Click
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")

        Dim info As New WCCInfo
        info.WCCID = Convert.ToInt32(hdnWCCID.Value)
        info.DScopeID = Integer.Parse(DdlScopeDetail.SelectedValue)


        info.PackageId = DisplayWPID.Text
        info.SCONID = Integer.Parse(HdnSubconId.Value)
        info.WCTRDate = DateTime.ParseExact(TxtWCTRDate.Text, "dd-MMMM-yyyy", Nothing)
        info.POSubcontractor = TxtPOSubcontractor.Text
        info.LMBY = CommonSite.UserId
        info.ActivityId = acontroller.GetActivityIdBaseRole(CommonSite.RollId)
        Dim isReadySaved As Boolean = True
        If MvCorePanelAcceptanceDate.ActiveViewIndex() = 1 Then
            If (String.IsNullOrEmpty(TxtAcceptanceDateNY.Text)) Then
                isReadySaved = False
                LblWarningMessageAcceptanceDate.Text = "Please fill Document (BAUT/QC) Acceptance Date!"
                LblWarningMessageAcceptanceDate.Visible = True
            Else

                info.BAUTDate = DateTime.ParseExact(TxtAcceptanceDateNY.Text, "dd-MMMM-yyyy", Nothing)
                LblWarningMessageAcceptanceDate.Visible = False
            End If
        Else
            If String.IsNullOrEmpty(TxtBAUTBASTDate.Text) Then
                info.BAUTDate = Nothing
            Else
                info.BAUTDate = DateTime.ParseExact(TxtBAUTBASTDate.Text, "dd-MMMM-yyyy", Nothing)
            End If

        End If

        Dim wccid As Int32 = 0
        If isReadySaved = True Then
            wccid = wcccontroller.ODWCC_I(info)
        End If

        'Dim wccid As Int32 = 4
        'info.DScopeID = 2
        'info.PackageId = "6189380"
        If (wccid > 0) Then
            hdnWCCID.Value = Convert.ToString(wccid)
            BindSupportingDocument(info.DScopeID, info.PackageId, acontroller.GetActivityIdBaseRole(CommonSite.RollId))
            LbtSave.Visible = False
            LbtEdit.Visible = True
            LbtCreateFolder.Visible = True
        End If
    End Sub


    Protected Sub LbtSaveWithoutBAUT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtSaveWithoutBAUT.Click
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")

        Dim info As New WCCInfo
        info.WCCID = Convert.ToInt32(hdnWCCID.Value)
        info.DScopeID = Integer.Parse(DdlScopeDetail.SelectedValue)


        info.PackageId = DisplayWPID.Text
        info.SCONID = Integer.Parse(HdnSubconId.Value)
        info.WCTRDate = DateTime.ParseExact(TxtWCTRDate.Text, "dd-MMMM-yyyy", Nothing)
        info.POSubcontractor = TxtPOSubcontractor.Text
        info.LMBY = CommonSite.UserId
        info.ActivityId = acontroller.GetActivityIdBaseRole(CommonSite.RollId)
        Dim isReadySaved As Boolean = True
        If MvCorePanelAcceptanceDate.ActiveViewIndex() = 1 Then
            If (String.IsNullOrEmpty(TxtAcceptanceDateNY.Text)) Then
                isReadySaved = False
                LblWarningMessageAcceptanceDate.Text = "Please fill Document (BAUT/QC) Acceptance Date!"
                LblWarningMessageAcceptanceDate.Visible = True
            Else

                info.BAUTDate = DateTime.ParseExact(TxtAcceptanceDateNY.Text, "dd-MMMM-yyyy", Nothing)
                LblWarningMessageAcceptanceDate.Visible = False
            End If
        Else
            If String.IsNullOrEmpty(TxtBAUTBASTDate.Text) Then
                info.BAUTDate = Nothing
            Else
                info.BAUTDate = DateTime.ParseExact(TxtBAUTBASTDate.Text, "dd-MMMM-yyyy", Nothing)
            End If

        End If

        Dim wccid As Int32 = 0
        If isReadySaved = True Then
            wccid = wcccontroller.ODWCC_I(info)
        End If

        'Dim wccid As Int32 = 4
        'info.DScopeID = 2
        'info.PackageId = "6189380"
        If (wccid > 0) Then
            hdnWCCID.Value = Convert.ToString(wccid)
            BindSupportingDocument(info.DScopeID, info.PackageId, acontroller.GetActivityIdBaseRole(CommonSite.RollId))
            LbtSave.Visible = False
            LbtEdit.Visible = True
            LbtCreateFolder.Visible = True
        End If
    End Sub

    Protected Sub LbtCreateFolder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtCreateFolder.Click
        Dim j As Integer
        For j = 0 To GvSupportingDocuments.Rows.Count - 1
            Dim chk As New CheckBox
            chk = CType(GvSupportingDocuments.Rows(j).Cells(0).FindControl("ChkChecked"), CheckBox)
            If chk.Checked = True Then
                Dim ParentDocType As Label = CType(GvSupportingDocuments.Rows(j).Cells(0).FindControl("LblParentDocType"), Label)
                Dim DocPath As Label = CType(GvSupportingDocuments.Rows(j).Cells(0).FindControl("LblDocPath"), Label)
                Dim OrgDocPath As Label = CType(GvSupportingDocuments.Rows(j).Cells(0).FindControl("LblOrgDocPath"), Label)
                Dim docid As Label = CType(GvSupportingDocuments.Rows(j).Cells(0).FindControl("LblDocId"), Label)
                Dim ViewDocLink As HtmlAnchor = CType(GvSupportingDocuments.Rows(j).Cells(0).FindControl("ViewDocLink"), HtmlAnchor)
                If Not ParentDocType Is Nothing And Not DocPath Is Nothing And Not docid Is Nothing Then
                    Dim sitedocinfo As New WCCSitedocInfo
                    sitedocinfo.DocId = Integer.Parse(docid.Text)
                    sitedocinfo.LMBY = CommonSite.UserId
                    sitedocinfo.ParentDocType = ParentDocType.Text
                    sitedocinfo.WCCID = Convert.ToInt32(hdnWCCID.Value)
                    If ViewDocLink.Visible = True Then
                        sitedocinfo.IsUploaded = True
                        sitedocinfo.DocPath = DocPath.Text
                        sitedocinfo.OrgDocPath = OrgDocPath.Text
                    Else
                        sitedocinfo.IsUploaded = False
                        sitedocinfo.DocPath = String.Empty
                        sitedocinfo.OrgDocPath = String.Empty
                    End If
                    sitedocinfo.Rstatus = 2
                    wcccontroller.SiteFolder_I(sitedocinfo)
                End If
            End If
        Next
        Response.Redirect("frmODWCCFinal.aspx?wid=" & hdnWCCID.Value)
    End Sub

    Protected Sub GvSupportingDocuments_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvSupportingDocuments.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim LblParentDocType As Label = CType(e.Row.FindControl("LblParentDocType"), Label)
            Dim LblDocId As Label = CType(e.Row.FindControl("LblDocId"), Label)
            If (Not LblParentDocType Is Nothing) Then
                Dim ViewDocLink As HtmlAnchor = CType(e.Row.FindControl("ViewDocLink"), HtmlAnchor)
                If Not ViewDocLink Is Nothing Then
                    If LblParentDocType.Text.ToLower().Equals("baut") Then
                        ViewDocLink.Visible = True
                        ViewDocLink.HRef = "../fancybox_Form/fb_ViewDocument.aspx?docid=" & LblDocId.Text & "&wpid=" & TxtWorkpackageId.Text
                    Else
                        ViewDocLink.Visible = False
                    End If
                End If
            End If
        End If
    End Sub

    Protected Sub GvWCCList_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvWCCList.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim opennewframe As HtmlAnchor = CType(e.Row.FindControl("opennewframe"), HtmlAnchor)
            Dim aDeletedWCC As HtmlAnchor = CType(e.Row.FindControl("aDeletedWCC"), HtmlAnchor)
            Dim LblPackageId As Label = CType(e.Row.FindControl("LblPackageId"), Label)
            Dim LblWCCID As Label = CType(e.Row.FindControl("LblWCCID"), Label)


            opennewframe.HRef = "~/fancybox_Form/fb_SiteInformation.aspx?pid=" & LblPackageId.Text

            If Not aDeletedWCC Is Nothing Then
                aDeletedWCC.HRef = "~/Dashboard_WCC/WCCDeletion.aspx?wid=" & LblWCCID.Text
            End If

            Dim ImgEdit As ImageButton = CType(e.Row.FindControl("ImgEdit"), ImageButton)

            If Not ImgEdit Is Nothing Then
                If wcccontroller.IsWCCDone(Convert.ToInt32(LblWCCID.Text)) Then
                    ImgEdit.Visible = False
                Else
                    ImgEdit.Visible = True
                End If
            End If

        End If
    End Sub

    Protected Sub GVWCCList_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvWCCList.RowCommand
        If e.CommandName.Equals("EditWCC") Then
            If wcccontroller.WCCCheckingProcess(Convert.ToInt32(e.CommandArgument.ToString())) = 1 Then
                Response.Redirect("frmODWCCFinal.aspx?wid=" & e.CommandArgument.ToString())
            Else
                Response.Redirect("frmWCCPreparation.aspx?wid=" & e.CommandArgument.ToString())
            End If
        ElseIf e.CommandName.Equals("deletewcc") Then
            HdnWCCId_Del.Value = e.CommandArgument.ToString()
            BindDeleteData(Convert.ToInt32(e.CommandArgument.ToString()))

        End If
    End Sub

    Protected Sub LbtEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtEdit.Click
        LbtSave.Visible = True
        LbtEdit.Visible = False
        LbtCreateFolder.Visible = False
    End Sub

    Protected Sub LbtDeleteWCC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtDeleteWCC.Click
        wcccontroller.WCCDeletionFlag(Convert.ToInt32(HdnWCCId_Del.Value), TxtRemarks.Text, True, CommonSite.UserId)
        SearchData(TxtPackageId.Text)
        If (Not ClientScript.IsStartupScriptRegistered("alert")) Then
            Page.ClientScript.RegisterStartupScript _
            (Me.GetType(), "alert", "WCCDeleteSucceed();", True)
        End If
    End Sub

#Region "Custom Methods"
    Private Sub BindSupportingDocument(ByVal dscopeid As Integer, ByVal packageid As String, ByVal activityid As Integer)
        MvCorePanelDocInitiatilation.SetActiveView(VwBAUTDocInitialize)
        GvSupportingDocuments.DataSource = wcccontroller.GetDocWCCAdditionalDocument(dscopeid, False, Integer.Parse(ConfigurationManager.AppSettings("WCCDOCID")), packageid, activityid)
        GvSupportingDocuments.DataBind()
    End Sub

    Private Sub SaveData()
        Dim info As New WCCConfigInfo
        info.PackageId = TxtPackageId.Text
        'info.DocRequiredId = Integer.Parse(DdlDocumentRequired.SelectedValue)
        info.QCRequired = False
        info.ATPRequired = True
        info.DScopeId = Integer.Parse(DdlScopeDetail.SelectedValue)
        info.LMBY = CommonSite.UserId
        info.SubconId = PartnerController.GetSubconIdByUser(CommonSite.UserId)
        info.WCCConfigId = 0
        wcccontroller.WCCConfigIU(info)
    End Sub

    Private Sub BindDocRequired(ByVal packageid As String)
        'DdlDocumentRequired.DataSource = wcccontroller.GetRequiredDocWCC(packageid, Integer.Parse(HdnSubconId.Value))
        'DdlDocumentRequired.DataTextField = "RequiredDoc"
        'DdlDocumentRequired.DataValueField = "DocReqId"
        'DdlDocumentRequired.DataBind()
        'DdlDocumentRequired.Items.Insert(0, "-- Document Required --")
    End Sub

    Private Sub BindSiteAtt(ByVal packageid As String)
        Dim getSiteAttributeQuery As String = "select  TselProjectID,SiteNo, PoNo,POName, Workpackageid,Scope,sitename,siteidpo,sitenamepo from poepmsitenew where workpackageid = '" & packageid & "'"
        Dim dtSiteAtt As DataTable = objdb.ExeQueryDT(getSiteAttributeQuery, "siteAtt")
        If dtSiteAtt.Rows.Count > 0 Then
            MvCorePanel.SetActiveView(VwWCCConfig)
            DisplayPoNo.Text = dtSiteAtt.Rows(0).Item(2)
            DisplayPOName.Text = objdb.ExeQueryScalarString("select top 1 poname from poepmsitenew where pono='" & DisplayPoNo.Text & "' and potype is not null")
            DisplaySiteNo.Text = dtSiteAtt.Rows(0).Item(1)
            DisplaySiteName.Text = dtSiteAtt.Rows(0).Item(6)
            DisplaySiteNoPO.Text = dtSiteAtt.Rows(0).Item(7)
            DisplaySiteNamePO.Text = dtSiteAtt.Rows(0).Item(8)
            DisplayScope.Text = dtSiteAtt.Rows(0).Item(5)
            DisplayWPID.Text = dtSiteAtt.Rows(0).Item(4)
            DisplayProjectID.Text = dtSiteAtt.Rows(0).Item(0)
            aSiteInformation.HRef = "../fancybox_Form/fb_SiteInformation.aspx?pid=" & packageid
            TxtSubconCompany.Text = DisplayCompanyName.Text
            'TxtWccIssuanceDate.Text = String.Format("{0:dd-MMMM-yyyy}", DateTime.Now)
            TxtSiteID.Text = DisplaySiteNo.Text
            TxtSiteName.Text = DisplaySiteName.Text
            TxtWorkpackageId.Text = DisplayWPID.Text
            TxtWorkDescription.Text = objdb.ExeQueryScalarString("select top 1 packagename from epmdata where workpackageid='" & DisplayWPID.Text & "'")
            TxtPOTelkomsel.Text = DisplayPoNo.Text
            'TxtBAUTBASTDate.Text = String.Format("{0:dd-MMMM-yyyy}", wcccontroller.GetWCCBautApproved(DisplayWPID.Text, Integer.Parse(ConfigurationManager.AppSettings("BAUTID"))))
            DocUnlockedWCC(packageid, acontroller.GetActivityIdBaseRole(CommonSite.RollId))
            BindDocRequired(packageid)
            BindGeneralScope(DdlGeneralScope)
            If DdlGeneralScope.SelectedIndex < 1 Then
                BindScopeDetail(DdlScopeDetail, 0)
            End If
        Else
            MvCorePanel.SetActiveView(VwWCCConfig)
        End If
    End Sub

    Private Sub SearchData(ByVal packageid As String)
        Dim rowCount As Integer = wcccontroller.CheckingAvailableWPID(packageid, CommonSite.UserId)
        Dim activityid As Integer = acontroller.GetActivityIdBaseRole(CommonSite.RollId)
        If (rowCount) > 0 Then
            Dim WCCListCreation As List(Of WCCInfo) = wcccontroller.GetODWCCBaseOnPackageSubconId(packageid, Integer.Parse(HdnSubconId.Value), CommonSite.RollId)
            If WCCListCreation.Count > 0 Then
                MvCorePanel.SetActiveView(VwWCCList)
                GvWCCList.DataSource = WCCListCreation
                GvWCCList.DataBind()
            Else
                Dim doclockid As Integer = acontroller.GetDocIDBaseActivityRole(activityid)
                If doclockid = Integer.Parse(ConfigurationManager.AppSettings("BAUTID")) Then
                    Dim isBAUTApproved = wcccontroller.IsBAUTApproved(Integer.Parse(ConfigurationManager.AppSettings("BAUTID")), packageid)
                    'isBAUTApproved = True 'unclock BAUT Approved to handle dismantle scope 
                    If isBAUTApproved = True Then
                        BindSiteAtt(packageid)
                        'CheckingATPRequired(packageid, Integer.Parse(HdnSubconId.Value))
                        'CheckingQCRequired(packageid, Integer.Parse(HdnSubconId.Value))
                    Else
                        '   MvCorePanel.SetActiveView(VwBAUTNotApproved)
                        'If (Not ClientScript.IsStartupScriptRegistered("alert")) Then
                        '    Page.ClientScript.RegisterStartupScript _
                        '    (Me.GetType(), "alert", "BautNotYetApproved();", True)
                        'End If
                        BindSiteAtt(packageid)
                        LbtSave.Visible = False
                        LbtSaveWithoutBAUT.Visible = True
                        LbtSaveWithoutBAUT.Attributes.Add("onclick", "javascript:return BautNotYetApproved();")

                    End If
                Else
                    BindSiteAtt(packageid)
                End If
            End If
        Else
            MvCorePanel.SetActiveView(VwNotFound)
            If (Not ClientScript.IsStartupScriptRegistered("alert")) Then
                Page.ClientScript.RegisterStartupScript _
                (Me.GetType(), "alert", "WPIDNotFound();", True)
            End If
        End If
    End Sub

    Private Sub BindGeneralScope(ByVal ddl As DropDownList)
        ddl.DataSource = scopecontroller.GetScopeMaster(False)
        ddl.DataTextField = "ScopeName"
        ddl.DataValueField = "ScopeId"
        ddl.DataBind()
        DdlGeneralScope.Items.Insert(0, "-- General Scope --")
    End Sub

    Private Sub BindScopeDetail(ByVal ddl As DropDownList, ByVal gscopeid As Integer)
        ddl.DataSource = scopecontroller.GetAllDetailScopes(False, gscopeid)
        ddl.DataTextField = "DscopeName"
        ddl.DataValueField = "DScopeId"
        ddl.DataBind()
        ddl.Items.Insert(0, "-- Select Scope Detail --")
    End Sub

    Private Sub IsWCCAvailable(ByVal packageid As String)

    End Sub

    Private Sub CheckingATPRequired(ByVal packageid As String, ByVal subconid As Integer)
        If wcccontroller.IsATPorQCDone(Integer.Parse(ConfigurationManager.AppSettings("ATP")), packageid) = False Then
            ' ChkATPRequired.Text = "Not Yet Approved"
            ' ChkATPRequired.Enabled = False
        Else
            'ChkATPRequired.Text = ""
            'ChkATPRequired.Enabled = True
            If wcccontroller.IsATPAvailable(packageid, subconid) = False Then
                'ChkATPRequired.Checked = True
                'ChkATPRequired.Enabled = False
            End If
        End If
    End Sub

    Private Sub CheckingQCRequired(ByVal packageid As String, ByVal subconid As Integer)
        If wcccontroller.IsATPorQCDone(Integer.Parse(ConfigurationManager.AppSettings("QCID")), packageid) = False Then
            'ChkQCRequired.Text = "Not Yet Approved"
            'ChkQCRequired.Enabled = False
        Else
            'ChkQCRequired.Text = ""
            'ChkQCRequired.Enabled = True
            If wcccontroller.IsQCAvailable(packageid, subconid) = False Then
                'ChkATPRequired.Checked = True
                'ChkATPRequired.Enabled = False
            End If
        End If
    End Sub

    Private Sub DocUnlockedWCC(ByVal packageid As String, ByVal activityid As Integer)
        Dim lockinfo As CODDocActivityLockInfo = acontroller.GetDocActivityLockBaseActivity(activityid)
        Dim acceptancedate As System.Nullable(Of DateTime) = wcccontroller.GetDocAcceptanceDate(packageid, lockinfo.DocId)
        LblAcceptanceDate.Text = lockinfo.Disclaimer
        If acceptancedate.HasValue Then
            MvCorePanelAcceptanceDate.SetActiveView(vwAvailableAcceptanceDate)
            TxtBAUTBASTDate.Text = String.Format("{0:dd-MMMM-yyyy}", acceptancedate)
        Else
            If lockinfo.DocId = Integer.Parse(ConfigurationManager.AppSettings("BAUTID").ToString()) Then
                MvCorePanelAcceptanceDate.SetActiveView(vwAvailableAcceptanceDate)
                TxtBAUTBASTDate.Text = String.Empty
            Else
                MvCorePanelAcceptanceDate.SetActiveView(vwNotAvailableAcceptanceDate)
            End If
        End If
    End Sub

    Private Sub BindDeleteData(ByVal wccid As Int32)
        MvCorePanel.SetActiveView(VwDeleteWCCForm)
        Dim info As WCCInfo = wcccontroller.GetODWCCBaseId(wccid)
        BindGeneralScope(DdlMasterScope_Del)
        BindScopeDetail(DdlTypeofWork_Del, info.GScopeId)
        TxtSubcontractorName_Del.Text = info.SubconName
        If info.IssuanceDate.HasValue Then
            TxtWCCIssuanceDate_Del.Text = String.Format("{0:dd-MMMM-yyyy}", info.IssuanceDate)
        End If

        TxtCertificateNo_Del.Text = info.CertificateNumber
        TxtSiteID_Del.Text = info.SiteNo
        TxtSiteName_Del.Text = info.SiteName
        TxtPackageId_Del.Text = info.PackageId
        TxtWorkDesc_Del.Text = info.ScopeName
        TxtPOSubcontractor_Del.Text = info.POSubcontractor
        TxtPOTelkomsel_Del.Text = info.PONO
        If (info.WCTRDate.HasValue) Then
            TxtWCTRDate_Del.Text = String.Format("{0:dd-MMMM-yyyy}", info.WCTRDate)
        End If
        DdlMasterScope_Del.SelectedValue = info.GScopeId.ToString()
        DdlTypeofWork_Del.SelectedValue = info.DScopeID.ToString()

        Dim lockinfo As CODDocActivityLockInfo = acontroller.GetDocActivityLockBaseActivity(acontroller.GetActivityIdBaseRole(CommonSite.RollId))
        Dim acceptancedate As System.Nullable(Of DateTime) = wcccontroller.GetDocAcceptanceDate(info.PackageId, lockinfo.DocId)
        LblAcceptanceDate_Del.Text = lockinfo.Disclaimer
        If acceptancedate.HasValue Then
            MvCorePanelSignDate_Del.SetActiveView(vwSignDateRO_Del)
            TxtSignDate_Del.Text = String.Format("{0:dd-MMMM-yyyy}", acceptancedate)
        Else
            MvCorePanelSignDate_Del.SetActiveView(vwSignDateET_Del)
        End If
    End Sub

#End Region
End Class
