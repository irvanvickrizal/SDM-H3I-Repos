Imports System.IO

Partial Class fancybox_Form_SOAC_fb_SOACHistoricalRejectionLog
    Inherits System.Web.UI.Page

    Dim controller As New SOACController()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Not String.IsNullOrEmpty(Request.QueryString("sid")) Then
                BindAttribute(Request.QueryString("wpid"))
                BindData(Convert.ToInt32(Request.QueryString("sid")))
            End If
        End If
    End Sub

    Protected Sub BtnExportExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnExportExcel.Click
        If GvSOACLog.Rows.Count > 0 Then
            Dim tw As New StringWriter()
            Dim getsiteatt As String = LblSiteNo.Text & "_" & LblPONO.Text & "_"
            Dim strFilename As String = "SOAC_" + getsiteatt + "HistoricalRejectLog_" + DateTime.Now.ToShortDateString + ".xls"
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)
            Dim frm As HtmlForm = New HtmlForm()
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("content-disposition", "attachment;filename=" + strFilename)
            Response.Charset = ""
            EnableViewState = False
            Controls.Add(frm)
            frm.Controls.Add(GvSOACLog)
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

#Region "Custom Methods"
    Private Sub BindAttribute(ByVal packageid As String)
        Dim info As SiteInfo = controller.GetSiteDetail_WPID(packageid, 1855)
        LblSitename.Text = info.SiteName
        LblSiteNo.Text = info.SiteNo
        LblPONO.Text = info.PONO
        LblScope.Text = info.Scope
        LblWorkpackageid.Text = packageid
    End Sub
    Private Sub BindData(ByVal soacid As Int32)
        GvSOACLog.DataSource = controller.GetHistoricalRejection(soacid)
        GvSOACLog.DataBind()
    End Sub
#End Region

End Class
