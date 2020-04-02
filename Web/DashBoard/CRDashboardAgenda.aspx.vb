Imports System.Data
Imports CRFramework
Imports System.IO

Partial Class DashBoard_CRDashboardAgenda
    Inherits System.Web.UI.Page

    Dim controller As New CRController
    Dim co_controller As New COController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            If Not String.IsNullOrEmpty(Request.QueryString("displaytype")) Then
                btnBackToDashboard.Visible = True
                BtnBackToDashboard_SiteReadyCreation.Visible = True
                BtnGoToDashboardCODone.Visible = True
            Else
                btnBackToDashboard.Visible = False
                BtnBackToDashboard_SiteReadyCreation.Visible = False
                BtnGoToDashboardCODone.Visible = False
            End If
            BindData()
        End If
    End Sub

    Protected Sub BtnExportUserUnderSignatureClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnExportUserUnderSignature.Click
        If (GrdDocCount.Rows.Count() > 0) Then
            Dim tw As New StringWriter()
            Dim strFilename As String = LblUserUnderSignature.Text + DateTime.Now.ToShortDateString + ".xls"
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)
            Dim frm As HtmlForm = New HtmlForm()
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("content-disposition", "attachment;filename=" + strFilename)
            Response.Charset = ""
            EnableViewState = False
            Controls.Add(frm)
            frm.Controls.Add(GrdDocCount)
            frm.RenderControl(hw)
            Response.Write(tw.ToString())
            Response.End()
        Else
            If (Not ClientScript.IsStartupScriptRegistered("alert")) Then
                Page.ClientScript.RegisterStartupScript _
                (Me.GetType(), "alert", "invalidExportToExcel();", True)
            End If
        End If
    End Sub

    Protected Sub BtnExptSiteReadyCreationClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnExptSiteReadyCreation.Click
        If (GvSiteReadyCreation.Rows.Count() > 0) Then
            Dim tw As New StringWriter()
            Dim strFilename As String = LblSiteReadyCreation.Text + DateTime.Now.ToShortDateString + ".xls"
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)
            Dim frm As HtmlForm = New HtmlForm()
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("content-disposition", "attachment;filename=" + strFilename)
            Response.Charset = ""
            EnableViewState = False
            Controls.Add(frm)
            frm.Controls.Add(GvSiteReadyCreation)
            frm.RenderControl(hw)
            Response.Write(tw.ToString())
            Response.End()
        Else
            If (Not ClientScript.IsStartupScriptRegistered("alert")) Then
                Page.ClientScript.RegisterStartupScript _
                (Me.GetType(), "alert", "invalidExportToExcel();", True)
            End If
        End If
    End Sub

    Protected Sub btnExptCRCOReportingClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExptCRCOReporting.Click
        If (GvCRCOReporting.Rows.Count() > 0) Then
            Dim tw As New StringWriter()
            Dim strFilename As String = LblCRCOReporting.Text + DateTime.Now.ToShortDateString + ".xls"
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)
            Dim frm As HtmlForm = New HtmlForm()
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("content-disposition", "attachment;filename=" + strFilename)
            Response.Charset = ""
            EnableViewState = False
            Controls.Add(frm)
            frm.Controls.Add(GvCRCOReporting)
            frm.RenderControl(hw)
            Response.Write(tw.ToString())
            Response.End()
        Else
            If (Not ClientScript.IsStartupScriptRegistered("alert")) Then
                Page.ClientScript.RegisterStartupScript _
                (Me.GetType(), "alert", "invalidExportToExcel();", True)
            End If
        End If
    End Sub

    Protected Sub BtnExportCODoneClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnExportCODone.Click
        '
        If (GvCODoneReport.Rows.Count() > 0) Then
            Dim tw As New StringWriter()
            Dim strFilename As String = "CODone_" + DateTime.Now.ToShortDateString + ".xls"
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)
            Dim frm As HtmlForm = New HtmlForm()
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("content-disposition", "attachment;filename=" + strFilename)
            Response.Charset = ""
            EnableViewState = False
            Controls.Add(frm)
            frm.Controls.Add(GvCODoneReport)
            frm.RenderControl(hw)
            Response.Write(tw.ToString())
            Response.End()
        Else
            If (Not ClientScript.IsStartupScriptRegistered("alert")) Then
                Page.ClientScript.RegisterStartupScript _
                (Me.GetType(), "alert", "invalidExportToExcel();", True)
            End If
        End If
    End Sub

    Protected Sub GvSiteReadyCreationItemCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvSiteReadyCreation.RowCommand
        If GetTypeId().Equals("nc") Or GetTypeId().Equals("rc") Then
            If e.CommandName.Equals("checkdoc") Then
                FormCRCreation(e.CommandArgument.ToString())
            End If
        ElseIf GetTypeId().Equals("oc") Then
            If e.CommandName.Equals("checkdoc") Then

            End If
        End If
    End Sub

    Protected Sub GvCRCOReportingRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvCRCOReporting.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If GetTypeId().Equals("cf") Then
                e.Row.Cells(7).Visible = True
            Else
                e.Row.Cells(7).Visible = False
            End If
        End If
    End Sub

    Protected Sub btnBackToDashboardClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBackToDashboard.Click
        Response.Redirect("../frmDashboard_temp.aspx")
    End Sub

    Protected Sub BtnBackToDashboard_SiteReadyCreationClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnBackToDashboard_SiteReadyCreation.Click
        Response.Redirect("../frmDashboard_temp.aspx")
    End Sub

    Protected Sub BtnGoToDashboardCODoneClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnGoToDashboardCODone.Click
        Response.Redirect("../frmDashboard_temp.aspx")
    End Sub


#Region "Custom Methods"
    Private Sub BindData()
        If GetTypeId().Equals("nc") Then 'Mean get Site Need CR
            BindSiteNeedCR()
        ElseIf GetTypeId().Equals("rc") Then
            BindCRReadyCreation()
        ElseIf GetTypeId().Equals("ns") Then 'Mean get CR NSN Undersignature 
            BindCRNSNUndersignature()
        ElseIf GetTypeId().Equals("ts") Then 'Mean get CR Tsel Undersignature
            BindCRTselUndersignature()
        ElseIf GetTypeId().Equals("cf") Then 'Mean get CR Final
            BindCRFinal()
        ElseIf GetTypeId().Equals("oc") Then 'Mean get CO Ready Creation
            BindCOReadyCreation()
        ElseIf GetTypeId().Equals("cons") Then 'Mean get CO NSN Undersignature 
            BindCONSNUndersignature()
        ElseIf GetTypeId().Equals("cots") Then 'Mean get CO Tsel Undersignature 
            BindCOTselUndersignature()
        ElseIf GetTypeId().Equals("cod") Then
            BindCODone()
        End If
    End Sub

    Private Sub BindSiteNeedCR()
        MvCorePanel.SetActiveView(VwSiteCRCOReadyCreation)
        LblSiteReadyCreation.Text = "CR Ready Creation"
        GvSiteReadyCreation.DataSource = controller.GetDetailSiteNeedCR(CommonSite.UserId, Integer.Parse(ConfigurationManager.AppSettings("CRDOCID")))
        GvSiteReadyCreation.DataBind()
    End Sub

    Private Sub BindCRReadyCreation()
        MvCorePanel.SetActiveView(VwSiteCRCOReadyCreation)
        LblSiteReadyCreation.Text = "CR Ready Creation"
        GvSiteReadyCreation.DataSource = controller.GetDetailCRReadyCreation(CommonSite.UserId)
        GvSiteReadyCreation.DataBind()
    End Sub

    Private Sub BindCOReadyCreation()
        MvCorePanel.SetActiveView(VwSiteCRCOReadyCreation)
        LblSiteReadyCreation.Text = "CO Ready Creation"
        GvSiteReadyCreation.DataSource = controller.GetCOReadyCreation(CommonSite.UserId, Integer.Parse(ConfigurationManager.AppSettings("CODOCID")))
        GvSiteReadyCreation.DataBind()
    End Sub

    Private Sub FormCRCreation(ByVal packageid As String)
        Response.Redirect("../CR/frmListCR.aspx?wpid=" & packageid)
    End Sub

    Private Sub BindCRNSNUndersignature()
        MvCorePanel.SetActiveView(vwGeneralTask)
        LblUserUnderSignature.Text = "NSN Under Signature"
        GrdDocCount.DataSource = controller.GetUnderSignatureUser(CommonSite.UserId, 1)
        GrdDocCount.DataBind()
    End Sub

    Private Sub BindCRTselUndersignature()
        MvCorePanel.SetActiveView(vwGeneralTask)
        LblUserUnderSignature.Text = "Telkomsel Under Signature"
        GrdDocCount.DataSource = controller.GetUnderSignatureUser(CommonSite.UserId, 4)
        GrdDocCount.DataBind()
    End Sub

    Private Sub BindCRFinal()
        MvCorePanel.SetActiveView(VwReportingFinal)
        LblCRCOReporting.Text = "CR Done"
        GvCRCOReporting.DataSource = controller.GetAllCRFinal(CommonSite.UserId, Integer.Parse(ConfigurationManager.AppSettings("CRDOCID")))
        GvCRCOReporting.DataBind()
    End Sub

    Private Sub BindCONSNUndersignature()
        MvCorePanel.SetActiveView(vwGeneralTask)
        LblUserUnderSignature.Text = "CO NSN Under Signature"
        GrdDocCount.DataSource = co_controller.GetUnderSignature(CommonSite.UserId, 1, ConfigurationManager.AppSettings("codocid"))
        GrdDocCount.DataBind()
    End Sub

    Private Sub BindCOTselUndersignature()
        MvCorePanel.SetActiveView(vwGeneralTask)
        LblUserUnderSignature.Text = "CO Telkomsel Under Signature"
        GrdDocCount.DataSource = co_controller.GetUnderSignature(CommonSite.UserId, 4, ConfigurationManager.AppSettings("codocid"))
        GrdDocCount.DataBind()
    End Sub

    Private Sub BindCODone()
        MvCorePanel.SetActiveView(VwCODoneReporting)
        GvCODoneReport.DataSource = co_controller.GetCODone(Integer.Parse(ConfigurationManager.AppSettings("codocid")), Convert.ToInt32(CommonSite.UserId))
        GvCODoneReport.DataBind()
    End Sub

    Private Function GetTypeId() As String
        If Not String.IsNullOrEmpty(Request.QueryString("typedash")) Then
            Return Request.QueryString("typedash")
        End If
        Return String.Empty
    End Function


#End Region

End Class
