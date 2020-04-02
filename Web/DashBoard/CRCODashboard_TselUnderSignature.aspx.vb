Imports Common
Imports System.Data
Imports System.IO

Partial Class DashBoard_CRCODashboard_TselUnderSignature
    Inherits System.Web.UI.Page
    Dim dtTaskPendings As New DataTable
    Dim objdb As New DBUtil
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not Page.IsPostBack) Then
            Dim scope As String = Request.QueryString("scope")
            BindData(scope.ToLower())
        End If
    End Sub

    Protected Sub BtnExportUserUnderSignature_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnExportUserUnderSignature.Click
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

#Region "Custom Methods"
    Private Sub BindData(ByVal scope As String)
        dtTaskPendings = objdb.ExeQueryDT("exec uspCO_GetTaskPendingReview_Detail " & CommonSite.UserId & ", '" & scope & "'", "dtTaskPendings")
        GrdDocCount.DataSource = dtTaskPendings
        GrdDocCount.DataBind()
    End Sub
#End Region
End Class
