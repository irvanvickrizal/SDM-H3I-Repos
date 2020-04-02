
Partial Class WCC_frmWCCPreparation
    Inherits System.Web.UI.Page

    Private controller As New WCCController
    Private scontroller As New ScopeController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Not String.IsNullOrEmpty(Request.QueryString("wid")) Then
                BindData(Convert.ToInt32(Request.QueryString("wid")))
            End If
        End If
    End Sub

    Protected Sub LbtUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtUpdate.Click
        Dim info As New WCCInfo
        LbtEdit.Visible = True
        LbtCreateFolder.Visible = True
        info.WCCID = Convert.ToInt32(hdnWCCID.Value)
        info.DScopeID = Integer.Parse(DdlScopeDetail.SelectedValue)

        info.BAUTDate = DateTime.ParseExact(TxtBAUTBASTDate.Text, "dd-MMMM-yyyy", Nothing)
        info.PackageId = TxtWorkpackageId.Text
        info.SCONID = Integer.Parse(HdnSubconId.Value)
        info.WCTRDate = DateTime.ParseExact(TxtWCTRDate.Text, "dd-MMMM-yyyy", Nothing)
        info.POSubcontractor = TxtPOSubcontractor.Text
        info.LMBY = CommonSite.UserId
        Dim wccid As Int32 = controller.ODWCC_I(info)
        If (wccid > 0) Then
            hdnWCCID.Value = Convert.ToString(wccid)
            BindSupportingDocument(info.DScopeID, info.PackageId, New CODActivityController().GetActivityIdBaseRole(CommonSite.RollId))
            LbtUpdate.Visible = False
            LbtEdit.Visible = True
            LbtCreateFolder.Visible = True
        End If
    End Sub

    Protected Sub LbtEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtEdit.Click
        LbtEdit.Visible = False
        LbtUpdate.Visible = True
        LbtCreateFolder.Visible = False
        DdlGeneralScope.Enabled = True
        DdlScopeDetail.Enabled = True
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
                    controller.SiteFolder_I(sitedocinfo)
                End If
            End If
        Next
        Response.Redirect("frmODWCCFinal.aspx?wid=" & hdnWCCID.Value)
    End Sub

    Protected Sub DdlGeneralScope_IndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlGeneralScope.SelectedIndexChanged
        If (DdlGeneralScope.SelectedIndex > 0) Then
            BindDetailScope(Integer.Parse(DdlGeneralScope.SelectedValue), 0, False)
        Else
            BindDetailScope(0, 0, False)
        End If
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

#Region "Custom Methods"

    Private Sub BindData(ByVal wccid As Int32)
        hdnWCCID.Value = Convert.ToString(wccid)

        Dim info As WCCInfo = controller.GetODWCCBaseId(wccid)
        TxtSubconCompany.Text = info.SubconName
        'TxtWccIssuanceDate.Text = info.IssuanceDate
        TxtCertificateNumber.Text = info.CertificateNumber
        TxtSiteID.Text = info.SiteNo
        TxtSiteName.Text = info.SiteName
        TxtWorkpackageId.Text = info.PackageId
        TxtWorkDescription.Text = info.ScopeName
        TxtPOSubcontractor.Text = info.POSubcontractor
        TxtPOTelkomsel.Text = info.PONO
        TxtBAUTBASTDate.Text = String.Format("{0:dd-MMMM-yyyy}", info.BAUTDate)
        TxtWCTRDate.Text = String.Format("{0:dd-MMMM-yyyy}", info.WCTRDate)
        aSiteInformation.HRef = "../fancybox_Form/fb_SiteInformation.aspx?pid=" & info.PackageId
        BindGeneralScope(info.GScopeId, True)
        BindDetailScope(info.GScopeId, info.DScopeID, True)
        BindSupportingDocument(info.DScopeID, info.PackageId, New CODActivityController().GetActivityIdBaseRole(CommonSite.RollId))
        LbtUpdate.Visible = False
        TxtPOSubcontractor.ReadOnly = True
        DdlScopeDetail.Enabled = False
        DdlGeneralScope.Enabled = False
        TxtWCTRDate.Enabled = False
        HdnSubconId.Value = Integer.Parse(info.SCONID)
    End Sub

    Private Sub BindGeneralScope(ByVal scopeid As Integer, ByVal isBinding As Boolean)
        DdlGeneralScope.DataSource = scontroller.GetScopeMaster(False)
        DdlGeneralScope.DataTextField = "ScopeName"
        DdlGeneralScope.DataValueField = "ScopeId"
        DdlGeneralScope.DataBind()
        DdlGeneralScope.Items.Insert(0, "-- General Scope --")

        If isBinding = True Then
            DdlGeneralScope.SelectedValue = Convert.ToString(scopeid)
        End If
    End Sub

    Private Sub BindDetailScope(ByVal gscopeid As Integer, ByVal dscopeid As Integer, ByVal isBinding As Boolean)
        DdlScopeDetail.DataSource = scontroller.GetAllDetailScopes(False, gscopeid)
        DdlScopeDetail.DataTextField = "DscopeName"
        DdlScopeDetail.DataValueField = "DScopeId"
        DdlScopeDetail.DataBind()
        DdlScopeDetail.Items.Insert(0, "-- Select Scope Detail --")
        If isBinding = True Then
            DdlScopeDetail.SelectedValue = Convert.ToString(dscopeid)
        End If
    End Sub

    Private Sub BindSupportingDocument(ByVal dscopeid As Integer, ByVal packageid As String, ByVal activityid As Integer)
        MvCorePanelDocInitiatilation.SetActiveView(VwBAUTDocInitialize)
        GvSupportingDocuments.DataSource = controller.GetDocWCCAdditionalDocument(dscopeid, False, Integer.Parse(ConfigurationManager.AppSettings("WCCDOCID")), packageid, activityid)
        GvSupportingDocuments.DataBind()
    End Sub
#End Region

End Class
