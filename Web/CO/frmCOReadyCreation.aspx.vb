Imports Common
Imports CRFramework
Imports System.IO

Partial Class CO_frmCOReadyCreation
    Inherits System.Web.UI.Page
    Dim controller As New CRController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindData()
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

    Protected Sub GvSiteReadyCreationItemCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvSiteReadyCreation.RowCommand
        If e.CommandName.Equals("checkdoc") Then
            Response.Redirect("../CO/frmCO.aspx?wpid=" & e.CommandArgument.ToString())
        End If
    End Sub

#Region "Custom Methods"
    Private Sub BindData()
        LblSiteReadyCreation.Text = "CO Ready Creation"
        GvSiteReadyCreation.DataSource = controller.GetCOReadyCreation(CommonSite.UserId, Integer.Parse(ConfigurationManager.AppSettings("CODOCID")))
        GvSiteReadyCreation.DataBind()
    End Sub
#End Region

End Class
