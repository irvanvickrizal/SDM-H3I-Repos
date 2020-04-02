
Partial Class Admin_WCCDeletion
    Inherits System.Web.UI.Page

    Dim controller As New WCCController
    Dim acontroller As New CODActivityController
    Dim scopecontroller As New ScopeController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then

        End If
    End Sub

    Protected Sub LbtSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtSearch.Click
        If Not String.IsNullOrEmpty(TxtPackageId.Text) Then
            BindData(TxtPackageId.Text)
        Else
            If (Not ClientScript.IsStartupScriptRegistered("alert")) Then
                Page.ClientScript.RegisterStartupScript _
                (Me.GetType(), "alert", "NoWPIDSearch();", True)
            End If
        End If
    End Sub

    Protected Sub GVWCCList_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvWCCList.RowCommand
        If e.CommandName.Equals("deletewcc") Then
            HdnWCCId_Del.Value = e.CommandArgument.ToString()
            BindDeleteData(Convert.ToInt32(e.CommandArgument.ToString()))
        End If
    End Sub

    Protected Sub LbtDeleteWCC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtDeleteWCC.Click
        controller.WCCDeletionFlag(Convert.ToInt32(HdnWCCId_Del.Value), TxtRemarks.Text, True, CommonSite.UserId)
        BindData(TxtPackageId.Text)
        If (Not ClientScript.IsStartupScriptRegistered("alert")) Then
            Page.ClientScript.RegisterStartupScript _
            (Me.GetType(), "alert", "WCCDeleteSucceed();", True)
        End If
    End Sub

#Region "Custom Methods"
    Private Sub BindData(ByVal packageid As String)
        MvCorePanel.SetActiveView(VwWCCList)
        GvWCCList.DataSource = controller.GetODWCCBaseOnPackageId(packageid)
        GvWCCList.DataBind()
    End Sub

    Private Sub BindDeleteData(ByVal wccid As Int32)
        MvCorePanel.SetActiveView(VwDeleteWCCForm)
        Dim info As WCCInfo = controller.GetODWCCBaseId(wccid)
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
        Dim acceptancedate As System.Nullable(Of DateTime) = controller.GetDocAcceptanceDate(info.PackageId, lockinfo.DocId)
        LblAcceptanceDate_Del.Text = lockinfo.Disclaimer
        If acceptancedate.HasValue Then
            MvCorePanelSignDate_Del.SetActiveView(vwSignDateRO_Del)
            TxtSignDate_Del.Text = String.Format("{0:dd-MMMM-yyyy}", acceptancedate)
        Else
            MvCorePanelSignDate_Del.SetActiveView(vwSignDateET_Del)
        End If
    End Sub

    Private Sub BindGeneralScope(ByVal ddl As DropDownList)
        ddl.DataSource = scopecontroller.GetScopeMaster(False)
        ddl.DataTextField = "ScopeName"
        ddl.DataValueField = "ScopeId"
        ddl.DataBind()
        'DdlGeneralScope.Items.Insert(0, "-- General Scope --")
    End Sub

    Private Sub BindScopeDetail(ByVal ddl As DropDownList, ByVal gscopeid As Integer)
        ddl.DataSource = scopecontroller.GetAllDetailScopes(False, gscopeid)
        ddl.DataTextField = "DscopeName"
        ddl.DataValueField = "DScopeId"
        ddl.DataBind()
        ddl.Items.Insert(0, "-- Select Scope Detail --")
    End Sub

  
#End Region
End Class
